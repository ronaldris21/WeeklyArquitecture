using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeeklyTask.Models;
using WeeklyTask.Views.SelectorTasks;

namespace WeeklyTask
{
    public class Singleton
    {
        #region Singleton

        private static Singleton _instance;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Singleton();

                return _instance;
            }
        }

        #endregion



        public ViewConfigurations CurrentPageConfigurations { get; set; }
        public Views.SelectorTasks.TaskDataTemplateSelector TaskSelector { get; set; }

        //TODO: Check Dictionaries if used or get rid off them
        public Dictionary<string,int> DictionaryDaytoInt { get; set; }
        public Dictionary<int,String> DictionaryInttoDay { get; set; }
        public Singleton()
        {
            var dia = DateTime.Now;

            dia = new DateTime();

            CurrentPageConfigurations = new ViewConfigurations()
            {
                ViewTypeCard = ViewTypeCard.Simple,
                FilterTask = FilterTask.Todos
            };

            DictionaryDaytoInt = new Dictionary<string, int>();
            DictionaryDaytoInt.Add("Domingo", 0);
            DictionaryDaytoInt.Add("Lunes", 1);
            DictionaryDaytoInt.Add("Martes", 2);
            DictionaryDaytoInt.Add("Miércoles", 3);
            DictionaryDaytoInt.Add("Jueves", 4);
            DictionaryDaytoInt.Add("Viernes", 5);
            DictionaryDaytoInt.Add("Sábado", 6);

            DictionaryInttoDay = new Dictionary<int, string>();
            DictionaryInttoDay.Add(0,"Domingo");
            DictionaryInttoDay.Add(1,"Lunes");
            DictionaryInttoDay.Add(2,"Martes");
            DictionaryInttoDay.Add(3,"Miércoles");
            DictionaryInttoDay.Add(4,"Jueves");
            DictionaryInttoDay.Add(5,"Viernes");
            DictionaryInttoDay.Add(6,"Sábado");


            TaskSelector = new Views.SelectorTasks.TaskDataTemplateSelector();

        }


    }
}
