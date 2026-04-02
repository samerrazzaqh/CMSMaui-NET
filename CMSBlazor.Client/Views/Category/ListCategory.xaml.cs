using CMSBlazor.Client.Service.Administration;
using CMSBlazor.Client.Service.Categories;
using CMSBlazor.Shared.ViewModels.Administration;
using CMSBlazor.Shared.ViewModels.Post;

namespace CMSBlazor.Client.Views.Category;

public partial class ListCategory : ContentPage
{
    CategoryService CategoryService = new CategoryService();
    public IEnumerable<CategoryViewModel> CategoryViewModels = new List<CategoryViewModel>();
    public CategoryViewModel categoryViewModel = new CategoryViewModel();
    int updatecategoryId;
    
    //=========================PolicyClaims=========================================
    public bool _policyCreate, _policyEdit, _policyDelete;
    public string? _SuperAdmin, _Admin, _User;
    AdministrationService AdministrationService = new AdministrationService();
    public IEnumerable<Policy> Policies = new List<Policy>();
    public IEnumerable<Role> Role = new List<Role>();
    public PolicyRole? PolicyClaims = new PolicyRole();
    public PolicyRole? PolicyRole = new PolicyRole();
    //==================================================================

    
    public ListCategory()
    {
        InitializeComponent();
    }

     protected async override void OnAppearing()
    {
        base.OnAppearing();
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
        
        
        if (_policyCreate)
        {
            gridCreateCategory.IsEnabled = false;
        }
        if (_policyEdit)
        {
            gridUpdateCategory.IsEnabled = false;
        }
        
        CategoryViewModels = (await CategoryService!.GetCategories())!.ToList();
        foreach (var Category in CategoryViewModels)
        {
            Category.IsEnabledEdit = true;
            Category.IsEnabledDelete = true;
            if (_policyEdit)
            {
                Category.IsEnabledEdit = false;
            }
            if (_policyDelete)
            {
                Category.IsEnabledDelete = false;
            }
            CategoryViewModels.Append(Category);
        }
        categoryList.ItemsSource = CategoryViewModels;
    }

    //==================================================================
    
    
    
    async void BtCreate_OnClicked(object? sender, EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etCategoryName.Text.Trim()))
            {
                categoryViewModel.CatName = etCategoryName.Text;
                var result =  await CategoryService!.CreateCategory(categoryViewModel);
                if (result!.Successful == false)
                {
                    await DisplayAlert("Info Category", $"{result.Message}", "OK");
                }
                else
                {
                    CategoryViewModels = (await CategoryService!.GetCategories())!.ToList();
                    categoryList.ItemsSource = CategoryViewModels;
                }
                entryCategoryName.Text = string.Empty;
            }
            else
            {
                ErrorMessage();
            }
        }
        catch {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        }
    }

    private void EntryCategoryName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etCategoryName.HasError = false;
        etCategoryName.HelperText = "";
        etCategoryName.ErrorText = "";
        etCategoryName.Stroke = Color.FromHex("#776CE6");
    }

    async void BtnEdite_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        int categoryId = (int)getbinding.CommandParameter;
        
        categoryViewModel.CategoryId = categoryId;
        categoryViewModel = await CategoryService!.GetCategory(categoryViewModel.CategoryId);
        entryUpdateName.Text = string.Empty;
        entryUpdateName.Text = categoryViewModel.CatName;
    }

    async void BtnDelete_OnClicked(object? sender, EventArgs e)
    {
        var getbinding = ((Button)sender);
        int categoryId = (int)getbinding.CommandParameter;
        categoryViewModel.CategoryId = categoryId;
        categoryViewModel = await CategoryService!.GetCategory(categoryViewModel.CategoryId);
        
        bool answer = await DisplayAlert("", "Are you Delete "+categoryViewModel.CatName, "Yes", "No");
        if (answer)
        {
            var result =  await CategoryService!.DeleteCategory(categoryViewModel);
            if (result.Successful == false)
            {
                await DisplayAlert("Info Category", $"{result.Message}", "OK");
            }
            else
            {
                CategoryViewModels = (await CategoryService!.GetCategories())!.ToList();
                categoryList.ItemsSource = CategoryViewModels;
            }
        }
    }
    
    public void ErrorMessage() {
        if (string.IsNullOrEmpty(etCategoryName.Text.Trim())) {
            etCategoryName.HasError = true;
            etCategoryName.HelperText = "Enter Category Name";
            etCategoryName.ErrorText = "Please enter Category Name";
            etCategoryName.Stroke = Color.FromHex("#B3261E");
        }
    }


    async void BtUpdate_OnClicked(object? sender, EventArgs e)
    {
        try {
            if (!string.IsNullOrEmpty(etUpdateName.Text.Trim()))
            {
                categoryViewModel.CatName = etUpdateName.Text;
                categoryViewModel.CategoryId = categoryViewModel.CategoryId;
                var result =  await CategoryService!.UpdateCategory(categoryViewModel);
                if (result!.Successful == false)
                {
                    await DisplayAlert("Info Category", $"{result.Message}", "OK");
                }
                else
                {
                    CategoryViewModels = (await CategoryService!.GetCategories())!.ToList();
                    foreach (var Category in CategoryViewModels)
                    {
                        Category.IsEnabledEdit = true;
                        Category.IsEnabledDelete = true;
                        if (_policyEdit)
                        {
                            Category.IsEnabledEdit = false;
                        }
                        if (_policyDelete)
                        {
                            Category.IsEnabledDelete = false;
                        }
                        CategoryViewModels.Append(Category);
                    }
                    categoryList.ItemsSource = CategoryViewModels;
                }
                entryUpdateName.Text = string.Empty;
            }
            else
            {
                etUpdateName.HasError = true;
                etUpdateName.HelperText = "Enter Category Name";
                etUpdateName.ErrorText = "Please enter Category Name";
                etUpdateName.Stroke = Color.FromHex("#B3261E");
            }
        }
        catch {
            await DisplayAlert("Error", "please fill out all required fields", "OK");
        } 
    }

    async void EntryUpdateName_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        etUpdateName.HasError = false;
        etUpdateName.HelperText = "";
        etUpdateName.ErrorText = "";
        etUpdateName.Stroke = Color.FromHex("#776CE6");
    }
}