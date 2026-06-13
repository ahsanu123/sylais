# Squeezenet with ORT
<p align="center">
  <img width="571"  alt="image" src="https://github.com/user-attachments/assets/8bea1882-bcb0-4cbe-8b1c-c9673a99cc93" />  
</p>


```shell

ah@ah-legion ~/p/s/e/squeezenet_ort (main)> cargo run -- --model ~/aimodels/squeezenet-candle-onnx/squeezenet1.1-7.onnx --image ./toilet.jpg
    Finished `dev` profile [unoptimized + debuginfo] target(s) in 0.06s
     Running `target/debug/squeezenet_ort --model /home/ah/aimodels/squeezenet-candle-onnx/squeezenet1.1-7.onnx --image ./toilet.jpg`
Image path: /home/ah/projects/sylais/explore/squeezenet_ort/toilet.jpg
Model path: /home/ah/aimodels/squeezenet-candle-onnx/squeezenet1.1-7.onnx
Loaded image tensor shape: [1, 3, 224, 224]
image tensor shape: [1, 3, 224, 224]
input name: data, type: Tensor<f32>(1, 3, 224, 224)
output name: squeezenet0_flatten0_reshape0, type: Tensor<f32>(1, 1000)
output key: squeezenet0_flatten0_reshape0
output tensor shape: [1, 1000]
prediction shape: [1, 1000]
861 -> toilet seat                                       : 29.13%
896 -> washbasin, handbasin, washbowl, lavabo, wash-hand basin: 24.71%
804 -> soap dispenser                                    : 23.71%
505 -> coffeepot                                         : 22.22%
435 -> bathtub, bathing tub, bath, tub                   : 21.56%

```


- https://huggingface.co/lmz/candle-onnx/tree/main
