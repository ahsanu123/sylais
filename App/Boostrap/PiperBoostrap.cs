using System.Text;
using CliWrap;
using Sylais.Steps;

namespace Sylais.Boostrap;

public class PiperBoostrap : IBaseStep
{
    private Command _pythonCommand = Cli.Wrap("python");
    private StringBuilder _outputStringBuilder = new StringBuilder();

    public async Task CheckPythonVersion()
    {
        var pythonVersion = await _pythonCommand
            .WithArguments(["--version"])
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(_outputStringBuilder))
            .ExecuteAsync();
    }

    public async Task CreatePiperVenv()
    {

    }

    public void InstallPiper() { }

    public void DownloadPiperVoice() { }

    public async Task Run()
    {
        await CreatePiperVenv();
        InstallPiper();
        DownloadPiperVoice();
    }

    public void Dispose() { }
}
