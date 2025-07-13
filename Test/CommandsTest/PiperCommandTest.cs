using CliWrap;
using CliWrap.Buffered;
using Xunit.Abstractions;

namespace Sylais.Test.Boostrap;

public class PiperCommandTest
{
    protected readonly ITestOutputHelper _output;

    public PiperCommandTest(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
    }

    [Fact]
    public void PiperBoostrapTest_CheckPythonVersion() { }

    [Fact]
    public void PiperBoostrapTest_StartPiperServer()
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(20));

        var workingDirectory =
            "D:\\project\\personalProject\\sylais\\App\\BinaryDependencies\\piper\\venv\\Scripts";

        var result = Cli.Wrap(Path.Combine(workingDirectory, "python"))
            .WithWorkingDirectory(workingDirectory)
            .WithArguments(["-m", "piper.http_server", "-m", "../../en_US-hfc_female-medium"])
            .ExecuteBufferedAsync(cts.Token)
            .GetAwaiter()
            .GetResult();

        _output.WriteLine(result.StandardOutput);
    }
}
