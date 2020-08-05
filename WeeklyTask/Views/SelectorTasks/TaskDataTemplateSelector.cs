using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using WeeklyTask.Models;

namespace WeeklyTask.Views.SelectorTasks
{
    public class TaskDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HideTemplate { get; set; }
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate DetailsTemplate { get; set; }
        public String Day { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            //TODO Sort Data by Date and by DayOfWeek Number! Not by Text of the day

            if (String.IsNullOrEmpty(Day))
            {
                if (!String.IsNullOrEmpty(((Item)item).Day))
                {
                    return HideTemplate;
                }
            }
            else if (Day != ((Item)item).Day)
            {
                return HideTemplate;
            }



            switch (Singleton.Instance.CurrentPageConfigurations.FilterTask)
            {
                case FilterTask.Todos:
                    //No requiere evaluación
                    break;
                case FilterTask.Prioritarios:
                    if (((Item)item).IsMain == false)
                    {
                        return HideTemplate;
                    }
                    break;
                case FilterTask.Terminados:
                    if (((Item)item).IsDone == false)
                    {
                        return HideTemplate;
                    }
                    break;
                case FilterTask.Faltantes:
                    if (((Item)item).IsDone == true)
                    {
                        return HideTemplate;
                    }
                    break;
            }

            //if not hidden previously, then it's showed!
            switch (Singleton.Instance.CurrentPageConfigurations.ViewTypeCard)
            {
                case ViewTypeCard.Simple:
                    return NormalTemplate;
                case ViewTypeCard.Detailed:
                    return DetailsTemplate;
            }

            return NormalTemplate;

        }
    }
}
