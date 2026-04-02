using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;

public partial class ListRolesPage : ContentPage
{
    public IEnumerable<Roles> Roles = new List<Roles>();
    AdministrationService AdministrationService = new AdministrationService();
    public CreateRoleViewModel CreateRoleViewModel = new CreateRoleViewModel();
    public RoleViewModel? RoleViewModel = new RoleViewModel();
    
    
    
    //=========================PolicyClaims=========================================
    public bool _policyCreate, _policyEdit, _policyDelete;
    public string? _SuperAdmin, _Admin, _User;
    public IEnumerable<Policy> Policies = new List<Policy>();
    public IEnumerable<Role> Role = new List<Role>();
    public PolicyRole? PolicyClaims = new PolicyRole();
    public PolicyRole? PolicyRole = new PolicyRole();
    //==================================================================
    
    public ListRolesPage()
    {
        InitializeComponent();
        // _Policy();
    }
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("((((  "+Preferences.Get("Token", "")+"  )))");
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

        if (_policyCreate)
        {
            gridCreateRole.IsEnabled = false;
        }
        
        
        Roles = (await AdministrationService!.ListRoles()).ToList();
        foreach (var role in Roles)
        {
            role.IsEnabledEdit = true;
            role.IsEnabledDelete = true;
            if (_policyEdit)
            {
                role.IsEnabledEdit = false;
            }
            if (_policyDelete)
            {
                role.IsEnabledDelete = false;
            }
            Roles.Append(role);
        }
        roleslist.ItemsSource = Roles;
    }

    //==================================================================

    async void BtnEdite_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        string roleId = (string)getbinding.CommandParameter;
        await Navigation.PushModalAsync(new NavigationPage(new EditRolePage(roleId)));
    }

    async void BtnDelete_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        string roleId = (string)getbinding.CommandParameter;
        RoleViewModel = await AdministrationService!.GetEditRole(roleId!);
        RoleViewModel!.Id = roleId;
        bool answer = await DisplayAlert("", "Are you Delete "+RoleViewModel.RoleName, "Yes", "No");
        if (answer)
        {
            var result =  await AdministrationService!.DeleteRole(RoleViewModel);
            if (result.Successful == false)
            {
                await DisplayAlert("Info Role", $"{result.Message}", "OK");
            }
            else
            {
                Roles = (await AdministrationService!.ListRoles()).ToList();
                roleslist.ItemsSource = Roles;
            }
        }
    }

    async void BtCreate_OnClicked(object? sender, EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etRoleName.Text.Trim()))
            {
                CreateRoleViewModel.RoleName = etRoleName.Text;
                var result = await AdministrationService!.CreateRole(CreateRoleViewModel);
                if (result!.Successful == false)
                {
                    await DisplayAlert("Info Role", $"{result.Message}", "OK");
                }
                else
                {
                    Roles = (await AdministrationService!.ListRoles()).ToList();
                    roleslist.ItemsSource = Roles;
                }
                entryRoleName.Text = string.Empty;
            }
            else
            {
                ErrorMessage();
            }
        }
        catch {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        }
    }

    async void EntryRoleName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etRoleName.HasError = false;
        etRoleName.HelperText = "";
        etRoleName.ErrorText = "";
        etRoleName.Stroke = Color.FromHex("#776CE6");
    }
    
    
    public void ErrorMessage() {
        if (string.IsNullOrEmpty(etRoleName.Text.Trim())) {
            etRoleName.HasError = true;
            etRoleName.HelperText = "Enter Role Name";
            etRoleName.ErrorText = "Please enter Role Name";
            etRoleName.Stroke = Color.FromHex("#B3261E");
        }
    }
}