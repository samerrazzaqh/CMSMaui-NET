using CMSBlazor.Client.Helpers;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;
using The49.Maui.BottomSheet;

using Microsoft.Maui;
using System.Drawing;

#if IOS
using UIKit;
using Foundation;
#endif

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace CMSBlazor.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureSyncfusionCore()
            .ConfigureSyncfusionToolkit()
            .UseBottomSheet()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "GoogleMaterialFont");
            });
        builder.ConfigureSyncfusionCore();

        FormHandler.RemoveBorders();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    
    
   
    
    
    
}