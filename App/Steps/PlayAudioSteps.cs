using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Providers;
using SoundFlow.Structs;
using Sylais.Models;

namespace Sylais.Steps
{
    public class PlayAudioSteps : IBaseStep
    {
        private MiniAudioEngine? _audioEngine;
        private DeviceInfo? _currentCaptureDevice;
        private AudioFileConfig _audioConfig;

        public PlayAudioSteps(AudioFileConfig audioFileConfig)
        {
            _audioConfig = audioFileConfig;
        }

        public void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsPlayback();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public PlayAudioSteps PlayRecordedAudio()
        {
            if (_audioEngine == null)
                throw new Exception("Call TakeAudioEngine First");

            Console.WriteLine("Playing Recorded Audio");

            var outputFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                _audioConfig.FolderName,
                _audioConfig.FileName
            );
            using var dataProvider = new StreamDataProvider(File.OpenRead(outputFilePath));
            var audioPlayer = new SoundPlayer(dataProvider);

            Mixer.Master.AddComponent(audioPlayer);

            audioPlayer.Play();

            Console.WriteLine($"{audioPlayer.State}...");

            while (audioPlayer.State == PlaybackState.Playing)
            {
                if (Console.KeyAvailable)
                {
                    audioPlayer.Stop();
                    break;
                }

                Thread.Sleep(100);
            }

            if (audioPlayer.State != PlaybackState.Stopped)
                audioPlayer.Stop();

            Mixer.Master.RemoveComponent(audioPlayer);

            return this;
        }

        public void Run()
        {
            TakeAudioEngine();
            PlayRecordedAudio().Dispose();
        }

        public void Dispose()
        {
            _audioEngine?.Dispose();
        }
    }
}
