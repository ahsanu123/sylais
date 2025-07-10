using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;
using SoundFlow.Components;
using SoundFlow.Providers;

namespace Sylais.Steps
{
    public class PlayAudioSteps
    {
        private MiniAudioEngine _audioEngine;

        public PlayAudioSteps(MiniAudioEngine miniAudioEngine)
        {
            if (miniAudioEngine.Capability != Capability.Playback)
                throw new Exception($"audio engine must able to do {Capability.Playback}");
            _audioEngine = miniAudioEngine;
        }

        public PlayAudioSteps PlayRecordedAudio()
        {
            Console.WriteLine("Playing Recorded Audio");

            var outputFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "AudioSample",
                "output.wav"
            );
            using var dataProvider = new StreamDataProvider(File.OpenRead(outputFilePath));
            var audioPlayer = new SoundPlayer(dataProvider);

            Mixer.Master.AddComponent(audioPlayer);

            audioPlayer.Play();

            Console.WriteLine("Playing audio... Press any key to stop.");
            Console.ReadKey();

            audioPlayer.Stop();

            Mixer.Master.RemoveComponent(audioPlayer);
            return this;
        }

    }
}
