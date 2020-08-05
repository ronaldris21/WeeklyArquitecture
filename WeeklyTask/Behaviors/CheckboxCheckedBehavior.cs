using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeeklyTask.Behaviors
{
    public class CheckboxCheckedBehavior : Behavior<CheckBox>
    {
        protected override void OnDetachingFrom(CheckBox bindable)
        {
            bindable.CheckedChanged -= Bindable_CheckedChanged;
            bindable.Focused -= Bindable_Focused;
            base.OnDetachingFrom(bindable);
        }
        protected override void OnAttachedTo(CheckBox bindable)
        {
            bindable.CheckedChanged += Bindable_CheckedChanged;
            bindable.Focused += Bindable_Focused;
            bindable.PropertyChanged += Bindable_PropertyChanged;
            base.OnAttachedTo(bindable);
        }

       
        //ALL UNUSED
        
        private void Bindable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine(((CheckBox)sender).ClassId + "   " + e.PropertyName);
        }

        private void Bindable_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //Console.WriteLine(e.Value);
            //Console.WriteLine(((CheckBox)sender).IsChecked);
            ////((CheckBox)sender).IsChecked = e.Value;
            ////Views.ItemsPage.viewModel.Reordenar();
        }
        private void Bindable_Focused(object sender, FocusEventArgs e)
        {
            //Views.ItemsPage.viewModel.Reordenar();
        }
    }
}
