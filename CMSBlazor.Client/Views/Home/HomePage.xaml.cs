using CMSBlazor.Client.Views.Administration;

namespace CMSBlazor.Client.Views.Home;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
    }

    // async void Button_Clicked(System.Object sender, System.EventArgs e)
    // {
    //     //Preferences.Default.Remove("Token", "");
    //     //App.Current.MainPage = new AuthShell();
    //     //await Shell.Current.GoToAsync(nameof(ListUserPage), true);
    //     await Shell.Current.Navigation.PushAsync(new ListUserPage());
    // }

    async void BtnForgotPassword_OnClicked(object? sender, EventArgs e)
    {
	    await Shell.Current.Navigation.PushAsync(new ListUserPage());
    }
}
