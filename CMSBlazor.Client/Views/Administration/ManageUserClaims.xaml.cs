using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Shared.ViewModels.Administration;

namespace CMSBlazor.Client.Views.Administration;


public partial class ManageUserClaims : ContentPage
{
    AdministrationService AdministrationService = new AdministrationService();
    public UserClaimsViewModel? UserClaimsViewModel = new UserClaimsViewModel();
    public UserClaimsViewModel? EditeUserClaimsViewModel = new UserClaimsViewModel();
    public UserID? UserID = new UserID();
    public List<UserClaim> UserClaims = new List<UserClaim>();
    public List<UserClaim> EditeUserClaims = new List<UserClaim>();
    string _userId,_email;
    public ManageUserClaims(string userId,string email)
    {
        InitializeComponent();
        _userId = userId;
        _email = email;
        LoadData();
    }

    public async void LoadData()
    {
        UserID!.UserId = _userId;
        UserClaimsViewModel = await AdministrationService!.GetManageUserClaims(UserID!);
        lbEmail.Text = _email;
        UserClaims = UserClaimsViewModel.Cliams!;
        userClaimslist.ItemsSource = UserClaims;
    }
    

    public async Task Edite()
    {
        foreach (var list in userClaimslist.ItemsSource)
        {
            EditeUserClaims.Add(list as UserClaim);
        }
        EditeUserClaimsViewModel.Cliams = EditeUserClaims;
        EditeUserClaimsViewModel!.UserId = _userId;
        await AdministrationService!.ManageUserClaims(EditeUserClaimsViewModel!);
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