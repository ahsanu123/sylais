using System.Reactive.Concurrency;
using ReactiveUI;
using Sylais;
using Terminal.Gui.App;
using Terminal.Gui.Configuration;

ConfigurationManager.Enable(ConfigLocations.All);

Application.Init();

RxApp.MainThreadScheduler = TerminalScheduler.Default;
RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

Application.Run(new LoginView(new LoginViewModel()));
Application.Top?.Dispose();
Application.Shutdown();
