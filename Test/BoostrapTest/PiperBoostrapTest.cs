using Sylais.Boostrap;
using Sylais.Extensions;
using Sylais.Test.Fixture;
using Xunit.Abstractions;

namespace Sylais.Test.Boostrap;

public class PiperBoostrapTest : IClassFixture<BoostrapFixture>
{
    BoostrapFixture _boostrapFixture;

    protected readonly ITestOutputHelper _output;

    public PiperBoostrapTest(BoostrapFixture boostrapFixture, ITestOutputHelper testOutputHelper)
    {
        _boostrapFixture = boostrapFixture;
        _output = testOutputHelper;
    }

    [Fact]
    public void PiperBoostrapTest_CheckPythonVersion()
    {
        var piper = new PiperBoostrap();
        piper.CheckPythonVersion().GetAwaiter().GetResult();
    }
}
