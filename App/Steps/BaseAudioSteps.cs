using SoundFlow.Backends.MiniAudio;
using SoundFlow.Structs;
using Sylais.Models;

namespace Sylais.Steps
{
    public abstract class BaseAudioSteps : IBaseStep
    {
        protected MiniAudioEngine? _audioEngine;
        protected DeviceInfo? _currentCaptureDevice;
        protected AudioFileConfig _audioConfig;

        public BaseAudioSteps(AudioFileConfig audioFileConfig)
        {
            _audioConfig = audioFileConfig;
        }

        public virtual void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsRecord();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public void Dispose()
        {
            _audioEngine?.Dispose();
        }

        public abstract Task Run();
    }
}
