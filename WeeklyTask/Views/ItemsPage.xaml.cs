using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeeklyTask.Models;
using WeeklyTask.Views;
using WeeklyTask.ViewModels;

namespace WeeklyTask.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        public static ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
            ChangeTitle();
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            //GET THE BINDING CONTEXT WITHOUT CARING ABOUT THE OBJECT ON XAML
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage())); 
            await Task.Delay(300);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }

        private async void ChangeDataTemplate_Clicked(object sender, EventArgs e)
        {
            string answer = await App.Current.MainPage.DisplayActionSheet("Tipo de tarjetas", "Cancel", "", 
                new string[] {
                    Constants.Detailed,
                    Constants.Simple
                }
                );

            bool reload = false;
            switch (answer)
            {
                case Constants.Simple:
                    Singleton.Instance.CurrentPageConfigurations.ViewTypeCard = ViewTypeCard.Simple;
                    reload = true;
                    break;
                case Constants.Detailed:
                    Singleton.Instance.CurrentPageConfigurations.ViewTypeCard = ViewTypeCard.Detailed;
                    reload = true;
                    break;

                default:
                    break;
            }

            if (reload)
                viewModel.ReloadData();
        }

        private async void btn_AllorMain_Clicked(object sender, EventArgs e)
        {
            string answer = await App.Current.MainPage.DisplayActionSheet("Prioridad", "Cancelar", "",
                new string[] {
                    Constants.Todos,
                    Constants.Prioritarios,
                    Constants.Terminados,
                    Constants.Faltantes
                }
                );


            bool reload = false;
            switch (answer)
            {
                case Constants.Todos:
                    Singleton.Instance.CurrentPageConfigurations.FilterTask = FilterTask.Todos;
                    reload = true;
                    break;

                case Constants.Prioritarios:
                    Singleton.Instance.CurrentPageConfigurations.FilterTask = FilterTask.Prioritarios;
                    reload = true;
                    break;


                case Constants.Terminados:
                    Singleton.Instance.CurrentPageConfigurations.FilterTask = FilterTask.Terminados;
                    reload = true;
                    break;

                case Constants.Faltantes:
                    Singleton.Instance.CurrentPageConfigurations.FilterTask = FilterTask.Faltantes;
                    reload = true;
                    break;

                default:
                    break;
            }

            ChangeTitle();


            if (reload)
                viewModel.ReloadData();

        }

        public void ChangeTitle()
        {
            switch (Singleton.Instance.CurrentPageConfigurations.FilterTask)
            {
                case FilterTask.Todos:
                    viewModel.Title = Constants.Todos;
                    break;
                case FilterTask.Prioritarios:
                    viewModel.Title = Constants.Prioritarios;
                    break;
                case FilterTask.Terminados:
                    viewModel.Title = Constants.Terminados;
                    break;
                case FilterTask.Faltantes:
                    viewModel.Title = Constants.Faltantes;
                    break;
                default:
                    viewModel.Title = Constants.Todos;
                    break;
            }
        }
    }
}