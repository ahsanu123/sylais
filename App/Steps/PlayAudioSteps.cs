using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Providers;
using Sylais.Models;

namespace Sylais.Steps
{
    public class PlayAudioSteps : BaseAudioSteps
    {
        public PlayAudioSteps(AudioFileConfig audioFileConfig)
            : base(audioFileConfig) { }

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

        public override void TakeAudioEngine()
        {
            _audioEngine = AudioEngineManager.Instance.UseAsPlayback();
            _currentCaptureDevice = _audioEngine.CurrentCaptureDevice;
        }

        public override async Task Run()
        {
            TakeAudioEngine();
            PlayRecordedAudio().Dispose();
        }
    }
}
