using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Client.Service.Posts;
using CMSBlazor.Shared.ViewModels.Administration;
using CMSBlazor.Shared.ViewModels.Post;
using MvvmHelpers;

namespace CMSBlazor.Client.Views.Post;

public partial class ListPost : ContentPage
{
    public PostAll? PostAll = new PostAll();

    public ObservableRangeCollection<PostViewModel> ObservPostViewModel { get; set; } =
        new ObservableRangeCollection<PostViewModel>();

    public PostUtilities? postUtilities = new PostUtilities();
    public IEnumerable<PostViewModel> PostViewModels = new List<PostViewModel>();
    PostService PostService = new PostService();
    public int num, PostsCount, newPostsCount;

    
    
    //=========================PolicyClaims=========================================
    public bool _policyCreate, _policyEdit, _policyDelete;
    public string? _SuperAdmin, _Admin, _User;
    public IEnumerable<Policy> Policies = new List<Policy>();
    public IEnumerable<Role> Role = new List<Role>();
    public PolicyRole? PolicyClaims = new PolicyRole();
    public PolicyRole? PolicyRole = new PolicyRole();
    AdministrationService AdministrationService = new AdministrationService();
    //==================================================================
    public ListPost()
    {
        InitializeComponent();
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        ObservPostViewModel.Clear();
        Loaddata();
    }

    public async void Loaddata()
    {
        try
        {
            IndicatorLoadMore.IsRunning = true;
            
            //=========================PolicyClaims=========================================
            PolicyClaims!.UserId = Preferences.Get("UserId", "");
            PolicyClaims = await AdministrationService!.GetClaimsByUser(PolicyClaims!);
            Policies = PolicyClaims.policies!;

            PolicyRole!.UserId = Preferences.Get("UserId", "");
            PolicyRole = await AdministrationService!.GetRolesByUser(PolicyRole!);
            Role = PolicyRole.roles!;

            _policyCreate = Policies.Where(e => e.ClaimType == "Create Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;
            _policyEdit = Policies.Where(e => e.ClaimType == "Edit Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;
            _policyDelete = Policies.Where(e => e.ClaimType == "Delete Role").Select(e => e.ClaimValue).FirstOrDefault() == true ? false : true;

            _SuperAdmin = Role.Where(e => e.RoleName == "SuperAdmin").Select(e => e.RoleName).FirstOrDefault();
            _Admin = Role.Where(e => e.RoleName == "Admin").Select(e => e.RoleName).FirstOrDefault();
            _User = Role.Where(e => e.RoleName == "User").Select(e => e.RoleName).FirstOrDefault();
            //==================================================================
            
            
            postUtilities!.NumGetPost = 20;
            PostAll = await PostService!.ListPost(postUtilities);
            PostsCount = PostAll.PostsCount;
            foreach (var postViewModels in PostAll.PostViewModels)
            {
                postViewModels.IsEnabledEdit = true;
                postViewModels.IsEnabledDelete = true;
                if (_policyEdit)
                {
                    postViewModels.IsEnabledEdit = false;
                }
                if (_policyDelete)
                {
                    postViewModels.IsEnabledDelete = false;
                }
                PostAll.PostViewModels.Append(postViewModels);
            }
            ObservPostViewModel.AddRange(PostAll.PostViewModels!.Take(postUtilities.NumGetPost));
            listPost.ItemsSource = ObservPostViewModel;
            IndicatorLoadMore.IsRunning = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }


    async void ListPost_OnRemainingItemsThresholdReached(object? sender, EventArgs e)
    {
        if (searchPost.Text == null || searchPost.Text == "")
        {
            postUtilities!.NumGetPost += 10;
            PostAll = await PostService!.ListPost(postUtilities);
            newPostsCount = PostAll.PostViewModels.Count();
            
            if (PostsCount != newPostsCount)
            {
                IndicatorLoadMore.IsRunning = true;
                if (ObservPostViewModel.Count > 0)
                {
                    ObservPostViewModel.AddRange(PostAll.PostViewModels!.Skip(ObservPostViewModel.Count).Take(10));
                }

                IndicatorLoadMore.IsRunning = false;
            }
        }
    }

    async void BtnSettings_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        long postId = (long)getbinding.CommandParameter;
        await Navigation.PushModalAsync(new NavigationPage(new EditePost(postId)));
    }

    async void BtnDelete_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        long postId = (long)getbinding.CommandParameter;
        postUtilities.PostId = postId;
        bool answer = await DisplayAlert("", "Are you Delete Post", "Yes", "No");
        if (answer)
        {
            postUtilities = await PostService!.DeletePost(postUtilities);
            if (postUtilities.Successful)
            {
                ObservPostViewModel.Clear();
                Loaddata();
            }
            else
            {
                await DisplayAlert("Info Post", $"{postUtilities.Message}", "OK");
            }
        }
       
        
    }

    async void SearchPost_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (searchPost!.Text != null || searchPost.Text != "")
        {
            postUtilities!.NumGetPost = PostAll!.PostsCount;
            postUtilities.Search = searchPost.Text;
            PostAll = await PostService!.ListPost(postUtilities);
            listPost.ItemsSource = PostAll!.PostViewModels.ToList();
        }
        else if (searchPost!.Text == null || searchPost.Text == "")
        {
            PostAll!.PostViewModels.Clear();
            Loaddata();
        }
    }

    async void BtAdd_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new NavigationPage(new CreatePost()));
    }
}