using System.Text;
using CliWrap;

namespace Sylais.Extensions;

public static class CommandExtension
{
    public static Command AddOutAndErrorStringBuilderBuffer(
        this Command command,
        StringBuilder outBuffer,
        StringBuilder errorBuffer
    )
    {
        return command
            .WithStandardOutputPipe(PipeTarget.ToStringBuilder(outBuffer))
            .WithStandardErrorPipe(PipeTarget.ToStringBuilder(errorBuffer));
    }
}
