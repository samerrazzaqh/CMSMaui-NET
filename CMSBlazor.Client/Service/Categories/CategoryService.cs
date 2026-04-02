using System;
using CMSBlazor.Shared.ViewModels.Post;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CMSBlazor.Client.Helpers;
using System.Net.Http;

namespace CMSBlazor.Client.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        //public CategoryService()
        //{
        //}

        public async Task<IEnumerable<CategoryViewModel>?> GetCategories()
        {
            HttpClient _httpClient = new HttpClient();
            List<CategoryViewModel> result_categories = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                var response = await _httpClient.GetAsync(CMSConstant.BaseApiAddress + "api/Category");


                var content = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(content);
                JToken jToken = jObject["categories"]!;

                var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jToken.ToString());
                result_categories = categories!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result_categories = null!;
            }
            return result_categories;
        }



        public async Task<CategoryViewModel> CreateCategory(CategoryViewModel newCategory)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Category", newCategory);

            var content = await result.Content.ReadAsStringAsync();
            JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);

            var success = jObject!.Value<bool>("success");

            if (success == true)
            {
                newCategory.Message = jObject.Value<string>("message");
                newCategory.Successful = success;
            }
            else
            {
                newCategory.Message = jObject.Value<string>("message");
                newCategory.Successful = success;
            }
            return newCategory;

        }

        public async Task<CategoryViewModel>  DeleteCategory(CategoryViewModel categoryViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

            var result = await _httpClient.DeleteAsync(CMSConstant.BaseApiAddress + $"api/Category/{categoryViewModel.CategoryId}");
            
            var content = await result.Content.ReadAsStringAsync();
            JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
            var success = jObject!.Value<bool>("success");

            if (success == true)
            {
                categoryViewModel.Message = jObject.Value<string>("message");
                categoryViewModel.Successful = success;
            }
            else
            {
                categoryViewModel.Message = jObject.Value<string>("message");
                categoryViewModel.Successful = success;
            }
            return categoryViewModel;
        }

        public async Task<CategoryViewModel?> GetCategory(int id)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));
            return await _httpClient.GetFromJsonAsync<CategoryViewModel>(CMSConstant.BaseApiAddress + $"api/Category/{id}");
        }

        public async Task<CategoryViewModel?> UpdateCategory(CategoryViewModel updatedCategory)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

            var result = await _httpClient.PutAsJsonAsync<CategoryViewModel>(CMSConstant.BaseApiAddress + "api/Category", updatedCategory);


            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                updatedCategory.Message = jObject.Value<string>("message");
                updatedCategory.Successful = success;
            }
            else
            {
                updatedCategory.Message = jObject.Value<string>("message");
                updatedCategory.Successful = success;
            }
            return updatedCategory;




        }
    }
}

