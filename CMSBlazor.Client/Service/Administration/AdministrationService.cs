using System;
using System.Net.Http.Headers;
using CMSBlazor.Client.Helpers;
using CMSBlazor.Shared.ViewModels.Administration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMSBlazor.Client.Service.Administration
{
	public class AdministrationService : IAdministrationService
    {

        //private readonly HttpClient _httpClient;
        //private readonly AuthenticationStateProvider _authenticationStateProvider;




  //      public AdministrationService(HttpClient httpClient)
		//{
  //          _httpClient = httpClient;
  //      }



        //==========================================================================================



        public async Task<IEnumerable<Roles>> ListRoles()
        {
            HttpClient _httpClient = new HttpClient();
            List<Roles> roles = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                var response = await _httpClient.GetAsync(CMSConstant.BaseApiAddress + "api/Administration/ListRoles");


                var content = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(content);
                JToken? jToken = jObject["roles"];

                var reslt = JsonConvert.DeserializeObject<List<Roles>>(jToken!.ToString());
                roles = reslt!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                roles = null!;
            }
            return roles;
        }


        //==========================================================================================


        public async Task<IEnumerable<Users>> ListUsers()
        {
            HttpClient _httpClient = new HttpClient();
            List<Users> users = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                var response = await _httpClient.GetAsync(CMSConstant.BaseApiAddress + "api/Administration/ListUsers");


                var content = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(content);
                JToken? jToken = jObject["users"];

                var reslt = JsonConvert.DeserializeObject<List<Users>>(jToken!.ToString());
                users = reslt!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                users = null!;
            }
            return users;
        }


        //==========================================================================================


        public async Task<RoleViewModel?> GetEditRole(string roleId)
        {
            HttpClient _httpClient = new HttpClient();
            RoleViewModel roleViewModel = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                var model = new RoleViewModel
                {
                    Id = roleId,
                };

                var json = JsonConvert.SerializeObject(model);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetEditRole", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var rolename = jObject.Value<string>("name");
                var roleid = jObject.Value<string>("id");
                JToken jToken = jObject["users"]!;
                var users = JsonConvert.DeserializeObject<List<string>>(jToken.ToString());
                //var user = jObject.Value<List<string>>("users");


                var _roleViewModel = new RoleViewModel
                {
                    Successful = success,
                    Message = message,

                    Id = roleid,
                    RoleName = rolename,
                    Users = users,
                };
                roleViewModel = _roleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                roleViewModel = null!;
            }
            return roleViewModel;
        }




        //==========================================================================================

        public async Task<RoleViewModel?> EditRole(RoleViewModel roleViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                //var model = new RoleViewModel
                //{
                //    Id = roleId,
                //};

                var json = JsonConvert.SerializeObject(roleViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/EditRole", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var rolename = jObject.Value<string>("name");
                var roleid = jObject.Value<string>("id");
                JToken jToken = jObject["users"]!;
                var users = JsonConvert.DeserializeObject<List<string>>(jToken.ToString());

                roleViewModel = null!;
                var _roleViewModel = new RoleViewModel
                {
                    Successful = success,
                    Message = message,

                    Id = roleid,
                    RoleName = rolename,
                    Users = users,
                };
                roleViewModel = _roleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                roleViewModel = null!;
            }
            return roleViewModel;
        }




        //==========================================================================================


        public async Task<UserRoleViewModel> GetEditUsersInRole(UserRoleViewModel userRoleViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));

                
                var json = JsonConvert.SerializeObject(userRoleViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetEditUsersInRole", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                JToken jToken = jObject["listUserRole"]!;
                var listUserRole = JsonConvert.DeserializeObject<List<UserRole>>(jToken.ToString());
               
                userRoleViewModel = null!;

                var _userRoleViewModel = new UserRoleViewModel
                {
                    Successful = success,
                    Message = message,

                    userRoles = listUserRole,
                };
                userRoleViewModel = _userRoleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userRoleViewModel = null!;
            }
            return userRoleViewModel;
        }



        //==========================================================================================


        public async Task<UserRoleViewModel> EditUsersInRole(UserRoleViewModel userRoleViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userRoleViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/EditUsersInRole", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var id = jObject.Value<string>("id");

                userRoleViewModel = null!;

                var _userRoleViewModel = new UserRoleViewModel
                {
                    Successful = success,
                    Message = message,

                    RoleId = id,
                };
                userRoleViewModel = _userRoleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userRoleViewModel = null!;
            }
            return userRoleViewModel;
        }




        //==========================================================================================


        public async Task<CreateRoleViewModel> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            HttpClient _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

            var json = JsonConvert.SerializeObject(createRoleViewModel);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/CreateRole", httpContent);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                createRoleViewModel.Message = jObject.Value<string>("message");
                createRoleViewModel.Successful = success;
            }
            else
            {
                createRoleViewModel.Message = jObject.Value<string>("message");
                createRoleViewModel.Successful = success;
            }
            return createRoleViewModel;
        }

        //==========================================================================================

        public async Task<RoleViewModel> DeleteRole(RoleViewModel roleViewModel)
        {
            HttpClient _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

            var json = JsonConvert.SerializeObject(roleViewModel);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/DeleteRole", httpContent);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                roleViewModel.Message = jObject.Value<string>("message");
                roleViewModel.Successful = success;
            }
            else
            {
                roleViewModel.Message = jObject.Value<string>("message");
                roleViewModel.Successful = success;
            }
            return roleViewModel;
        }



        //==========================================================================================


        public async Task<UserID> DeleteUser(UserID userID)
        {
            HttpClient _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 "Bearer", Preferences.Default.Get("Token", ""));

            var json = JsonConvert.SerializeObject(userID);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var result = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/DeleteUser", httpContent);

            var content = await result.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content)!;

            var success = jObject.Value<bool>("success");

            if (success == true)
            {
                userID.Message = jObject.Value<string>("message");
                userID.Successful = success;
            }
            else
            {
                userID.Message = jObject.Value<string>("message");
                userID.Successful = success;
            }
            return userID;
        }




        //==========================================================================================

        public async Task<UserViewModel> GetEditUser(UserID userID)
        {
            HttpClient _httpClient = new HttpClient();
            UserViewModel userViewModel = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userID);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetEditUser", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var model = jObject.Value<object>("model");
                JObject? jObject_model = JsonConvert.DeserializeObject<dynamic>(model!.ToString()!);
                var id = jObject_model!.Value<string>("id");
                var userName = jObject_model!.Value<string>("userName");
                var email = jObject_model!.Value<string>("email");
                var block = jObject_model!.Value<bool>("block");

                JToken claims = jObject_model["claims"]!;
                var listclaims = JsonConvert.DeserializeObject<List<string>>(claims.ToString());

                JToken roles = jObject_model["roles"]!;
                var listroles = JsonConvert.DeserializeObject<List<string>>(roles.ToString());



                var _userViewModel = new UserViewModel
                {
                    Successful = success,
                    Message = message,

                    Id = id,
                    UserName = userName!,
                    Email = email,
                    Block= block,
                    Claims = listclaims!,
                    Roles = listroles!
                };
                userViewModel = _userViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userViewModel = null!;
            }
            return userViewModel;
        }


        //==========================================================================================
        public async Task<UserViewModel> EditUser(UserViewModel userViewModel)
        {
            HttpClient _httpClient = new HttpClient();

            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/EditUser", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");

                var _userViewModel = new UserViewModel
                {
                    Successful = success,
                    Message = message,
                };
                userViewModel = _userViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userViewModel = null!;
            }
            return userViewModel;
        }


        //==========================================================================================

        public async Task<UserRoleViewModel> GetManageUserRoles(UserID userID)
        {
            HttpClient _httpClient = new HttpClient();
            UserRoleViewModel userRoleViewModel = null!;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userID);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetManageUserRoles", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                JToken model = jObject["model"]!;
                var rolesUser = JsonConvert.DeserializeObject<List<RolesUser>>(model!.ToString());

                var _userRoleViewModel = new UserRoleViewModel
                {
                    Successful = success,
                    Message = message,

                    rolesUsers = rolesUser
                };
                userRoleViewModel = _userRoleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userRoleViewModel = null!;
            }
            return userRoleViewModel;
        }
        //==========================================================================================
        public async Task<UserRoleViewModel> ManageUserRoles(UserRoleViewModel userRoleViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userRoleViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/ManageUserRoles", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var _userRoleViewModel = new UserRoleViewModel
                {
                    Successful = success,
                    Message = message,
                };
                userRoleViewModel = _userRoleViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userRoleViewModel = null!;
            }
            return userRoleViewModel;
        }


        //==========================================================================================



        public async Task<UserClaimsViewModel> GetManageUserClaims(UserID userID)
        {
            HttpClient _httpClient = new HttpClient();
            UserClaimsViewModel userClaimsViewModel = null!;
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userID);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetManageUserClaims", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                var model = jObject.Value<object>("model");
                JObject? jObject_model = JsonConvert.DeserializeObject<dynamic>(model!.ToString()!);
                var userId = jObject_model!.Value<string>("userId");
                JToken cliams = jObject_model["cliams"]!;
                var listcliams = JsonConvert.DeserializeObject<List<UserClaim>>(cliams!.ToString());

                var _userClaimsViewModel = new UserClaimsViewModel
                {
                    Successful = success,
                    Message = message,

                    UserId = userId,
                    Cliams = listcliams!
                };
                userClaimsViewModel = _userClaimsViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userClaimsViewModel = null!;
            }
            return userClaimsViewModel;
        }


        //==========================================================================================

        public async Task<UserClaimsViewModel> ManageUserClaims(UserClaimsViewModel userClaimsViewModel)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(userClaimsViewModel);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/ManageUserClaims", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");

                var _userClaimsViewModel = new UserClaimsViewModel
                {
                    Successful = success,
                    Message = message,
                };
                userClaimsViewModel = _userClaimsViewModel;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                userClaimsViewModel = null!;
            }
            return userClaimsViewModel;
        }






        public async Task<PolicyRole> GetClaimsByUser(PolicyRole policyRole)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(policyRole);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetClaimsByUser", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                JToken claims = jObject["claims"]!;
                var listcliams = JsonConvert.DeserializeObject<List<Policy>>(claims!.ToString());

                policyRole = null!;

                var _policyRole = new PolicyRole
                {
                    Successful = success,
                    Message = message,
                    policies = listcliams
                };
                Console.WriteLine("====================");
                policyRole = _policyRole;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                policyRole = null!;
            }
            return policyRole;
        }

        public async Task<PolicyRole> GetRolesByUser(PolicyRole policyRole)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                     "Bearer", Preferences.Default.Get("Token", ""));


                var json = JsonConvert.SerializeObject(policyRole);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                var response = await _httpClient.PostAsync(CMSConstant.BaseApiAddress + "api/Administration/GetRolesByUser", httpContent);
                var content = await response.Content.ReadAsStringAsync();




                //Get Objects==============================================================
                JObject? jObject = JsonConvert.DeserializeObject<dynamic>(content);
                var success = jObject!.Value<bool>("success");
                var message = jObject.Value<string>("message");
                JToken roles = jObject["roles"]!;
                var listrole = JsonConvert.DeserializeObject<List<Role>>(roles!.ToString());

                policyRole = null!;

                var _policyRole = new PolicyRole
                {
                    Successful = success,
                    Message = message,

                    roles = listrole

                };
                policyRole = _policyRole;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                policyRole = null!;
            }
            return policyRole;
        }






    }
}

