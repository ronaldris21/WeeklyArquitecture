using System;

namespace WeeklyTask.Models
{
    public class Item : ObservableObject
    {
        private string id;
        public string Id    
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
        private bool isDone;
        public bool IsDone
        {
            get { return isDone; }
            set { SetProperty(ref isDone, value); }
        }
        private bool isMain;
        public bool IsMain
        {
            get { return isMain; }
            set { SetProperty(ref isMain, value); }
        }

        private bool _hasReminder;
        public bool HasReminder
        {
            get { return _hasReminder; }
            set { SetProperty(ref _hasReminder, value); }
        }

        private string day;
        public string Day
        {
            get { return day; }
            set { SetProperty(ref day, value); }
        }

        //TODO: GENERALIZAR LA FORMA EN QUE ELIJO EL D[IA!!
        private TimeSpan remindertime;
        public TimeSpan ReminderTime
        {
            get { return remindertime; }
            set { SetProperty(ref remindertime, value); }
        }

        private DateTime reminderdate = DateTime.Now;
        public DateTime ReminderDate
        {
            get { return reminderdate; }
            set { SetProperty(ref reminderdate, value); }
        }


        private Xamarin.Forms.Command _isMainCommand;
        public Xamarin.Forms.Command IsMainCommand
        {
            get => _isMainCommand ?? (_isMainCommand = new Xamarin.Forms.Command(IsMainMethod));
        }
        private void IsMainMethod(object obj)
        {
            IsMain = !IsMain;
            if (Singleton.Instance.CurrentPageConfigurations.FilterTask == FilterTask.Prioritarios && IsMain == false)
            {
                Views.ItemsPage.viewModel.ReInsert_Item(this); 
            }
            Views.ItemsPage.viewModel.IsMainChanged(this);
        }


        private Xamarin.Forms.Command _isDoneCommand;
        public Xamarin.Forms.Command IsDoneCommand
        {
            get => _isDoneCommand ?? (_isDoneCommand = new Xamarin.Forms.Command(IsDoneMethod));
        }
        private void IsDoneMethod(object obj)
        {
            //TODO: Maybe put this at the end
            IsDone = !IsDone;
            if (Singleton.Instance.CurrentPageConfigurations.FilterTask == FilterTask.Terminados && IsDone == false)
            {
                Views.ItemsPage.viewModel.ReInsert_Item(this);
            }
            if (Singleton.Instance.CurrentPageConfigurations.FilterTask == FilterTask.Faltantes && IsDone == true)
            {
                Views.ItemsPage.viewModel.ReInsert_Item(this);
            }
        }




    }

}