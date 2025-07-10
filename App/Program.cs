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
using Sylais.Extensions;
using Sylais.Steps;

var services = new ServiceCollection();

services.RegisterConfigurationServices().RegisterServices();

var provider = services.BuildServiceProvider();

provider.RunStep<CaptureAudioSteps>().RunStep<PlayAudioSteps>().RunStep<TranscribeAudioSteps>();
