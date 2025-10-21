using Xunit.Abstractions;

namespace Sylais.Test
{
    public class KokoroSampleTest
    {
        private readonly ITestOutputHelper _output;

        public KokoroSampleTest(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
        }

        [Fact]
        public void RunSampleTest()
        {
            Sylais.Kokoro.KokoroTester.RunSample();
        }
    }
}
