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

using Microsoft.Extensions.DependencyInjection;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Enums;
using Sylais;
using Sylais.Constant;
using Sylais.Extensions;
using Sylais.Steps;

var services = new ServiceCollection();

services.RegisterServices();

var provider = services.BuildServiceProvider();

using (var recordEngine = AudioEngineManager.Instance.UseAsRecord())
{
    new CaptureAudioSteps(recordEngine).ChooseCaptureDevice().RecordAudio();
}

using (var playEngine = new MiniAudioEngine(AudioConstant.SampleRate, Capability.Playback))
{
    new PlayAudioSteps(playEngine).PlayRecordedAudio();
}
