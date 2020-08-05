using Xamarin.Forms;

namespace WeeklyTask.Interfaces
{
    public interface ILodingPageService
    {
        void InitLoadingPage(ContentPage loadingIndicatorPage = null);
        void ShowLoadingPage();
        void HideLoadingPage();
    }
}
