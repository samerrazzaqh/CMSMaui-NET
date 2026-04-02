using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Categories;
using CMSBlazor.Client.Service.Posts;
using CMSBlazor.Client.Service.Profile;
using CMSBlazor.Shared.ViewModels.Post;
using CMSBlazor.Shared.ViewModels.Profile;

namespace CMSBlazor.Client.Views.Post;

public partial class CreatePost : ContentPage
{
    
    public CategoryService? categoryService = new CategoryService();
    public PostService? postService = new PostService();
    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    public CategoryViewModel Category { get; set; } = new CategoryViewModel();
    public PostViewModel postViewModel = new PostViewModel();
    
    
    
    public CreatePost()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        Categories = (await categoryService!.GetCategories())!.ToList();
        pickerCategory.ItemsSource = Categories;
        pickerCategory.SelectedItem = Categories.Where(p => p.CategoryId == 1).FirstOrDefault();
    }
    
    
    public async Task UploadImageCover()
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image Please",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
                return;
            
            var stream = await result!.OpenReadAsync();
            byte[] buffer;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                buffer = ms.ToArray();
            }
            postViewModel.PostImg = Convert.ToBase64String(buffer);
            postViewModel.PostImgName = result.FileName;
            
            var imageBytes = Convert.FromBase64String(Convert.ToBase64String(buffer));
            MemoryStream imageDecodeStream = new(imageBytes);
            imagePost.Source = ImageSource.FromStream(() => imageDecodeStream);
            
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
    }

    
    
    public async Task HandleCreate()
    {
        Category = (CategoryViewModel)pickerCategory.ItemsSource[pickerCategory.SelectedIndex]!;

        postViewModel.Title = etTitle.Text;
        postViewModel.PostContent = etPostContent.Text;
        postViewModel.LinkVideo = etLinkVideo.Text;
        postViewModel.IsPublish = chBlock.IsChecked;
        postViewModel.Auther = Preferences.Get("AboutUserId", "");
        postViewModel.CategoryId = Category.CategoryId;

        if (!string.IsNullOrEmpty(postViewModel.Title)&& !string.IsNullOrEmpty(postViewModel.PostContent)&& !string.IsNullOrEmpty(postViewModel.PostImg)
            && !string.IsNullOrEmpty(postViewModel.LinkVideo))
        {
            var result = await postService!.CreatePost(postViewModel);
            if (result.Successful)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Info Post", $"{result.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Info Post", $"Fill in the fields", "OK");
        }
    }
    public async void BtCreate_OnClicked(object? sender, EventArgs e)
    {
        HandleCreate();
    }
    
    async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }


    private async void BtnUploadimg_OnClicked(object? sender, EventArgs e)
    {
        UploadImageCover();
    }

}