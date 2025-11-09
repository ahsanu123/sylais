using System.Diagnostics;
using System.Text;
using Whisper.net;
using Whisper.net.Ggml;
using Whisper.net.Logger;

namespace Sylais.STT;

internal class WhisperNetConfig
{
    public GgmlType GgmlType { get; set; }
    public string ModelFileName { get; set; }
}

public class WhisperNet
{
    private WhisperNetConfig _config = new WhisperNetConfig
    {
        GgmlType = GgmlType.Base,
        ModelFileName = "ggml-base.bin"
    };

    public async Task<string> RunTest(string wavFileName = "output.wav")
    {
        var stopwatch = Stopwatch.StartNew();

        var modelPath = Path.Combine(Directory.GetCurrentDirectory(), _config.ModelFileName);
        var wavFile = Path.Combine(Directory.GetCurrentDirectory(), wavFileName);

        Console.WriteLine($"Attempt To read model from: {modelPath}");
        Console.WriteLine($"Attempt To read wav file from: {wavFile}");

        using var whisperFactory = WhisperFactory.FromPath(modelPath);
        using var processor = whisperFactory.CreateBuilder()
          .WithLanguage("auto")
          .Build();

        using var fileStream = File.OpenRead(wavFile);

        var text = new StringBuilder();
        await foreach (var result in processor.ProcessAsync(fileStream))
        {
            Console.WriteLine($"{result.Start}->{result.End}: {result.Text}");
            text.Append($" {result.Text}");
        }

        stopwatch.Stop();
        Console.WriteLine("===========================");
        Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

        return text.ToString();
    }
}
