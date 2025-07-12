using Sylais.Models;

namespace Sylais.Steps
{
    public class TranscribeTextToAudioSteps : BaseAudioSteps
    {
        public TranscribeTextToAudioSteps(AudioFileConfig audioFileConfig)
            : base(audioFileConfig) { }

        public override void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsPlayback();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }


        public override async Task Run()
        {
            TakeAudioEngine();

        }
    }
}
