using CMSBlazor.Client.Service.Account;
using CMSBlazor.Client.Views.Account;
using CMSBlazor.Client.Views.Administration;
using CMSBlazor.Client.Views.Category;
using CMSBlazor.Client.Views.Home;
using CMSBlazor.Client.Views.Post;
using CMSBlazor.Client.Views.Profile;

namespace CMSBlazor.Client.Views;

public partial class HomeShell : Shell
{
    AccountService accountService = new AccountService();
	public HomeShell()
	{
		InitializeComponent();
        
        Routing.RegisterRoute(nameof(ListUserPage), typeof(ListUserPage));
        Routing.RegisterRoute(nameof(ListPostByCategory), typeof(ListPostByCategory));
        Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
        Routing.RegisterRoute(nameof(ListRolesPage), typeof(ListRolesPage));
        Routing.RegisterRoute(nameof(ListCategory), typeof(ListCategory));
        Routing.RegisterRoute(nameof(ListPost), typeof(ListPost));
        Routing.RegisterRoute(nameof(CreatePost), typeof(CreatePost));
        Routing.RegisterRoute(nameof(EditePost), typeof(EditePost));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(SinglePost), typeof(SinglePost));
        Routing.RegisterRoute(nameof(CommentsPage), typeof(CommentsPage));
        
    }
   
    // async void borProfile_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
    //     await DisplayAlert("Info Account", $"ok", "OK");
    //     FlyoutBehavior = FlyoutBehavior.Disabled;
    //     FlyoutBehavior = FlyoutBehavior.Flyout;
    // }
    //
    // async void borUsers_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
    //     FlyoutBehavior = FlyoutBehavior.Disabled;
    //     FlyoutBehavior = FlyoutBehavior.Flyout;
    //     await Shell.Current.GoToAsync(nameof(ListUserPage),true);
    // }
    //
    // void borRoles_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
    // }
    //
    // void borPosts_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
    // }
    //
    // void borCategory_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    // {
    // }
    //
    // async void BorLogout_OnTapped(object? sender, TappedEventArgs e)
    // {
    //     await accountService.Logout();
    //     App.Current.MainPage = new LoginPage();
    // }
}
