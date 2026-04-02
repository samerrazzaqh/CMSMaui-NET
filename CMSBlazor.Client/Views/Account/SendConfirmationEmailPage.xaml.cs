using CMSBlazor.Client.Service.Account;
using CMSBlazor.Shared.ViewModels.Account;

namespace CMSBlazor.Client.Views.Account;

public partial class SendConfirmationEmailPage : ContentPage
{
    public SendTokenEmail SendTokenEmail = new SendTokenEmail();
    AccountService accountService = new AccountService();
    public SendConfirmationEmailPage()
	{
		InitializeComponent();
	}

    async void btnSubmit_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(etEmail.Text.Trim()))
            {
               ErrorMessage();
            }
            else
            {
                IndicatorBorder.IsVisible = true;
                SendTokenEmail.Email = etEmail.Text;
                var result = await accountService!.SendConfirmationEmail(SendTokenEmail);
                if (result.Successful == true)
                {
                    await Shell.Current.GoToAsync($"{nameof(ConfirmEmailPage)}?email={SendTokenEmail.Email}", true);
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
        if (string.IsNullOrEmpty(etEmail.Text.Trim())) {
            etEmail.HasError = true;
            etEmail.HelperText = "Enter Email";
            etEmail.ErrorText = "Please enter your Email";
            etEmail.Stroke = Color.FromHex("#B3261E");
        }
    }
    private void EntryEmail_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etEmail.HasError = false;
        etEmail.HelperText = "";
        etEmail.ErrorText = "";
        etEmail.Stroke = Color.FromHex("#776CE6");
    }
}
