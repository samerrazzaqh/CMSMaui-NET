using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Categories;
using CMSBlazor.Client.Service.Posts;
using CMSBlazor.Shared.ViewModels.Post;

namespace CMSBlazor.Client.Views.Post;

public partial class EditePost : ContentPage
{
    public CategoryService? categoryService = new CategoryService();
    public PostService? postService = new PostService();
    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    public CategoryViewModel Category { get; set; } = new CategoryViewModel();
    public PostViewModel? postViewModel { get; set; } = new PostViewModel();
    public PostViewModel? editpostViewModel { get; set; } = new PostViewModel();
    public PostUtilities? postUtilities { get; set; } = new PostUtilities();
    
    public string? pathImage,pathImageName;
    private long _postId;
    public EditePost(long postId)
    {
        InitializeComponent();
        _postId = postId;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        
        postUtilities!.PostId =_postId;
        
        postViewModel = await postService!.GetEditPost(postUtilities);
        imagePost.Source =CMSConstant.BaseApiAddress+ $"/post/{postViewModel!.PostImg}" ;
        entryTitle.Text = postViewModel.Title;
        entryPostContent.Text = postViewModel.PostContent;
        entryLinkVideo.Text = postViewModel.LinkVideo;
        chBlock.IsChecked = postViewModel.IsPublish;
        
        Categories = (await categoryService!.GetCategories())!.ToList();
        pickerCategory.ItemsSource = Categories;
        pickerCategory.SelectedItem = Categories.Where(p => p.CategoryId == postViewModel.CategoryId).FirstOrDefault();

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
            pathImage = Convert.ToBase64String(buffer);
            pathImageName = result.FileName;
            
            var imageBytes = Convert.FromBase64String(Convert.ToBase64String(buffer));
            MemoryStream imageDecodeStream = new(imageBytes);
            imagePost.Source = ImageSource.FromStream(() => imageDecodeStream);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
        
    }
    
    
    public async Task HandleEdit()
    {
        Category = (CategoryViewModel)pickerCategory.ItemsSource[pickerCategory.SelectedIndex]!;

        editpostViewModel.PostId = _postId;
        editpostViewModel.Title = etTitle.Text;
        editpostViewModel.PostContent = etPostContent.Text;
        editpostViewModel.LinkVideo = etLinkVideo.Text;
        editpostViewModel.IsPublish = chBlock.IsChecked;
        editpostViewModel.CategoryId = Category.CategoryId;
        if (pathImage == null)
        {
            editpostViewModel.PostImg = null;
        }
        else
        {
            editpostViewModel.PostImg = pathImage;
            editpostViewModel.PostImgName = pathImageName;
        }
        

        if (!string.IsNullOrEmpty(editpostViewModel.Title)&& !string.IsNullOrEmpty(editpostViewModel.PostContent)&& !string.IsNullOrEmpty(editpostViewModel.LinkVideo))
        {
            var result = await postService!.EditPost(editpostViewModel!);
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
    
    
    private async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void BtUpdate_OnClicked(object? sender, EventArgs e)
    {
        HandleEdit();
    }

    private void BtnUploadimg_OnClicked(object? sender, EventArgs e)
    {
        UploadImageCover();
    }
}