using CMSBlazor.Client.Service.Account;
using CMSBlazor.Shared.ViewModels.Account;

namespace CMSBlazor.Client.Views.Account;

[QueryProperty(nameof(Email), "email")]
public partial class PasswordConfirmEmailPage : ContentPage
{
    public string email { set; get; }
    public string Email { set { email = Uri.UnescapeDataString(value); } }
    public ConfirmEmailViewModel PasswordConfirmEmailViewModel = new ConfirmEmailViewModel();
    AccountService accountService = new AccountService();
    public PasswordConfirmEmailPage()
	{
		InitializeComponent();
	}

    async void btnSubmit_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(etToken.Text.Trim()))
            {
               ErrorMessage();
            }
            else
            {
                IndicatorBorder.IsVisible = true;
                PasswordConfirmEmailViewModel.TokenFromEmail = etToken.Text;
                PasswordConfirmEmailViewModel.EmailOrUserName = email;
                var result = await accountService!.PasswordConfirmEmail(PasswordConfirmEmailViewModel);
                if (result.Successful == true)
                {
                    await Shell.Current.GoToAsync($"{nameof(ResetPasswordbyEmailPage)}?email={PasswordConfirmEmailViewModel.EmailOrUserName}", true);
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
        if (string.IsNullOrEmpty(etToken.Text.Trim())) {
            etToken.HasError = true;
            etToken.HelperText = "Enter Code";
            etToken.ErrorText = "Please enter your Code";
            etToken.Stroke = Color.FromHex("#B3261E");
        }
    }
    private void EntryToken_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etToken.HasError = false;
        etToken.HelperText = "";
        etToken.ErrorText = "";
        etToken.Stroke = Color.FromHex("#776CE6");
    }
}
