using System;
using System.Net.Http;
using System.Net.Http.Headers;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.CommentLike;
using CMSBlazor.Shared.ViewModels.Post;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSBlazor.Client.Service.Comment
{
	public class CommentService : ICommentService
    {

        public CommentService()
        {
        }

    

        public async Task<CommentsViewModel> ListComment(CommentsViewModel model)
        {
            HttpClient _httpClient = new HttpClient();
            
            CommentsViewModel commentsViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                  "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/ListComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();



                
                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var postscount = jObject.Value<int>("commentsCount");
                JToken comments = jObject["comments"]!;
                var InfoComment = JsonConvert.DeserializeObject<List<InfoComment>>(comments.ToString());



                var _commentsViewModel = new CommentsViewModel
                {
                    Successful = success,

                    PostsCount = postscount,
                    InfoComments = InfoComment
                };
                commentsViewModel = _commentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                commentsViewModel = null!;
            }
            return commentsViewModel;
        }


        public async Task<CommentsViewModel> CreateComment(CommentsViewModel model)
        {
            HttpClient _httpClient = new HttpClient();
            CommentsViewModel commentsViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/CreateComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");

                var _commentsViewModel = new CommentsViewModel
                {
                    Successful = success,
                };
                commentsViewModel = _commentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                commentsViewModel = null!;
            }
            return commentsViewModel;
        }

        public async Task<CommentsViewModel> DeleteComment(CommentsViewModel model)
        {
            HttpClient _httpClient = new HttpClient();
            CommentsViewModel commentsViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/DeleteComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");

                var _commentsViewModel = new CommentsViewModel
                {
                    Successful = success,
                };
                commentsViewModel = _commentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                commentsViewModel = null!;
            }
            return commentsViewModel;
        }

        public async Task<CommentsViewModel> EditComment(CommentsViewModel model)
        {
            HttpClient _httpClient = new HttpClient();
            CommentsViewModel commentsViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/EditComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");

                var _commentsViewModel = new CommentsViewModel
                {
                    Successful = success,
                };
                commentsViewModel = _commentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                commentsViewModel = null!;
            }
            return commentsViewModel;
        }

        public async Task<CommentsViewModel> LikeCommentCreate(LikesComment model)
        {
            HttpClient _httpClient = new HttpClient();
            CommentsViewModel commentsViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                  "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/LikeCommentCreate", httpContent);
                var content = await response.Content.ReadAsStringAsync();

                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");

                var _commentsViewModel = new CommentsViewModel
                {
                    Successful = success,
                };
                commentsViewModel = _commentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                commentsViewModel = null!;
            }
            return commentsViewModel;
        }



        public async Task<PostViewModel> LikePostCreate(CommentsViewModel model)
        {
            HttpClient _httpClient = new HttpClient();
            PostViewModel postViewModels = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));



                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Comments/LikePostCreate", httpContent);
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

