using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Comment;
using CMSBlazor.Client.Service.Home;
using CMSBlazor.Client.Service.ReComment;
using CMSBlazor.Shared.ViewModels.CommentLike;

namespace CMSBlazor.Client.Views.Home;

public partial class CommentsPage : ContentPage
{
    HomeService homeService = new HomeService();
    CommentService commentService = new CommentService();
    
    public CommentsViewModel? CommentsViewModel { get; set; } = new CommentsViewModel();
    public InfoComment? InfoCommentss { get; set; } = new InfoComment();
    public IEnumerable<InfoComment> InfoComments = new List<InfoComment>();
    
    long _postId;
    private string _title;
    FormatDate formatDate = new FormatDate();
    public CommentsPage(long postId, string title)
    {
        InitializeComponent();
        _postId = postId;
        _title = title;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        lbPosts.Text = _title;
        await GetComments();
    }


    protected async Task GetComments()
    {
       
        CommentsViewModel!.PostId = _postId;
        CommentsViewModel.GetNumComment = 4;
        CommentsViewModel = await commentService!.ListComment(CommentsViewModel!);
        foreach (var item in CommentsViewModel.InfoComments)
        {
            item.UrlImageAuther =  $"{CMSConstant.BaseApiAddress}{item.UrlImageAuther}";
            item.LCODatestring = formatDate.Date(item.LCODate);
            CommentsViewModel.InfoComments!.Append(item);
        }
        listComments.ItemsSource = CommentsViewModel.InfoComments!;
       
    }
    
    private async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void BtnReComments_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        long commenttId = (long)getbinding.CommandParameter;
        string textComment="";

        foreach (var item in CommentsViewModel.InfoComments)
        {
            if (item.CommentId == commenttId)
            {
                textComment = item.TextComment;
                break;
            }
        }
        await Navigation.PushModalAsync(new NavigationPage(new ReCommentsPage(commenttId,_postId,textComment)));
    }
}