using CMSBlazor.Client.Service.Account;
using CMSBlazor.Shared.ViewModels.Account;

namespace CMSBlazor.Client.Views.Account;

public partial class RegisterPage : ContentPage
{
    AccountService accountService = new AccountService();
    public RegisterViewModel registerViewModel = new RegisterViewModel();
    public RegisterPage()
	{
		InitializeComponent();
	}
    
    async void btnRegister_Clicked(System.Object sender, System.EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etFirstName.Text.Trim()) || !string.IsNullOrEmpty(etLastName.Text.Trim())
                  || !string.IsNullOrEmpty(etEmail.Text.Trim()) || !string.IsNullOrEmpty(etUserName.Text.Trim())
                  || !string.IsNullOrEmpty(etPassword.Text.Trim()) || !string.IsNullOrEmpty(etRePassword.Text.Trim()))
            { 
                 if (entryPassword.Text.Trim() != entryRePassword.Text.Trim())
                 {
                     await DisplayAlert("Error Password", "Password and RetypePassword do not match", "OK");
                 }
                 else if (!string.IsNullOrEmpty(etFirstName.Text.Trim()) && !string.IsNullOrEmpty(etLastName.Text.Trim()) && 
                          !string.IsNullOrEmpty(etEmail.Text.Trim()) && !string.IsNullOrEmpty(etUserName.Text.Trim())
                          || !string.IsNullOrEmpty(etPassword.Text.Trim()) && !string.IsNullOrEmpty(etRePassword.Text.Trim()))
                 {
                     IndicatorBorder.IsVisible = true;
                     registerViewModel.FirstName = entryFirstName.Text;
                     registerViewModel.LastName = entryLastName.Text;
                     registerViewModel.Email = entryEmail.Text;
                     registerViewModel.UserName = entryUserName.Text;
                     registerViewModel.Password = entryPassword.Text;
                     registerViewModel.ConfirmPassword = entryRePassword.Text;
                     var result = await accountService!.Register(registerViewModel);
                     if (result.Successful == true) {
                         await Shell.Current.GoToAsync($"{nameof(ConfirmEmailPage)}?email={registerViewModel.Email}", true);
                     }
                     else {
                         await DisplayAlert("Info Account", $"{result.Message}", "OK");
                     }
                     IndicatorBorder.IsVisible = false;
                 }
            }
            if (string.IsNullOrEmpty(etFirstName.Text.Trim()) || string.IsNullOrEmpty(etLastName.Text.Trim()) ||
                string.IsNullOrEmpty(etEmail.Text.Trim()) || string.IsNullOrEmpty(etUserName.Text.Trim()) ||
                string.IsNullOrEmpty(etPassword.Text.Trim()) || string.IsNullOrEmpty(etRePassword.Text.Trim()))
            {
                ErrorMessage();
            }
        }
        catch {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        }
    }
// etFirstName etLastName etEmail etUserName etPassword  etRePassword
// entryFirstName entryLastName entryEmail entryUserName entryPassword  entryRePassword

    public void ErrorMessage() {
        if (string.IsNullOrEmpty(etFirstName.Text.Trim())) {
            etFirstName.HasError = true;
            etFirstName.HelperText = "Enter FirstName";
            etFirstName.ErrorText = "Please enter your FirstName";
            etFirstName.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etLastName.Text.Trim())) {
            etLastName.HasError = true;
            etLastName.HelperText = "Enter LastName";
            etLastName.ErrorText = "Please enter your LastName";
            etLastName.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etEmail.Text.Trim())) {
            etEmail.HasError = true;
            etEmail.HelperText = "Enter Email";
            etEmail.ErrorText = "Please enter your Email";
            etEmail.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etUserName.Text.Trim())) {
            etUserName.HasError = true;
            etUserName.HelperText = "Enter UserName";
            etUserName.ErrorText = "Please enter your UserName";
            etUserName.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etPassword.Text.Trim()))
        {
            etPassword.HasError = true;
            etPassword.HelperText = "Enter your Password";
            etPassword.ErrorText = "Please enter your Password";
            etPassword.Stroke = Color.FromHex("#B3261E");
        }
        if (string.IsNullOrEmpty(etRePassword.Text.Trim()))
        {
            etRePassword.HasError = true;
            etRePassword.HelperText = "Enter your RePassword";
            etRePassword.ErrorText = "Please enter your RePassword";
            etRePassword.Stroke = Color.FromHex("#B3261E");
        }
    }


    private void EntryFirstName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etFirstName.HasError = false;
        etFirstName.HelperText = "";
        etFirstName.ErrorText = "";
        etFirstName.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryLastName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etLastName.HasError = false;
        etLastName.HelperText = "";
        etLastName.ErrorText = "";
        etLastName.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryEmail_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etEmail.HasError = false;
        etEmail.HelperText = "";
        etEmail.ErrorText = "";
        etEmail.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryUserName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etUserName.HasError = false;
        etUserName.HelperText = "";
        etUserName.ErrorText = "";
        etUserName.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryPassword_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etPassword.HasError = false;
        etPassword.HelperText = "";
        etPassword.ErrorText = "";
        etPassword.Stroke = Color.FromHex("#776CE6");
    }

    private void EntryRePassword_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etRePassword.HasError = false;
        etRePassword.HelperText = "";
        etRePassword.ErrorText = "";
        etRePassword.Stroke = Color.FromHex("#776CE6");
    }
}
