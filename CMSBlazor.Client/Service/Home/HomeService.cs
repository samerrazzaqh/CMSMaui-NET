using System;
using System.Net.Http.Headers;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.Post;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSBlazor.Client.Service.Home
{
	public class HomeService: IHomeService
    {
        public HomeService()
        {
        }
        
        public async Task<PostAll> ListPost(PostUtilities postUtilities)
        {
            PostAll postAll = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", Preferences.Default.Get("Token", ""));
                

                var json = JsonConvert.SerializeObject(postUtilities);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Home/ListPost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var postscount = jObject.Value<int>("postscount");
                JToken posts = jObject["posts"]!;
                var post = JsonConvert.DeserializeObject<List<PostViewModel>>(posts.ToString());

                var _postAll = new PostAll
                {
                    PostViewModels = post,
                    PostsCount = postscount,
                };
                postAll = _postAll;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postAll = null!;
            }
            return postAll;
        }

        //==========================================================================
        public async Task<IEnumerable<PostViewModel>?> MostPopularPost(int numpost)
        {
            List<PostViewModel> postViewModels = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var model = new PostUtilities
                {
                    NumGetPost = numpost,
                };

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Home/MostPopularPost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject jObject = JObject.Parse(content);
                JToken posts = jObject["posts"]!;
                var post = JsonConvert.DeserializeObject<List<PostViewModel>>(posts.ToString());
                postViewModels = post!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postViewModels = null!;
            }
            return postViewModels;
        }

        //==========================================================================
        public async Task<IEnumerable<PostViewModel>?> ListPostByCategory(int numpost,int catId)
        {
            List<PostViewModel> postViewModels = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var model = new PostUtilities
                {
                    NumGetPost = numpost,
                    CategoryId = catId
                };

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Home/ListPostByCategory", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject jObject = JObject.Parse(content);
                JToken posts = jObject["posts"]!;
                var post = JsonConvert.DeserializeObject<List<PostViewModel>>(posts.ToString());
                postViewModels = post!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postViewModels = null!;
            }
            return postViewModels;
        }

        //==========================================================================

        public async Task<PostViewModel> SinglePost(PostUtilities postUtilities)
        {
            PostViewModel postViewModels = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));



                var json = JsonConvert.SerializeObject(postUtilities);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Home/SinglePost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var post = jObject.Value<object>("post");
                JObject? jObject_post = JsonConvert.DeserializeObject<dynamic>(post!.ToString()!);
                var postId = jObject_post!.Value<long>("postId");
                var title = jObject_post!.Value<string>("title");
                var postImg = jObject_post!.Value<string>("postImg");
                var postContent = jObject_post!.Value<string>("postContent");
                var linkVideo = jObject_post!.Value<string>("linkVideo");
                var postDate = jObject_post.Value<DateTime>("postDate");
                var categoryId = jObject_post!.Value<int>("categoryId");
                var catName = jObject_post!.Value<string>("catName");
                var postViews = jObject_post!.Value<int>("postViews");
                var auther = jObject_post!.Value<string>("auther");
                var urlImageAuther = jObject_post!.Value<string>("urlImageAuther");
                var numbercomment = jObject_post!.Value<int>("numbercomment");
                var numberlikes = jObject_post!.Value<int>("numberlikes");
                JToken usersLikePost = jObject_post["usersLikePost"]!;
                var usersLikePostlist = JsonConvert.DeserializeObject<List<UsersLikePost>>(usersLikePost.ToString());


                var _postViewModels = new PostViewModel
                {
                    Successful = success,

                    PostId = postId,
                    Title = title,
                    PostImg = postImg,
                    PostContent = postContent,
                    LinkVideo = linkVideo,
                    PostDate = postDate,
                    CategoryId = categoryId,
                    CatName = catName,
                    PostViews = postViews,
                    Auther = auther,
                    UrlImageAuther = urlImageAuther,
                    NumberComment = numbercomment,
                    NumberLikes = numberlikes,
                    UsersLikePosts = usersLikePostlist

                };
                postViewModels = _postViewModels;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postViewModels = null!;
            }
            return postViewModels;
        }










    }
}

