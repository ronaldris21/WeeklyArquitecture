using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Plugin.CurrentActivity;
using System;
using WeeklyTask.Droid.Interfaces;
using WeeklyTask.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFPlatform = Xamarin.Forms.Platform.Android.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(LoadingServiceDroid))]
namespace WeeklyTask.Droid.Interfaces
{
    public class LoadingServiceDroid : WeeklyTask.Interfaces.ILodingPageService
    {
        private Android.Views.View _nativeView;
        private Dialog _dialog;
        private bool _isInitialized;

        public void InitLoadingPage(ContentPage loadingIndicatorPage = null)
        {
            //check if parameter is available
            if (loadingIndicatorPage == null)
            {
                //Build loading with native base 
                loadingIndicatorPage.Parent = Xamarin.Forms.Application.Current.MainPage;
                loadingIndicatorPage.Layout(new Rectangle(0, 0, Xamarin.Forms.Application.Current.MainPage.Width, Xamarin.Forms.Application.Current.MainPage.Height));
                var renderer = loadingIndicatorPage.GetOrCreateRenderer();
                _nativeView = renderer.View;

                
                _dialog = new Dialog(CrossCurrentActivity.Current.Activity);
                _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);  ///TRY SWIPEtoDISSMIS
                _dialog.SetCancelable(true); // It can be cancelled by presioning Back Button  //TODO Set Cancellable
                _dialog.SetContentView(_nativeView);

                
                Window window = _dialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.ClearFlags(WindowManagerFlags.DimBehind); //Reduce Bright(dim) all content under the _dialog
                window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                _isInitialized = true;
            }
        }
        public void HideLoadingPage()
        {
            try //Try catch in case _dialog is null
            {
                _dialog.Hide();
            }
            catch (Exception) { }
        }



        public void ShowLoadingPage()
        {
            if (!_isInitialized)
                InitLoadingPage(new ILoadingPage()); // set the default page

            // showing the native loading page
            _dialog.Show();
        }
    }


    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);  //Get
            if (renderer == null) 
            {//Create
                renderer = XFPlatform.CreateRendererWithContext(bindable, CrossCurrentActivity.Current.Activity);
                XFPlatform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}