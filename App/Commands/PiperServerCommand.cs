using CliWrap;
using Microsoft.Extensions.Hosting;
using Sylais.Models;

namespace Sylais.Commands;

// (venv) D:\project\personalProject\sylais\App\BinaryDependencies\piper>python -m piper.http_server --help
// usage: http_server.py [-h] [--host HOST] [--port PORT] -m MODEL [-s SPEAKER] [--length-scale LENGTH_SCALE] [--noise-scale NOISE_SCALE]
//                       [--noise-w-scale NOISE_W_SCALE] [--cuda] [--sentence-silence SENTENCE_SILENCE] [--data-dir DATA_DIR]
//                       [--download-dir DOWNLOAD_DIR] [--debug]
//
// options:
//   -h, --help            show this help message and exit
//   --host HOST           HTTP server host
//   --port PORT           HTTP server port
//   -m, --model MODEL     Path to Onnx model file
//   -s, --speaker SPEAKER
//                         Id of speaker (default: 0)
//   --length-scale, --length_scale LENGTH_SCALE
//                         Phoneme length
//   --noise-scale, --noise_scale NOISE_SCALE
//                         Generator noise
//   --noise-w-scale, --noise_w_scale, --noise-w, --noise_w NOISE_W_SCALE
//                         Phoneme width noise
//   --cuda                Use GPU
//   --sentence-silence, --sentence_silence SENTENCE_SILENCE
//                         Seconds of silence after each sentence
//   --data-dir, --data_dir DATA_DIR
//                         Data directory to check for downloaded models (default: current directory)
//   --download-dir, --download_dir DOWNLOAD_DIR
//                         Path to download voices (default: first data dir)
//   --debug               Print DEBUG messages to console


// (venv) D:\project\personalProject\sylais\App\BinaryDependencies\piper>python -m piper.download_voices --help
// usage: download_voices.py [-h] [--download-dir DOWNLOAD_DIR] [--force-redownload] [--debug] [voice ...]
//
// positional arguments:
//   voice                 Name of voice like 'en_US-lessac-medium'
//
// options:
//   -h, --help            show this help message and exit
//   --download-dir, --download_dir, --data-dir, --data_dir DOWNLOAD_DIR
//                         Directory to download voices into (default: current directory)
//   --force-redownload, --force_redownload
//                         Force redownloading of voice files even if they exist already
//   --debug               Print DEBUG logs to console

public class PiperServerCommand : BaseCommand, IHostedService
{
    // TODO: 
    // Big TODO.
    private Command _startPiperServer = Cli.Wrap("python -m piper.http_server");
    private Command _listVoices = Cli.Wrap("python -c \"from piper import download_voices; download_voices.list_voices()\"");
    private PiperConfig _piperConfig;

    public PiperServerCommand(PiperConfig piperConfig)
    {
        _piperConfig = piperConfig;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // var result = Cli.Wrap("python").WithWorkingDirectory(_piperConfig.PiperPath).WithArguments([
        //     "-m",
        //     ""
        // ]);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}





























