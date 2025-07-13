using Microsoft.Extensions.Configuration;
using Sylais.Test.Common;
using Xunit.Abstractions;

namespace Sylais.Test.Fixture;

public class BoostrapFixture : IDisposable
{
    public IConfiguration Configuration;

    public BoostrapFixture()
    {
        Configuration = CommonConfiguration.InitConfiguration();
    }

    public void Dispose()
    {
        // TODO:
    }
}
