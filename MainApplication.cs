using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui.Configuration;
using Terminal.Gui.App;
using Sylais.Windows;

namespace Sylais;

public static class MainApplication
{
    public static void Run(IHost app)
    {
        // using (var serviceScope = app.Services.CreateScope())
        // {
        // var exampleWindow = app.Services.GetKeyedService

        // Override the default configuration for the application to use the Light theme
        ConfigurationManager.RuntimeConfig = """{ "Theme": "Dark" }""";
        ConfigurationManager.Enable(ConfigLocations.All);

        Application.Run<ExampleWindow>().Dispose();

        // Before the application exits, reset Terminal.Gui for clean shutdown
        Application.Shutdown();

        // }

    }
}
