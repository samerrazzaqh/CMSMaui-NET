using CMSBlazor.Client.Views;
using CMSBlazor.Client.Views.Home;

namespace CMSBlazor.Client;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        bool t = false;
        string token = Preferences.Default.Get("Token", "");
        if (token != string.Empty) {
            MainPage = new HomeShell();
        }
        else {
            MainPage = new AuthShell();
        }

        
    }
}

