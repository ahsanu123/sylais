using Sylais.Models;

namespace Sylais.Commands;

public class BaseCommand
{
    protected DependenciesBinaryPathConfig _binaryPathConfig;

    public BaseCommand(DependenciesBinaryPathConfig dependenciesBinaryPathConfig)
    {
        _binaryPathConfig = dependenciesBinaryPathConfig;
    }
}
