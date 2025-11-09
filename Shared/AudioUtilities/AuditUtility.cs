using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Providers;
using SoundFlow.Structs;

namespace Sylais.AudioUtilities;

public interface IAudioUtility
{
    public DeviceInfo[] ListPlaybackDevices();
    public DeviceInfo[] ListCaptureDevices();
    public void ReInitDevices(DeviceInfo deviceInfo);
    public void CaptureAudio(string? path = null);
    public void PlayAudio(string path);
}

public class AudioUtility : IDisposable, IAudioUtility
{
    private MiniAudioEngine _audioEngine;
    private AudioCaptureDevice _captureDevice;
    private AudioPlaybackDevice _playbackDevice;

    public AudioUtility(MiniAudioEngine audioEngine)
    {
        _audioEngine = audioEngine;
        var audioFormat = new AudioFormat
        {
            Format = SampleFormat.F32,
            SampleRate = 48000,
            Channels = 1,
        };
        var capturDev = _audioEngine.CaptureDevices.FirstOrDefault(pr => pr.IsDefault);
        var playbackDev = _audioEngine.PlaybackDevices.FirstOrDefault(pr => pr.IsDefault);

        _captureDevice = _audioEngine.InitializeCaptureDevice(capturDev, audioFormat);
        _playbackDevice = _audioEngine.InitializePlaybackDevice(playbackDev, audioFormat);
    }

    public void ReInitDevices(DeviceInfo deviceInfo)
    {
        _captureDevice = _audioEngine.InitializeCaptureDevice(deviceInfo, AudioFormat.DvdHq);
        _playbackDevice = _audioEngine.InitializePlaybackDevice(deviceInfo, AudioFormat.DvdHq);
    }

    public DeviceInfo[] ListCaptureDevices() => _audioEngine.CaptureDevices;

    public DeviceInfo[] ListPlaybackDevices() => _audioEngine.PlaybackDevices;

    public void Dispose()
    {
        _captureDevice.Dispose();
        _playbackDevice.Dispose();
        _audioEngine.Dispose();
    }

    public void CaptureAudio(string? path = null)
    {
        if (String.IsNullOrEmpty(path))
            path = Path.Combine(Directory.GetCurrentDirectory(), "output.wav");

        var defaultDevice = _audioEngine.CaptureDevices.FirstOrDefault(pr => pr.IsDefault);

        if (defaultDevice.Id == IntPtr.Zero)
        {
            Console.WriteLine("Cant Find Default Device");
            return;
        }

        // ReInitDevices(defaultDevice);

        using var fileStream = new FileStream(
            path,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None
        );

        using var recorder = new Recorder(_captureDevice, fileStream, EncodingFormat.Wav);

        Console.WriteLine("Recording... Press any key to stop.");
        _captureDevice.Start();
        recorder.StartRecording();

        Console.ReadKey();
        recorder.StopRecording();
        _captureDevice.Stop();

        Console.WriteLine($"Recording stopped. Saved to {path}");
    }

    public void PlayAudio(string path)
    {
        var audioFormat = new AudioFormat
        {
            Format = SampleFormat.F32,
            SampleRate = 48000,
            Channels = 1,
        };
        using var dataProvider = new StreamDataProvider(
            _audioEngine,
            audioFormat,
            File.OpenRead(path)
        );

        using var player = new SoundPlayer(_audioEngine, audioFormat, dataProvider);

        _playbackDevice.MasterMixer.AddComponent(player);

        _playbackDevice.Start();

        // Start playback.
        player.Play();

        Console.WriteLine("Playing audio... Press any key to exit.");
        Console.ReadKey();

        _playbackDevice.Stop();
    }
}
