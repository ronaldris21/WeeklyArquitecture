using System;
using System.Collections.Generic;
using System.Text;

namespace WeeklyTask.Models
{
    public class ViewConfigurations
    {
        public ViewTypeCard ViewTypeCard { get; set; }
        public FilterTask FilterTask { get; set; }
    }

    public enum ViewTypeCard
    {
        Simple, Detailed
    }

    public enum FilterTask
    {
        Todos, Prioritarios, Terminados, Faltantes
    }

}
