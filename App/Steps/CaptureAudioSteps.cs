using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;
using SoundFlow.Structs;
using Spectre.Console;
using Sylais.Constant;

namespace Sylais.Steps
{
    public class CaptureAudioSteps
    {
        private MiniAudioEngine _audioEngine;
        private DeviceInfo? _currentCaptureDevice;

        public CaptureAudioSteps(MiniAudioEngine miniAudioEngine)
        {
            if (miniAudioEngine.Capability != Capability.Record)
                throw new Exception($"audio engine must able to do {Capability.Record}");
            _audioEngine = miniAudioEngine;
            _currentCaptureDevice = miniAudioEngine.CurrentCaptureDevice;
        }

        public CaptureAudioSteps ChooseCaptureDevice()
        {
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
                "AudioSample",
                "output.wav"
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
    }
}
