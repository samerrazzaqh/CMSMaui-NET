using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace CMSBlazor.Client;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    ////Swipe NavAndStatusBar ----------------------------------
    public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);

        int uiOptions = (int)Window.DecorView.SystemUiVisibility;

        //uiOptions |= (int)SystemUiFlags.LayoutStable;
        //uiOptions |= (int)SystemUiFlags.LayoutHideNavigation;
        //uiOptions |= (int)SystemUiFlags.LayoutFullscreen;

        uiOptions |= (int)SystemUiFlags.HideNavigation;
        uiOptions |= (int)SystemUiFlags.Fullscreen;
        uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

        Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        //Color-----------------
        //Window.SetStatusBarColor(Android.Graphics.Color.Rgb(182, 156, 104));
        //Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(182, 156, 104));






        //var uiModeFlags = Application.BaseContext.Resources.Configuration.UiMode & UiMode.NightMask;
        //switch (uiModeFlags)
        //{
        //    case UiMode.NightYes:
        //        // dark mode
        //        SetTheme(Resource.Style.Base_Theme_Material3_Dark);
        //        Window.SetStatusBarColor(Android.Graphics.Color.Rgb(30, 30, 30));
        //        Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(30, 30, 30));
        //        break;
        //    case UiMode.NightNo:
        //        // default mode
        //        SetTheme(Resource.Style.Base_Theme_Material3_Light);
        //        Window.SetStatusBarColor(Android.Graphics.Color.Rgb(250, 250, 250));
        //        Window.SetNavigationBarColor(Android.Graphics.Color.Rgb(250, 250, 250));


        //        uiOptions |= (int)SystemUiFlags.LightStatusBar;
        //        uiOptions |= (int)SystemUiFlags.LightNavigationBar;
        //        Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;

        //        break;
        //    default:
        //        // default mode
        //        break;
        //}




    }
}