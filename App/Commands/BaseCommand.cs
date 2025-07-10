using System.Text;
using Sylais.Models;

namespace Sylais.Commands;

public class BaseCommand
{
    protected DependenciesBinaryPathConfig _dependenciesBinaryPathConfig;
    protected StringBuilder _outBuffer = new StringBuilder();
    protected StringBuilder _errBuffer = new StringBuilder();

    public BaseCommand(DependenciesBinaryPathConfig dependenciesBinaryPathConfig)
    {
        _dependenciesBinaryPathConfig = dependenciesBinaryPathConfig;
    }
}
