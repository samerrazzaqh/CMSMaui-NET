using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Account;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Client.Service.Categories;
using CMSBlazor.Client.Service.Profile;
using CMSBlazor.Client.Views.Account;
using CMSBlazor.Client.Views.Administration;
using CMSBlazor.Client.Views.Category;
using CMSBlazor.Client.Views.Post;
using CMSBlazor.Client.Views.Profile;
using CMSBlazor.Shared.ViewModels.Administration;
using CMSBlazor.Shared.ViewModels.Post;
using CMSBlazor.Shared.ViewModels.Profile;
using Syncfusion.Maui.Toolkit.Expander;

namespace CMSBlazor.Client.Views.Home;

public partial class MenuPage : ContentView
{
    AccountService accountService = new AccountService();
    
    IEnumerable<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>();
    CategoryService CategoryService = new CategoryService();
    
    
    AdministrationService AdministrationService = new AdministrationService();
    PolicyRole PolicyRole = new PolicyRole();
    public IEnumerable<Role> Roles = new List<Role>();
    public string? _SuperAdmin, _Admin, _User;
    
    
    InfoProfile infoProfile = new InfoProfile();
    ProfileService ProfileService = new ProfileService();
    
    public MenuPage()
    {
        InitializeComponent();
        LoadAdministration();
        LoadInfoProfile();
        LoadCategories();
    }

    private async void  LoadAdministration()
    {
        try
        {
            PolicyRole!.UserId = Preferences.Get("UserId", "");
            PolicyRole = await AdministrationService!.GetRolesByUser(PolicyRole);
            Roles = PolicyRole.roles!;
            _SuperAdmin = Roles.Where(e => e.RoleName == "SuperAdmin").Select(e => e.RoleName).FirstOrDefault();
            _Admin = Roles.Where(e => e.RoleName == "Admin").Select(e => e.RoleName).FirstOrDefault();
            _User = Roles.Where(e => e.RoleName == "User").Select(e => e.RoleName).FirstOrDefault();
            // Console.WriteLine("Samer  PolicyRole  ----"+_SuperAdmin+_Admin+_User);
            if (_SuperAdmin != null || _Admin != null)
            {
                stSettings.IsVisible = true;
            }
            else
            {
                stSettings.IsVisible = false;
            }
        }
        catch (Exception e)
        {
            // Console.WriteLine("Samer No PolicyRole  ----"+Preferences.Get("UserId", ""));
        }
    }
    
    private async void  LoadInfoProfile()
    {
        try
        {
            var result = await ProfileService!.GetInfoProfile(Preferences.Get("UserId", ""));
            lbNameProfile.Text = result!.UserName;
            imgProfile.ImageSource = CMSConstant.BaseApiAddress+ $"/profile/{result.UrlImageProfile}";
        }
        catch (Exception e)
        {
            Console.WriteLine("Samer No Intenet  ----"+e);
        }
    }
    
    
    private async void  LoadCategories()
    {
        try
        {
            categoryViewModel = (await CategoryService!.GetCategories()).ToList();
            categorylist.ItemsSource = categoryViewModel;
        }
        catch (Exception e)
        {
           Console.WriteLine("Samer No Intenet  ----"+e);
        }
    }

    
    private async void borProfile_Tapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage),true);
    }

    async void borUsers_Tapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListUserPage),true);
    }
    
    async void borRoles_Tapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListRolesPage),true);
    }
    
    async void borPosts_Tapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListPost),true);
    }
    
    async void borCategory_Tapped(object? sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ListCategory),true);
    }
    
    async void BorLogout_OnTapped(object? sender, TappedEventArgs e)
    {
        await accountService.Logout();
        App.Current.MainPage = new LoginPage();
    }

    async void Expander2_OnExpanded(object? sender, ExpandedAndCollapsedEventArgs e)
    {
        if ( categorylist.ItemsSource == null)
        {
            try
            {
                categoryViewModel = (await CategoryService!.GetCategories()).ToList();
                categorylist.ItemsSource = categoryViewModel;
            }
            catch 
            {
                Console.WriteLine("Samer No Intenet  ----");
            }
            Console.WriteLine("Samer No CategoryViewModel  ----");
        }
    }


    async void ButCategoryId_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        int Id = (int)getbinding.CommandParameter;
        string categoryId = Id.ToString();
        // await Shell.Current.GoToAsync(nameof(ListPostByCategory),true);
        await Shell.Current.GoToAsync($"{nameof(ListPostByCategory)}?categoryId={categoryId}", true);
    }
}