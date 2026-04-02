using System;
using System.Net.Http.Headers;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.CommentLike;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSBlazor.Client.Service.ReComment
{
    public class ReCommentService : IReCommentService
	{
        public ReCommentService()
        {
        }

     

        public async Task<ReComentsViewModel> ListReComment(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/ListReComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var numberRecomment = jObject.Value<int>("numberRecomment");
                JToken recomments = jObject["recomments"]!;
                var InfoReComment = JsonConvert.DeserializeObject<List<InfoReComment>>(recomments.ToString());



                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                    numberRecomment= numberRecomment,
                    InfoReComments = InfoReComment
                };
                reComentsViewModel = _reComentsViewModel;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }


        public async Task<ReComentsViewModel> ListLikeReComment(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/ListLikeReComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var numberlikesReComment = jObject!.Value<int>("numberlikesReComment");
                JToken getLikeReComments = jObject["getLikeReComments"]!;
                var LikeReComments = JsonConvert.DeserializeObject<List<LikeReComments>>(getLikeReComments.ToString());



                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                    LikeReComments = LikeReComments,
                    numberlikesReComment = numberlikesReComment
                };
                reComentsViewModel = _reComentsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }



        public async Task<ReComentsViewModel> CreateReComment(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/CreateReComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");



                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                };
                reComentsViewModel = _reComentsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }

        public async Task<ReComentsViewModel> DeleteReComment(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/DeleteReComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");



                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                };
                reComentsViewModel = _reComentsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }

        public async Task<ReComentsViewModel> EditReComment(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/EditReComment", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");


                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                };
                reComentsViewModel = _reComentsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }

        public async Task<ReComentsViewModel> LikeReCommentCreate(ReComentsViewModel model)
        {
            ReComentsViewModel reComentsViewModel = null!;
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                   "Bearer", Preferences.Default.Get("Token", ""));

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/ReComments/LikeReCommentCreate", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");


                var _reComentsViewModel = new ReComentsViewModel
                {
                    Successful = success,
                };
                reComentsViewModel = _reComentsViewModel;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                reComentsViewModel = null!;
            }
            return reComentsViewModel;
        }
    }
}

