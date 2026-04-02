using CMSBlazor.Client.Service.Account;
using CMSBlazor.Shared.ViewModels.Account;

namespace CMSBlazor.Client.Views.Account;

[QueryProperty(nameof(Email), "email")]
public partial class ResetPasswordbyEmailPage : ContentPage
{
    public string email { set; get; }
    public string Email { set { email = Uri.UnescapeDataString(value); } }
    public ResetPasswordbyEmailViewModel ResetPasswordbyEmailViewModel = new ResetPasswordbyEmailViewModel();
    AccountService accountService = new AccountService();

    public ResetPasswordbyEmailPage()
	{
		InitializeComponent();
	}

    async void btnSubmit_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(etNewPassword.Text.Trim()))
            {
                ErrorMessage();
            }
            else if (string.IsNullOrEmpty(etConfirmNewPassword.Text.Trim()))
            {
                ErrorMessage();
            }
            else if (etNewPassword.Text.Trim() != etConfirmNewPassword.Text.Trim())
            {
                await DisplayAlert("Error Password", "Password and RetypePassword do not match", "OK");
            }
            else
            {
                IndicatorBorder.IsVisible = true;
                ResetPasswordbyEmailViewModel.NewPassword = etNewPassword.Text;
                ResetPasswordbyEmailViewModel.ConfirmNewPassword = etConfirmNewPassword.Text;
                ResetPasswordbyEmailViewModel.Email = email;
                var result = await accountService!.ResetPasswordbyEmail(ResetPasswordbyEmailViewModel);
                if (result.Successful == true)
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
                }
                else
                {
                    await DisplayAlert("Info Account", $"{result.Message}", "OK");
                }
                IndicatorBorder.IsVisible = false;
            }
        }
        catch
        {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        }
    }

    
    public void ErrorMessage() {
        if (string.IsNullOrEmpty(etNewPassword.Text.Trim())) {
            etNewPassword.HasError = true;
            etNewPassword.HelperText = "Enter NewPassword";
            etNewPassword.ErrorText = "Please enter your NewPassword";
            etNewPassword.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etConfirmNewPassword.Text.Trim())) {
            etConfirmNewPassword.HasError = true;
            etConfirmNewPassword.HelperText = "Enter ConfirmNewPassword";
            etConfirmNewPassword.ErrorText = "Please enter your ConfirmNewPassword";
            etConfirmNewPassword.Stroke = Color.FromHex("#B3261E");
        }
    }
    
    private void EntryNewPassword_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etNewPassword.HasError = false;
        etNewPassword.HelperText = "";
        etNewPassword.ErrorText = "";
        etNewPassword.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryConfirmNewPassword_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etConfirmNewPassword.HasError = false;
        etConfirmNewPassword.HelperText = "";
        etConfirmNewPassword.ErrorText = "";
        etConfirmNewPassword.Stroke = Color.FromHex("#776CE6");
    }
}
