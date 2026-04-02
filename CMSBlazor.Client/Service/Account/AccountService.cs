using System;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.Account;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net.Http.Json;

namespace CMSBlazor.Client.Service.Account
{

    public class AccountService
    {
        //private readonly HttpClient _httpClient;

        //public AccountService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}
        //==================================Register=============================================
        public async Task<RegisterViewModel> Register(RegisterViewModel registerModel)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/Register", registerModel);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                registerModel.Email = jObject.Value<string>("email");
                registerModel.Message = jObject.Value<string>("message");
                registerModel.Successful = success;
            }
            else
            {
                registerModel.Email = jObject.Value<string>("email"); 
                registerModel.Message = jObject.Value<string>("message"); 
                registerModel.Successful = success;
            }
            return registerModel;
        }


        //==================================Login=============================================

        public async Task<LoginViewModel> Login(LoginViewModel loginModel)
        {
            HttpClient _httpClient = new HttpClient();
            var loginAsJson = JsonConvert.SerializeObject(loginModel);
            var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Account/Login",
                new StringContent(loginAsJson, Encoding.UTF8, "application/json"));


            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;
            var success = jObject.Value<bool>("success");


            if (success == true)
            {
                var userid = jObject.Value<string>("id");
                var userName = jObject.Value<string>("userName");
                var email = jObject.Value<string>("email");
                var firstName = jObject.Value<string>("firstName");
                var lastName = jObject.Value<string>("lastName");
                var aboutuserid = jObject.Value<string>("aboutuserid");
                var message = jObject.Value<string>("message");
                var token = jObject.Value<string>("token");

                loginModel.Message = message;
                loginModel.Successful = success;
                loginModel.Token = token;


                Preferences.Set("Token", token);
                //Preferences.Set(DateTime, DateTime.UtcNow.AddMonths(12));
                Preferences.Set("UserId", userid);
                Preferences.Set("Email", email);
                Preferences.Set("UserName", userName);
                Preferences.Set("FirstName", firstName);
                Preferences.Set("LastName", lastName);
                Preferences.Set("AboutUserId", aboutuserid);
            }
            else
            {
                loginModel.Message = jObject.Value<string>("message");
                loginModel.Successful = success;
            }


            return loginModel;
        }

        //==================================LoginStorage=============================================

        //public void LoginStorage(LocalStorageModel localStorageModel)
        //{

        //    //((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(localStorageModel.Email!);
        //    //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", localStorageModel.Token);
        //}
       
        //==================================ExternalLogin=============================================

        public async Task<RegisterExternalViewModel> ExternalLogin(RegisterExternalViewModel registerExternal)
        {
            HttpClient _httpClient = new HttpClient();
            var loginAsJson = JsonConvert.SerializeObject(registerExternal);
            var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Account/ExternalLogin",
                new StringContent(loginAsJson, Encoding.UTF8, "application/json"));


            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;
            var success = jObject.Value<bool>("success");


            if (success == true)
            {
                var userid = jObject.Value<string>("id");
                var userName = jObject.Value<string>("userName");
                var email = jObject.Value<string>("email");
                var aboutuserid = jObject.Value<string>("aboutuserid");
                var message = jObject.Value<string>("message");
                var token = jObject.Value<string>("token");

                registerExternal.Message = message;
                registerExternal.Successful = success;



                //localStorageModel.Token = token;
                //localStorageModel.UserId = userid;
                //localStorageModel.Email = email;
                //localStorageModel.UserName = userName;
                //localStorageModel.AboutUserId = aboutuserid;

                //await _localStorage.SetItemAsync("Storage", localStorageModel);

                //((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(email!);
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                registerExternal.Email = jObject.Value<string>("email");
                registerExternal.Message = jObject.Value<string>("message");
                registerExternal.Successful = success;
            }


            return registerExternal;
        }

        //==================================Logout=============================================
        public async Task Logout()
        {
            HttpClient _httpClient = new HttpClient();
            Preferences.Default.Clear();
            await _httpClient.GetAsync(CMSConstant.BaseApiAddress + "api/Account/Logout");
        }
        //==================================ConfirmEmail=============================================
        public async Task<ConfirmEmailViewModel> ConfirmEmail(ConfirmEmailViewModel confirmEmailViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/ConfirmEmail", confirmEmailViewModel);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                confirmEmailViewModel.Message = jObject.Value<string>("message");
                confirmEmailViewModel.Successful = success;
            }
            else
            {
                confirmEmailViewModel.Message = jObject.Value<string>("message");
                confirmEmailViewModel.Successful = success;
            }
            return confirmEmailViewModel;
        }

        //==================================SendConfirmationEmail=============================================
        public async Task<SendTokenEmail> SendConfirmationEmail(SendTokenEmail sendTokenEmail)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/SendConfirmationEmail", sendTokenEmail);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                sendTokenEmail.Message = jObject.Value<string>("message");
                sendTokenEmail.Successful = success;
                sendTokenEmail.Email = jObject.Value<string>("email");
            }
            else
            {
                sendTokenEmail.Message = jObject.Value<string>("message");
                sendTokenEmail.Successful = success;
            }
            return sendTokenEmail;
        }
        //==================================ForgotPasswordbyEmail=============================================
        public async Task<SendTokenEmail> ForgotPasswordbyEmail(SendTokenEmail sendTokenEmail)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/ForgotPasswordbyEmail", sendTokenEmail);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                sendTokenEmail.Message = jObject.Value<string>("message");
                sendTokenEmail.Successful = success;
                sendTokenEmail.Email = jObject.Value<string>("email");
            }
            else
            {
                sendTokenEmail.Message = jObject.Value<string>("message");
                sendTokenEmail.Successful = success;
            }
            return sendTokenEmail;
        }

        //==================================PasswordConfirmEmail=============================================
        public async Task<ConfirmEmailViewModel> PasswordConfirmEmail(ConfirmEmailViewModel confirmEmailViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/PasswordConfirmEmail", confirmEmailViewModel);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                confirmEmailViewModel.Message = jObject.Value<string>("message");
                confirmEmailViewModel.Successful = success;
            }
            else
            {
                confirmEmailViewModel.Message = jObject.Value<string>("message");
                confirmEmailViewModel.Successful = success;
            }
            return confirmEmailViewModel;
        }
        //==================================ResetPasswordbyEmail=============================================
        public async Task<ResetPasswordbyEmailViewModel> ResetPasswordbyEmail(ResetPasswordbyEmailViewModel resetPasswordbyEmail)
        {
            HttpClient _httpClient = new HttpClient();
            var result = await _httpClient.PostAsJsonAsync(CMSConstant.BaseApiAddress + "api/Account/ResetPasswordbyEmail", resetPasswordbyEmail);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                resetPasswordbyEmail.Message = jObject.Value<string>("message");
                resetPasswordbyEmail.Successful = success;
            }
            else
            {
                resetPasswordbyEmail.Message = jObject.Value<string>("message");
                resetPasswordbyEmail.Successful = success;
            }
            return resetPasswordbyEmail;
        }

        
    }
}

