using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;
using Sylais.Constant;

namespace Sylais;

public class AudioEngineManager : IDisposable
{
    private MiniAudioEngine? _audioEngine;
    private Capability? _capability;
    private static Lazy<AudioEngineManager> _audioEngineManager = new Lazy<AudioEngineManager>(() =>
        new AudioEngineManager()
    );

    public static AudioEngineManager Instance => _audioEngineManager.Value;

    public MiniAudioEngine UseAsRecord()
    {
        if (_audioEngine != null && _capability != Capability.Record)
            _audioEngine.Dispose();

        if (_audioEngine != null && _capability == Capability.Record)
            return _audioEngine;

        return new MiniAudioEngine(AudioConstant.SampleRate, Capability.Record);
    }

    public MiniAudioEngine UseAsPlayback()
    {
        if (_audioEngine != null && _capability != Capability.Playback)
            _audioEngine.Dispose();

        if (_audioEngine != null && _capability == Capability.Playback)
            return _audioEngine;

        return new MiniAudioEngine(AudioConstant.SampleRate, Capability.Playback);
    }

    public void Dispose()
    {
        _audioEngine?.Dispose();
        _audioEngine = null;
        _capability = null;
    }
}
