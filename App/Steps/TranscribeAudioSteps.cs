using SoundFlow.Backends.MiniAudio;
using SoundFlow.Structs;
using Sylais.Commands;

namespace Sylais.Steps
{
    public class TranscribeAudioSteps : IBaseStep
    {
        private MiniAudioEngine? _audioEngine;
        private DeviceInfo? _currentCaptureDevice;
        private WhisperCppCommand _whisperCppCommand;

        public TranscribeAudioSteps(WhisperCppCommand whisperCppCommand)
        {
            _whisperCppCommand = whisperCppCommand;
        }

        public void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsPlayback();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public void Run()
        {
            var transcribedText = _whisperCppCommand.Transcribe().GetAwaiter().GetResult();
            Console.WriteLine(transcribedText);

            // TakeAudioEngine();
            // PlayRecordedAudio().Dispose();
        }

        public void Dispose()
        {
            _audioEngine?.Dispose();
        }
    }
}
