using Xunit.Abstractions;
using SoundFlow.Backends.MiniAudio;
using Sylais.AudioUtilities;

namespace Sylais.Test.Shared;

public class AudioUtilityTest
{
    protected readonly ITestOutputHelper _output;

    public AudioUtilityTest(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
    }
    [Fact]
    public void ListDevices()
    {
        using var audioEngine = new MiniAudioEngine();
        using var audioUtil = new AudioUtility(audioEngine);

        var captureDevices = audioUtil.ListCaptureDevices();
        var playbackDevices = audioUtil.ListPlaybackDevices();

        foreach (var device in captureDevices)
            Console.WriteLine($"- {device.Name} {(device.IsDefault ? "(Default)" : "")}");

        foreach (var device in playbackDevices)
            Console.WriteLine($"- {device.Name} {(device.IsDefault ? "(Default)" : "")}");
    }

    [Fact]
    public void TestRecord()
    {
        using var audioEngine = new MiniAudioEngine();
        using var audioUtil = new AudioUtility(audioEngine);

        audioUtil.CaptureAudio();
    }

}
