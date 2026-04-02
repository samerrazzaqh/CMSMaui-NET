using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;

public partial class EditUserPage : ContentPage
{
    string _userID,_email;
    AdministrationService AdministrationService = new AdministrationService();
    public UserViewModel? UserViewModel = new UserViewModel();
    public UserID? UserID = new UserID();
    public EditUserPage(string UserId)
    {
        InitializeComponent();
        _userID = UserId;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine("-----_userID------"+_userID);
        UserID!.UserId = _userID;
        UserViewModel = await AdministrationService!.GetEditUser(UserID);
        lbEmail.Text = UserViewModel.Email;
        _email= UserViewModel.Email;
        claimsList.ItemsSource= UserViewModel.Claims;
        RolesList.ItemsSource= UserViewModel.Roles;
        lbBlock.Text = $"Block :  {UserViewModel.UserName}";
        chBlock.IsChecked= UserViewModel.Block;
    }

    
    async void BtSubmit_OnClicked(object? sender, EventArgs e)
    {
        UserViewModel.Block = chBlock.IsChecked;
        await AdministrationService!.EditUser(UserViewModel!);
        await Navigation.PopModalAsync();
    }

    async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    async void PopManageClaims_OnClicked(object? sender, EventArgs e)
    {
        // Dismiss SfPopup from the view.
        await Navigation.PushModalAsync(new NavigationPage(new ManageUserClaims(_userID,_email)));
    }

    async void BtManageRole_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new ManageUserRoles(_userID,_email)));
    }
}