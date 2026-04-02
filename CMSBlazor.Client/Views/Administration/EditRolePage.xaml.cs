using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;

public partial class EditRolePage : ContentPage
{
    AdministrationService AdministrationService = new AdministrationService();
    public RoleViewModel? RoleViewModel = new RoleViewModel();
    public EditRoleViewModel? EditRoleViewModel = new EditRoleViewModel();
    
    
    string _roleID;
    public EditRolePage(string roleId)
    {
        InitializeComponent();
        _roleID = roleId;
    }
    
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        RoleViewModel = await AdministrationService!.GetEditRole(_roleID!);
        userroleList.ItemsSource = RoleViewModel.Users;
        lbNameRole.Text = RoleViewModel.RoleName;
        entryRoleName.Text = RoleViewModel.RoleName;
    }
    
    
    async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
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

    async void BtSubmit_OnClicked(object? sender, EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etRoleName.Text.Trim()))
            {
                RoleViewModel.RoleName = etRoleName.Text;
                var result = await AdministrationService!.EditRole(RoleViewModel!);

                if (result!.Successful == false)
                {
                    await DisplayAlert("Info Role", $"{result.Message}", "OK");
                }
                else
                {
                    await Navigation.PopModalAsync();
                }
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

    async void BtAddRemove_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new EditUsersInRole(_roleID)));
    }
}