using CliWrap;
using Microsoft.Extensions.Hosting;

namespace Sylais.Commands;

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
    private Command _command;
    public PiperServerCommand()
    {
        _command = Cli.Wrap("");

    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
