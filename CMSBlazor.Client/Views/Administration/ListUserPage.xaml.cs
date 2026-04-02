using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;
using CommunityToolkit.Maui.Views;
//using DevExpress.Maui.Controls;
using The49.Maui.BottomSheet;

namespace CMSBlazor.Client.Views.Administration;

public partial class ListUserPage : ContentPage
{
    public IEnumerable<Users> Users = new List<Users>();
    AdministrationService AdministrationService = new AdministrationService();
    public UserViewModel? UserViewModel = new UserViewModel();
    public UserID? UserID = new UserID();
    
    
    //=========================PolicyClaims=========================================
    public bool _policyCreate, _policyEdit, _policyDelete;
    public string? _SuperAdmin, _Admin, _User;
    public IEnumerable<Policy> Policies = new List<Policy>();
    public IEnumerable<Role> Role = new List<Role>();
    public PolicyRole? PolicyClaims = new PolicyRole();
    public PolicyRole? PolicyRole = new PolicyRole();
    //==================================================================
    
    public ListUserPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        
        //=========================PolicyClaims=========================================
        PolicyClaims!.UserId = Preferences.Get("UserId", "");
        PolicyClaims = await AdministrationService!.GetClaimsByUser(PolicyClaims!);
        Policies = PolicyClaims.policies!;

        PolicyRole!.UserId = Preferences.Get("UserId", "");
        PolicyRole = await AdministrationService!.GetRolesByUser(PolicyRole!);
        Role = PolicyRole.roles!;

        _policyCreate = Policies.Where(e => e.ClaimType == "Create Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;
        _policyEdit = Policies.Where(e => e.ClaimType == "Edit Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;
        _policyDelete = Policies.Where(e => e.ClaimType == "Delete Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;

        _SuperAdmin = Role.Where(e => e.RoleName == "SuperAdmin").Select(e => e.RoleName).FirstOrDefault();
        _Admin = Role.Where(e => e.RoleName == "Admin").Select(e => e.RoleName).FirstOrDefault();
        _User = Role.Where(e => e.RoleName == "User").Select(e => e.RoleName).FirstOrDefault();
        //==================================================================
        
        Users = (await AdministrationService!.ListUsers()).ToList();
        
        foreach (var users in Users)
        {
            users.IsEnabledEdit = true;
            users.IsEnabledDelete = true;
            if (_policyEdit)
            {
                users.IsEnabledEdit = false;
            }
            if (_policyDelete)
            {
                users.IsEnabledDelete = false;
            }
            Users.Append(users);
        }
        usreslist.ItemsSource = Users;
    }

    async void btnSettings_Clicked(System.Object sender, System.EventArgs e)
    {
        var getbinding = ((Button)sender);
        string userId = (string)getbinding.CommandParameter;
        await Navigation.PushModalAsync(new NavigationPage(new EditUserPage(userId)));

    }

    async void BtnDelete_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        string userId = (string)getbinding.CommandParameter;
        UserID!.UserId = userId;
        UserViewModel = await AdministrationService!.GetEditUser(UserID);
        bool answer = await DisplayAlert("", "Are you Delete "+UserViewModel.UserName, "Yes", "No");
        if (answer)
        {
            var result = await AdministrationService!.DeleteUser(UserID);
            if (result.Successful == false)
            {
                await DisplayAlert("Info Account", $"{result.Message}", "OK");
            }
        }
        
    }
}
