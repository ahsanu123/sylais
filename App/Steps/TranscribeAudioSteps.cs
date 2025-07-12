using Sylais.Commands;
using Sylais.Models;

namespace Sylais.Steps
{
    public class TranscribeAudioSteps : BaseAudioSteps
    {
        private WhisperCppCommand _whisperCppCommand;

        public TranscribeAudioSteps(
            AudioFileConfig audioFileConfig,
            WhisperCppCommand whisperCppCommand
        )
            : base(audioFileConfig)
        {
            _whisperCppCommand = whisperCppCommand;
        }

        public override void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsPlayback();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public override async Task Run()
        {
            var transcribedText = _whisperCppCommand.Transcribe().GetAwaiter().GetResult();
            Console.WriteLine(transcribedText);
        }
    }
}
