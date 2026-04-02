using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.ReComment;
using CMSBlazor.Shared.ViewModels.CommentLike;

namespace CMSBlazor.Client.Views.Home;

public partial class ReCommentsPage : ContentPage
{
    ReCommentService ReCommentService = new ReCommentService();
    
    public ReComentsViewModel? ReComentsViewModel { get; set; } = new ReComentsViewModel();
    public List<InfoReComment> InfoReComments { get; set; } = new List<InfoReComment>();
    FormatDate formatDate = new FormatDate();
    private long _commenttId, _postId;
    private string _textComment;
    public ReCommentsPage(long commenttId, long postId, string? textComment)
    {
        InitializeComponent();
        _commenttId = commenttId;
        _postId = postId;
        _textComment = textComment;
    }
    


    protected override void OnAppearing()
    {
        base.OnAppearing();
        lbComments.Text = _textComment;
        LoadData();
    }


    public async void LoadData()
    {
        
        ReComentsViewModel!.PostId = _postId;
        ReComentsViewModel!.CommentId = _commenttId;
        ReComentsViewModel.GetNumReComment = 5;
        ReComentsViewModel = await ReCommentService!.ListReComment(ReComentsViewModel!);
       
        foreach (var item in ReComentsViewModel.InfoReComments)
        {
            item.UrlImageAuther =  $"{CMSConstant.BaseApiAddress}{item.UrlImageAuther}";
            item.LCODatestring = formatDate.Date(item.LCODate);
            ReComentsViewModel.InfoReComments!.Append(item);
        }
        listReComments.ItemsSource = ReComentsViewModel.InfoReComments;
    }


    private async void BtnClose_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}