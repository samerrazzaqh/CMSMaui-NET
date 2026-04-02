using CMSBlazor.Client.Service.Account;
using CMSBlazor.Client.Views.Home;
using CMSBlazor.Shared.ViewModels.Account;

namespace CMSBlazor.Client.Views.Account;

public partial class LoginPage : ContentPage
{
    AccountService accountService = new AccountService();
    public LoginViewModel loginModel = new LoginViewModel();
    AppTheme currentTheme = Application.Current.RequestedTheme;
    public LoginPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    async void btnLogin_Clicked(System.Object sender, System.EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etEmailorUserName.Text.Trim()) || !string.IsNullOrEmpty(etPassword.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(etEmailorUserName.Text.Trim()) && !string.IsNullOrEmpty(etPassword.Text.Trim()))
                {
                    IndicatorBorder.IsVisible = true;
                    loginModel.EmailOrUserName = etEmailorUserName.Text;
                    loginModel.Password = etPassword.Text;
                    var result = await accountService!.Login(loginModel);
                    if (result.Successful == true) {
                        App.Current.MainPage = new HomeShell();
                    }
                    else {
                        await DisplayAlert("Info Account", $"{result.Message}", "OK");
                    }
                    IndicatorBorder.IsVisible = false;
                }

            }
            if (string.IsNullOrEmpty(etEmailorUserName.Text.Trim()) || string.IsNullOrEmpty(etPassword.Text.Trim()))
            {
                ErrorMessage();
            }
        }
        catch {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        }
    }

    public void ErrorMessage() {
        if (string.IsNullOrEmpty(etEmailorUserName.Text.Trim())) {
            etEmailorUserName.HasError = true;
            etEmailorUserName.HelperText = "Enter your email address";
            etEmailorUserName.ErrorText = "Please enter your email or username";
            etEmailorUserName.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etPassword.Text.Trim()))
        {
            etPassword.HasError = true;
            etPassword.HelperText = "Enter your Password";
            etPassword.ErrorText = "Please enter your Password";
            etPassword.Stroke = Color.FromHex("#B3261E");
        }
    }
    

    async void btnRegister_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage),true);
    }

    async void btnConfirmationEmail_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SendConfirmationEmailPage), true);
    }

    async void btnForgotPassword_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ForgotPasswordbyEmailPage), true);
    }

    private void EntryEmailorUserName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etEmailorUserName.HasError = false;
        etEmailorUserName.HelperText = "";
        etEmailorUserName.ErrorText = "";
        etEmailorUserName.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryPassword_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etPassword.HasError = false;
        etPassword.HelperText = "";
        etPassword.ErrorText = "";
        etPassword.Stroke = Color.FromHex("#776CE6");
    }
}
