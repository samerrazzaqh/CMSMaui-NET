using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBlazor.Client.Views.Home;


[QueryProperty(nameof(CategoryId), "categoryId")]
public partial class ListPostByCategory : ContentPage
{
    
    public string categoryId { set; get; }
    public string CategoryId {set  { categoryId = Uri.UnescapeDataString(value); } }
    public ListPostByCategory()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        btCID.Text = categoryId;
        
    }
}