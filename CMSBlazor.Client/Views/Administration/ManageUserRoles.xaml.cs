using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;

public partial class ManageUserRoles : ContentPage
{
    AdministrationService AdministrationService = new AdministrationService();
    public UserID? UserID = new UserID();
    public UserRoleViewModel? UserRoleViewModel = new UserRoleViewModel();
    public UserRoleViewModel? EditUserRoleViewModel = new UserRoleViewModel();
    public List<RolesUser> RolesUsers = new List<RolesUser>();
    public List<RolesUser> EditeRolesUsers = new List<RolesUser>();
    
    
    string _userId,_email;
    
    public ManageUserRoles(string userId,string email)
    {
        InitializeComponent();
        _userId = userId;
        _email = email;
        LoadData();
    }

    public async void LoadData()
    {
        UserID!.UserId = _userId;
        UserRoleViewModel = await AdministrationService!.GetManageUserRoles(UserID!);
        lbEmail.Text = _email;
        RolesUsers = UserRoleViewModel.rolesUsers!;
        userRoleslist.ItemsSource = RolesUsers;
    }
    
    
    public async Task Edite()
    {
        foreach (var list in userRoleslist.ItemsSource)
        {
            EditeRolesUsers.Add(list as RolesUser);
        }
        EditUserRoleViewModel.rolesUsers = EditeRolesUsers;
        EditUserRoleViewModel!.UserId = _userId;
        await AdministrationService!.ManageUserRoles(EditUserRoleViewModel!);
    }

    async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    async void BtSubmit_OnClicked(object? sender, EventArgs e)
    {
        await Edite();
        await Navigation.PopModalAsync();
    }
}