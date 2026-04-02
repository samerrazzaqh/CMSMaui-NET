using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Client.Service.Home;
using CMSBlazor.Shared.ViewModels.Post;
using MvvmHelpers;

namespace CMSBlazor.Client.Views.Home;

public partial class MainPosts : ContentView
{
    HomeService homeService = new HomeService();
    public IEnumerable<PostViewModel> PostViewModels = new List<PostViewModel>();
    public PostUtilities? postUtilities = new PostUtilities();
    public PostAll? PostAllC,PostAllP,PostAllS = new PostAll();
    public ObservableRangeCollection<PostViewModel> ObservPostViewModelP { get; set; } =
        new ObservableRangeCollection<PostViewModel>();
    public ObservableRangeCollection<PostViewModel> ObservPostViewModelS { get; set; } =
        new ObservableRangeCollection<PostViewModel>();
    public ObservableCollection<PostViewModel> CarouselModel { get; set; } = new();
    public IEnumerable<PostViewModel> PostsModel,StoryModel = new List<PostViewModel>();
    public MainPosts()
    {
        InitializeComponent();
        LoadData();
    }

    private async Task LoadData()
    {
        await Carousel();
        await Storis();
        await Posts();
    }

    private async Task Storis()
    {
        postUtilities!.NumGetPost = 15;
        PostAllS = await homeService!.ListPost(postUtilities);
        StoryModel = PostAllS.PostViewModels;
        foreach (var item in StoryModel)
        {
            item.PostImg =  $"{CMSConstant.BaseApiAddress}{item.PostImg}";
            StoryModel.Append(item);
        }
        ObservPostViewModelS.AddRange(StoryModel!.Take(postUtilities!.NumGetPost));
        storyList.ItemsSource = ObservPostViewModelS;
    }
    
    private async Task Posts()
    {
        postUtilities!.NumGetPost = 10;
        PostAllP = await homeService!.ListPost(postUtilities);
        PostsModel =  PostAllP.PostViewModels;
        foreach (var item in PostsModel)
        {
            item.PostImg =  $"{CMSConstant.BaseApiAddress}{item.PostImg}";
            PostsModel.Append(item);
        }
        ObservPostViewModelP.AddRange(PostsModel!.Take(postUtilities!.NumGetPost));
        postsList.ItemsSource = ObservPostViewModelP;
    }

    public async Task Carousel()
    {
        postUtilities!.NumGetPost = 3;
        PostAllC = await homeService!.ListPost(postUtilities);
        CarouselModel = new ObservableCollection<PostViewModel>(PostAllC.PostViewModels);
        foreach (var item in CarouselModel)
        {
            item.PostImg =  $"{CMSConstant.BaseApiAddress}{item.PostImg}";
            CarouselModel.Append(item);
        }
        carouselPost.ItemsSource = CarouselModel;
    }


    private async void ListPost_OnRemainingItemsThresholdReached(object? sender, EventArgs e)
    {
        postUtilities!.NumGetPost += 10;
        PostAllP = await homeService!.ListPost(postUtilities);
        PostsModel = PostAllP.PostViewModels!;
        foreach (var item in PostsModel)
        {
            item.PostImg =  $"{CMSConstant.BaseApiAddress}{item.PostImg}";
            PostsModel.Append(item);
        }
            
        if (PostAllP.PostsCount == PostsModel.Count())
        {
            IndicatorLoadMore.IsRunning = true;
            if (ObservPostViewModelP.Count > 0)
            {
                ObservPostViewModelP.AddRange(PostsModel!.Skip(ObservPostViewModelP.Count).Take(10));
            }
            IndicatorLoadMore.IsRunning = false;
        }
    }


    private async void StoryPost_OnRemainingItemsThresholdReached(object? sender, EventArgs e)
    {
        postUtilities!.NumGetPost += 10;
        PostAllS = await homeService!.ListPost(postUtilities);
        StoryModel = PostAllS.PostViewModels;
        foreach (var item in StoryModel)
        {
            item.PostImg =  $"{CMSConstant.BaseApiAddress}{item.PostImg}";
            StoryModel.Append(item);
        }
            
        if (PostAllS.PostsCount == StoryModel.Count())
        {
            IndicatorLoadMore.IsRunning = true;
            if (ObservPostViewModelS.Count > 0)
            {
                ObservPostViewModelS.AddRange(StoryModel!.Skip(ObservPostViewModelS.Count).Take(10));
            }
            IndicatorLoadMore.IsRunning = false;
        }
    }

    private async void BtnMore_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        long postId = (long)getbinding.CommandParameter;
        await Navigation.PushModalAsync(new NavigationPage(new SinglePost(postId)));
    }
}