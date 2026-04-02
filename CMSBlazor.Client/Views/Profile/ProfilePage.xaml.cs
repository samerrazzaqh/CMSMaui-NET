using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Profile;
using CMSBlazor.Shared.ViewModels.Profile;

namespace CMSBlazor.Client.Views.Profile;

public partial class ProfilePage : ContentPage
{
    
    public InfoProfile infoProfile { get; set; } = new InfoProfile();
    
    ProfileService profileService = new ProfileService();
    
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfile();
    }


    public async void LoadProfile()
    {
        // isVisible = false;
        
     
        var result = await profileService!.GetInfoProfile(Preferences.Get("UserId", ""));
        Title = result!.UserName;
        infoProfile.Email = result.Email;
        FirstLastName.Text = result.FirstName + " " + result.LastName;
        infoProfile.LastName = result.LastName;
        infoProfile.Location = result.Location;
        infoProfile.PhoneNumber = result.PhoneNumber;
        infoProfile.Profession = result.Profession;
        infoProfile.DateOfBurthEdite = result.DateOfBurthEdite;
        infoProfile.Skills = result.Skills;

        //=========ImageProfile============
        if (result.UrlImageProfile != null) {
            infoProfile.UrlImageProfile =CMSConstant.BaseApiAddress+ $"/profile/{result.UrlImageProfile}";
            pathImageProfile.Source = infoProfile.UrlImageProfile;
        } else { infoProfile.UrlImageProfile = result.UrlImageProfile; }
            

        //=========ImageCover============
        if (result.UrlImageCover != null)
        {
            infoProfile.UrlImageCover = CMSConstant.BaseApiAddress+ $"/profile/{result.UrlImageCover}";
            pathImageCover.Source = infoProfile.UrlImageCover;
        } else { infoProfile.UrlImageCover = result.UrlImageCover; }



        infoProfile.WorkLink = result.WorkLink;
        infoProfile.Experience = result.Experience;
        infoProfile.UserId = result.UserId;
    }
    
    
    
}