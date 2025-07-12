using Microsoft.Extensions.Configuration;
using Sylais.Test.Common;
using Xunit.Abstractions;

namespace Sylais.Test.Fixture;

public class BoostrapFixture : IDisposable
{
    protected IConfiguration _configuration;

    public BoostrapFixture()
    {
        _configuration = CommonConfiguration.InitConfiguration();
    }

    public void Dispose()
    {
        // TODO:
    }
}
