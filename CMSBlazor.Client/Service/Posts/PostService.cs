using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.Post;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSBlazor.Client.Service.Posts
{
    public class PostService: IPostService
    {
        public PostService()
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


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Posts/ListPost", httpContent);
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



        public async Task<PostViewModel> CreatePost(PostViewModel postViewModel)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));



                var json = JsonConvert.SerializeObject(postViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Posts/CreatePost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");

                postViewModel = null!;
                var _postViewModel = new PostViewModel
                {
                    Successful = success,
                    Message = message,
                };
                postViewModel = _postViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postViewModel = null!;
            }
            return postViewModel;
        }




        public async Task<PostUtilities> DeletePost(PostUtilities postUtilities)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(postUtilities);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Posts/DeletePost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");

                postUtilities = null!;
                var _postUtilities = new PostUtilities
                {
                    Successful = success,
                    Message = message,
                };
                postUtilities = _postUtilities;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postUtilities = null!;
            }
            return postUtilities;
        }

        public async Task<PostViewModel?> GetEditPost(PostUtilities postUtilities)
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


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Posts/GetEditPost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject jObject = JObject.Parse(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var posts = jObject.Value<object>("posts");
                JObject? jObject_posts = JsonConvert.DeserializeObject<dynamic>(posts!.ToString()!);
                var postId = jObject_posts!.Value<long>("postId");
                var title = jObject_posts!.Value<string>("title");
                var auther = jObject_posts!.Value<string>("auther");
                var postContent = jObject_posts!.Value<string>("postContent");
                var postImg = jObject_posts!.Value<string>("postImg");
                var linkVideo = jObject_posts!.Value<string>("linkVideo");
                var isPublish = jObject_posts!.Value<bool>("isPublish");
                var categoryId = jObject_posts!.Value<int>("categoryId");


                var _postViewModels = new PostViewModel
                {
                    Successful = success,
                    Message = message,

                    PostId = postId,
                    Title = title,
                    Auther = auther,
                    PostContent = postContent,
                    PostImg = postImg,
                    LinkVideo = linkVideo,
                    IsPublish = isPublish,
                    CategoryId = categoryId
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

        public async Task<PostViewModel?> EditPost(PostViewModel postViewModel)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));



                var json = JsonConvert.SerializeObject(postViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Posts/EditPost", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");

                postViewModel = null!;
                var _postViewModel = new PostViewModel
                {
                    Successful = success,
                    Message = message,
                };
                postViewModel = _postViewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                postViewModel = null!;
            }
            return postViewModel;
        }
    }
}

