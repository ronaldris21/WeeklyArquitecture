using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WeeklyTask.Models;
using WeeklyTask.Views;
using System.Linq;

namespace WeeklyTask.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { 
            get
            {
                return Services.MockDataStore.items;
            }
            set
            {
                Services.MockDataStore.items = value;
                OnPropertyChanged();
            }
        }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            //Traigo los datos por primera vez
            LoadItemsCommand.Execute(null);

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                if (item.IsMain)
                {
                    Items.Insert(0, item);
                }
                else
                {
                    Items.Add(item);
                }

                await Task.Delay(1000);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                Items = await DataStore.GetItemsAsync(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void IsMainChanged(Item ItemChanged)
        {
            int posX0 = Items.IndexOf(ItemChanged);
            if (ItemChanged.IsMain)
            {
                Items.Move(posX0, 0);
            }
            else
            {
                Items.Move(posX0, Items.Count - 1);
            }
        }

        public void ReInsert_Item(Item data)
        {
            int pos = Items.IndexOf(data);
            Items.RemoveAt(pos);
            Items.Insert(pos, data);
        }

        internal void ReloadData()
        {
            OnPropertyChanged("Items");
            var old = Items;
            Items = null;
            Items = old;
        }
    }
}