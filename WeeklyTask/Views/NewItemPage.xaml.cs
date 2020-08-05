using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeeklyTask.Models;

namespace WeeklyTask.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                IsMain = false,
                HasReminder = false,
                ReminderDate = DateTime.Today
            };


            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (Item.HasReminder)
            {
                //TODO: Make Notification Here if Required
                //Item.ReminderDate.Add(Item.ReminderTime);
            }
            var dia = (int)Item.ReminderDate.DayOfWeek;
            Item.Day = Singleton.Instance.DictionaryInttoDay[dia];
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        #region tapped GestureRecognizers for Changing status
        private void IsMain_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Item.IsMain = !Item.IsMain;
        }

        private void HasReminder_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Item.HasReminder = !Item.HasReminder;
        }
        #endregion


        //unused  //TODO Delete iF not Used
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                Item.Day = (sender as RadioButton).Text;
                if (Item.Day == "Sin Día")
                {
                    Item.Day = null;
                }
            }
        }
    }
}