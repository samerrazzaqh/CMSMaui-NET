using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Comment;
using CMSBlazor.Client.Service.Home;
using CMSBlazor.Client.Utilities;
using CMSBlazor.Shared.ViewModels.CommentLike;
using CMSBlazor.Shared.ViewModels.Post;

namespace CMSBlazor.Client.Views.Home;

public partial class SinglePost : ContentPage
{
    HomeService homeService = new HomeService();
    CommentService commentService = new CommentService();
    public PostViewModel? PostViewModel { get; set; } = new PostViewModel();
    public IEnumerable<UsersLikePost> UsersLikePosts = new List<UsersLikePost>();
    public PostUtilities? postUtilities { get; set; } = new PostUtilities();
    public CommentsViewModel? CommentsViewModel { get; set; } = new CommentsViewModel();
    long _postId; bool isLike;
    string _Title;
    public SinglePost(long postId)
    {
        InitializeComponent();
        _postId = postId;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetPost();
    }
    
    protected async Task GetPost()
    {
        postUtilities!.PostId = _postId;
        PostViewModel = await homeService!.SinglePost(postUtilities!);
        
        imgPost.Source = $"{CMSConstant.BaseApiAddress}{PostViewModel.PostImg}";
        linkVideo.Source = PostViewModel.LinkVideo;
        lbPostContent.Text = PostViewModel.PostContent;
        lbTitle.Text = PostViewModel.Title;
        _Title = PostViewModel.Title;
        UsersLikePosts = PostViewModel.UsersLikePosts!;
        
        foreach (var item in UsersLikePosts)
        {
            if (item.userId == Preferences.Get("UserId", ""))
            {
                isLike = true;
                break;
            }
            else
            {
                isLike = false;
            }
        }
        if (isLike)
        {
            btnLike.Text = IconFont.Favorite_outline;
        }
        else
        {
            btnLike.Text = IconFont.Favorite;
        }
        
    }

    private async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        linkVideo.Reload();
        await Navigation.PopModalAsync();
    }

    private async void BtnLike_OnClicked(object? sender, EventArgs e)
    {
        CommentsViewModel!.PostId = _postId;
        CommentsViewModel.UserId = Preferences.Get("UserId", "");
        PostViewModel = await commentService!.LikePostCreate(CommentsViewModel!);

        if (isLike)
        {
            btnLike.Text = IconFont.Favorite;
            isLike = false;
        }
        else
        {
            btnLike.Text = IconFont.Favorite_outline;
            isLike = true;
        }
        
        
        
        
    }

    private async void BtnComments_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new CommentsPage(_postId,_Title)));
    }
}