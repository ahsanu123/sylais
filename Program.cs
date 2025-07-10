
// =========================================================
// Basic Terminal.Gui With ReactiveUI
// =========================================================
// using System.Reactive.Concurrency;
// using ReactiveUI;
// using Sylais;
// using Terminal.Gui.App;
// using Terminal.Gui.Configuration;
//
// ConfigurationManager.Enable(ConfigLocations.All);
//
// Application.Init();
//
// RxApp.MainThreadScheduler = TerminalScheduler.Default;
// RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;
//
// Application.Run(new LoginView(new LoginViewModel()));
// Application.Top?.Dispose();
// Application.Shutdown();



using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;

using var audioEngine = new MiniAudioEngine(48000, Capability.Playback, SampleFormat.F32, 2);

// List playback devices
audioEngine.UpdateDevicesInfo();
foreach (var device in audioEngine.PlaybackDevices)
{
    Console.WriteLine($"Device: {device.Name}, Default: {device.IsDefault}");
}
