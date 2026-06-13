use clap::Parser;
use image::{ImageReader, imageops::FilterType};
use ndarray::{Axis, s};
use ort::{session::Session, value::Tensor};
use std::path::{self, Path};

pub const IMAGENET_MEAN: [f32; 3] = [0.485f32, 0.456, 0.406];
pub const IMAGENET_STD: [f32; 3] = [0.229f32, 0.224, 0.225];

#[derive(Parser)]
struct Args {
    /// image path
    #[arg(long)]
    image: String,

    /// model path
    #[arg(long)]
    model: String,
}
pub fn load_image_with_std_mean<P: AsRef<std::path::Path>>(
    path: P,
    resolution: usize,
    mean: &[f32; 3],
    std: &[f32; 3],
) -> anyhow::Result<Tensor<f32>> {
    let img = ImageReader::open(path)?.decode()?.resize_to_fill(
        resolution as u32,
        resolution as u32,
        FilterType::Triangle,
    );

    let img = img.to_rgb8();
    let data = img.into_raw();

    let mut tensor_data =
        ndarray::Array::from_shape_vec((resolution, resolution, 3), data)?.mapv(|x| x as f32);
    tensor_data.permute_axes((2, 0, 1));

    let mean = ndarray::Array::from_shape_vec((3, 1, 1), mean.to_vec())?;
    let std = ndarray::Array::from_shape_vec((3, 1, 1), std.to_vec())?;

    let normalized_image = ((tensor_data / 255.) - mean) / std;
    let ort_tensor: Tensor<f32> = Tensor::from_array(normalized_image.insert_axis(Axis(0)))?;

    println!("Loaded image tensor shape: {:?}", ort_tensor.shape());

    Ok(ort_tensor)
}

pub fn load_image<P: AsRef<std::path::Path>>(
    path: P,
    resolution: usize,
) -> anyhow::Result<Tensor<f32>> {
    load_image_with_std_mean(path, resolution, &IMAGENET_MEAN, &IMAGENET_STD)
}

pub fn load_image224<P: AsRef<std::path::Path>>(path: P) -> anyhow::Result<Tensor<f32>> {
    load_image_with_std_mean(path, 224, &IMAGENET_MEAN, &IMAGENET_STD)
}

pub fn main() -> anyhow::Result<()> {
    let args = Args::parse();

    let image_path = Path::new(&args.image);
    let model_path = Path::new(&args.model);

    let abs_image_path = path::absolute(image_path)?;
    let abs_model_path = path::absolute(model_path)?;

    if abs_image_path.exists() {
        println!("Image path: {}", abs_image_path.display());
    } else {
        anyhow::bail!("Image path does not exist: {}", abs_image_path.display());
    }

    if abs_model_path.exists() {
        println!("Model path: {}", abs_model_path.display());
    } else {
        anyhow::bail!("Model path does not exist: {}", abs_model_path.display());
    }

    let image_tensor = load_image224(&abs_image_path)?;

    println!("image tensor shape: {:?}", image_tensor.shape());
    let mut session = Session::builder()?.commit_from_file(abs_model_path)?;

    for input in session.inputs() {
        println!("input name: {}, type: {}", input.name(), input.dtype());
    }

    for output in session.outputs() {
        println!("output name: {}, type: {}", output.name(), output.dtype());
    }

    let output = session.run(ort::inputs! {
        "data" => image_tensor
    })?;

    for key in output.keys() {
        println!("output key: {}", key);
    }

    let out_result = output
        .get("squeezenet0_flatten0_reshape0")
        .expect("fail to get output result");

    println!("output tensor shape: {:?}", out_result.shape());

    let out_result = out_result.try_extract_array::<f32>()?;
    println!("prediction shape: {:?}", out_result.shape());

    let prediction = out_result.slice(s!(0, ..));

    let mut sorted_prediction: Vec<_> = prediction.to_vec().into_iter().enumerate().collect();
    sorted_prediction.sort_by(|a, b| b.1.partial_cmp(&a.1).unwrap());

    let top = sorted_prediction.into_iter().take(5).collect::<Vec<_>>();

    // Print the top predictions
    for &(i, p) in &top {
        println!("{} -> {:50}: {:.2}%", i, squeezenet_ort::CLASSES[i], p);
    }

    Ok(())
}
