using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;

public partial class EditUsersInRole : ContentPage
{
    AdministrationService AdministrationService = new AdministrationService();
    public UserRoleViewModel? UserRoleViewModel = new UserRoleViewModel();
    public UserRoleViewModel? EditUserRoleViewModel = new UserRoleViewModel();
    public List<UserRole> UserRoles = new List<UserRole>();
    public List<UserRole> EditUserRoles = new List<UserRole>();
    private string _roleId;
    public EditUsersInRole(string roleId)
    {
        InitializeComponent();
        _roleId = roleId;
    }
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        UserRoleViewModel!.RoleId = _roleId;
        UserRoleViewModel = await AdministrationService!.GetEditUsersInRole(UserRoleViewModel!);
        userRoleslist.ItemsSource = UserRoleViewModel.userRoles!;
    }


    async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    
    public async Task Edite()
    {
        foreach (var list in userRoleslist.ItemsSource)
        {
            EditUserRoles.Add(list as UserRole);
        }
        EditUserRoleViewModel.RoleId = _roleId;
        EditUserRoleViewModel.userRoles = EditUserRoles;
        await AdministrationService!.EditUsersInRole(EditUserRoleViewModel!);
    }
    async void BtSubmit_OnClicked(object? sender, EventArgs e)
    {
        await Edite();
        await Navigation.PopModalAsync();
    }
}