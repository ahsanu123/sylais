using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using Terminal.Gui.App;
using Terminal.Gui.Configuration;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace Sylais;

public class LoginView : Window, IViewFor<LoginViewModel>
{
    private const string SuccessMessage = "The input is valid!";
    private const string ErrorMessage = "Please enter a valid user name and password.";
    private const string ProgressMessage = "Logging in...";
    private const string IdleMessage = "Press 'Login' to log in.";

    private readonly CompositeDisposable _disposable = [];

    public LoginViewModel ViewModel { get; set; }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (LoginViewModel)value;
    }

    protected override void Dispose(bool disposing)
    {
        _disposable.Dispose();
        base.Dispose(disposing);
    }

    public LoginView(LoginViewModel viewModel)
    {
        Title = $"Reactive Extensions Example - {Application.QuitKey} to Exit";

        ViewModel = viewModel;

        var title = this.AddControl<Label>(x => x.Text = "Login Form");

        var lengthLabel = title.AddControlAfter<Label>(
            (previous, labelUsernameLength) =>
            {
                labelUsernameLength.X = Pos.Left(previous);
                labelUsernameLength.Y = Pos.Top(previous) + 1;

                ViewModel
                    .WhenAnyValue(x => x.UsernameLength)
                    .Select(length => $"_Username ({length} characters):")
                    .BindTo(labelUsernameLength, x => x.Text)
                    .DisposeWith(_disposable);
            }
        );

        lengthLabel.AddControlAfter<TextField>(
            (previous, textFieldUsername) =>
            {
                textFieldUsername.X = Pos.Right(previous) + 1;
                textFieldUsername.Y = Pos.Top(previous);
                textFieldUsername.Width = 40;
                textFieldUsername.Text = ViewModel.Username;

                ViewModel
                    .WhenAnyValue(x => x.Username)
                    .BindTo(textFieldUsername, x => x.Text)
                    .DisposeWith(_disposable);

                textFieldUsername
                    .Events()
                    .TextChanged.Select(_ => textFieldUsername.Text)
                    .DistinctUntilChanged()
                    .BindTo(ViewModel, x => x.Username)
                    .DisposeWith(_disposable);
            }
        );

        lengthLabel
            .AddControlAfter<Label>(
                (previous, labelPasswordLength) =>
                {
                    labelPasswordLength.X = Pos.Left(previous);
                    labelPasswordLength.Y = Pos.Top(previous) + 1;

                    ViewModel
                        .WhenAnyValue(x => x.PasswordLength)
                        .Select(length => $"_Password ({length} characters):")
                        .BindTo(labelPasswordLength, x => x.Text)
                        .DisposeWith(_disposable);
                }
            )
            .AddControlAfter<TextField>(
                (previous, textFieldPassword) =>
                {
                    textFieldPassword.X = Pos.Right(previous) + 1;
                    textFieldPassword.Y = Pos.Top(previous);
                    textFieldPassword.Width = 40;
                    textFieldPassword.Text = ViewModel.Password;

                    ViewModel
                        .WhenAnyValue(x => x.Password)
                        .BindTo(textFieldPassword, x => x.Text)
                        .DisposeWith(_disposable);

                    textFieldPassword
                        .Events()
                        .TextChanged.Select(_ => textFieldPassword.Text)
                        .DistinctUntilChanged()
                        .BindTo(ViewModel, x => x.Password)
                        .DisposeWith(_disposable);
                }
            )
            .AddControlAfter<Label>(
                (previous, labelValidation) =>
                {
                    labelValidation.X = Pos.Left(previous);
                    labelValidation.Y = Pos.Top(previous) + 1;
                    labelValidation.Text = ErrorMessage;

                    ViewModel
                        .WhenAnyValue(x => x.IsValid)
                        .Select(valid => valid ? SuccessMessage : ErrorMessage)
                        .BindTo(labelValidation, x => x.Text)
                        .DisposeWith(_disposable);

                    ViewModel
                        .WhenAnyValue(x => x.IsValid)
                        .Select(valid =>
                            valid
                                ? SchemeManager.GetScheme("Base")
                                : SchemeManager.GetScheme("Error")
                        )
                        .Subscribe(scheme => labelValidation.SetScheme(scheme))
                        .DisposeWith(_disposable);
                }
            )
            .AddControlAfter<Button>(
                (previous, buttonLogin) =>
                {
                    buttonLogin.X = Pos.Left(previous);
                    buttonLogin.Y = Pos.Top(previous) + 1;
                    buttonLogin.Text = "_Login";

                    buttonLogin
                        .Events()
                        .Accepting.InvokeCommand(ViewModel, x => x.Login)
                        .DisposeWith(_disposable);
                }
            )
            .AddControlAfter<Button>(
                (previous, buttonClear) =>
                {
                    buttonClear.X = Pos.Left(previous);
                    buttonClear.Y = Pos.Top(previous) + 1;
                    buttonClear.Text = "_Clear";

                    buttonClear
                        .Events()
                        .Accepting.InvokeCommand(ViewModel, x => x.ClearCommand)
                        .DisposeWith(_disposable);
                }
            )
            .AddControlAfter<Label>(
                (previous, labelProgress) =>
                {
                    labelProgress.X = Pos.Left(previous);
                    labelProgress.Y = Pos.Top(previous) + 1;
                    labelProgress.Width = 40;
                    labelProgress.Height = 1;
                    labelProgress.Text = IdleMessage;

                    ViewModel
                        .WhenAnyObservable(x => x.Login.IsExecuting)
                        .Select(executing => executing ? ProgressMessage : IdleMessage)
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .BindTo(labelProgress, x => x.Text)
                        .DisposeWith(_disposable);
                }
            );
    }
}
