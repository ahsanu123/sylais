using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;
using SoundFlow.Structs;
using Spectre.Console;
using Sylais.Constant;
using Sylais.Models;

namespace Sylais.Steps
{
    public class CaptureAudioSteps : IBaseStep
    {
        private MiniAudioEngine? _audioEngine;
        private DeviceInfo? _currentCaptureDevice;
        private AudioFileConfig _audioConfig;

        public CaptureAudioSteps(AudioFileConfig audioFileConfig)
        {
            _audioConfig = audioFileConfig;
        }

        public void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsRecord();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public CaptureAudioSteps ChooseCaptureDevice()
        {
            if (_audioEngine == null)
                throw new Exception("Call TakeAudioEngine First");

            var inputDevices = _audioEngine.CaptureDevices;
            var inputDevicePrompt = new SelectionPrompt<DeviceInfo>()
                .Title("Choose Capture Device!!")
                .PageSize(5)
                .AddChoices(inputDevices);

            inputDevicePrompt.Converter = (deviceInfo) =>
                $"{deviceInfo.Name}, Default Device: {deviceInfo.IsDefault}";

            var selectedInputDevice = AnsiConsole.Prompt(inputDevicePrompt);

            var currentCaptureDevice = _audioEngine.CurrentCaptureDevice;

            if (currentCaptureDevice != null && !currentCaptureDevice.Equals(selectedInputDevice!))
            {
                _audioEngine.SwitchDevice(selectedInputDevice, DeviceType.Capture);
                _currentCaptureDevice = selectedInputDevice;
            }

            return this;
        }

        public CaptureAudioSteps RecordAudio()
        {
            var outputFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                _audioConfig.FolderName,
                _audioConfig.FileName
            );

            using var fileStream = new FileStream(
                outputFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None
            );

            using var recorder = new SoundFlow.Components.Recorder(
                fileStream,
                sampleRate: AudioConstant.SampleRate,
                encodingFormat: EncodingFormat.Wav
            );

            Console.WriteLine("Recording... Press any key to stop.");
            recorder.StartRecording();

            Console.ReadKey();
            recorder.StopRecording();

            Console.WriteLine($"Recording stopped. Saved to {outputFilePath}");
            return this;
        }

        public void Run()
        {
            TakeAudioEngine();
            ChooseCaptureDevice().RecordAudio().Dispose();
        }

        public void Dispose()
        {
            _audioEngine?.Dispose();
        }
    }
}
