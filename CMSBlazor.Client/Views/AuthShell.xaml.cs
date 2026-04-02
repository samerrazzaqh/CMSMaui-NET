using CMSBlazor.Client.Views.Account;
using CMSBlazor.Client.Views.Home;

namespace CMSBlazor.Client.Views;

public partial class AuthShell : Shell
{
	public AuthShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(ConfirmEmailPage), typeof(ConfirmEmailPage));
        Routing.RegisterRoute(nameof(SendConfirmationEmailPage), typeof(SendConfirmationEmailPage));
        Routing.RegisterRoute(nameof(ForgotPasswordbyEmailPage), typeof(ForgotPasswordbyEmailPage));
        Routing.RegisterRoute(nameof(PasswordConfirmEmailPage), typeof(PasswordConfirmEmailPage));
        Routing.RegisterRoute(nameof(ResetPasswordbyEmailPage), typeof(ResetPasswordbyEmailPage));
        
    }
}
