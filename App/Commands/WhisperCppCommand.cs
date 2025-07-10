using CliWrap;
using Sylais.Extensions;
using Sylais.Models;

namespace Sylais.Commands;

public class WhisperCppCommand : BaseCommand
{
    private Command _command;
    private AudioFileConfig _audioConfig;

    public WhisperCppCommand(
        DependenciesBinaryPathConfig dependenciesBinaryPathConfig,
        AudioFileConfig audioFileConfig
    )
        : base(dependenciesBinaryPathConfig)
    {
        _command = Cli.Wrap(Path.GetFullPath(dependenciesBinaryPathConfig.WhisperCppPath));
        _audioConfig = audioFileConfig;
    }

    public async Task<string> Transcribe()
    {
        var fullWavPath = _audioConfig.GetTempWavPath();
        var fullWhisperModelPath = Path.GetFullPath(_dependenciesBinaryPathConfig.WhisperModelPath);

        var result = await _command
            .WithArguments(["-f", fullWavPath, "-m", fullWhisperModelPath, "--no-prints"])
            .AddOutAndErrorStringBuilderBuffer(_outBuffer, _errBuffer)
            .ExecuteAsync();

        return _outBuffer.ToString();
    }
}
