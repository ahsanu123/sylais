using System.Text.Json;
using Xunit.Abstractions;

namespace Sylais.Test.Common;

public static class OutputSerializer
{
    public static void SerializeObject(this ITestOutputHelper output, object obj)
    {
        var jsonString = JsonSerializer.Serialize(
            obj,
            new JsonSerializerOptions { WriteIndented = true }
        );
        output.WriteLine(jsonString);
    }
}
