using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_
{
    //Основная VM
    internal class MainVM : NotifyBase
    {
            private int all_memory; //выделяемая память
            public int All_Memory
            {
                get
                {
                    return all_memory;
                }
                set
                {
                    all_memory = value;
                    OnPropertyChanged("All_Memory");
                }
            }
            private int use_memory;
            public int Use_Memory
            {
                get
                {
                    return use_memory;
                }
                set
                {
                    use_memory = value;
                    OnPropertyChanged("Use_Memory");
                }
            }
        
        //список задач
        private ObservableCollection<SimpleModel> _list;
        public ObservableCollection<SimpleModel> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }

        //Коллекция с выполняемыми задачами 
        private ObservableCollection<SimpleModel> _active;
        public ObservableCollection<SimpleModel> Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }
        //Коллекция с завершенными задачами
        private ObservableCollection<SimpleModel> _results;
        public ObservableCollection<SimpleModel> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }
        private ObservableCollection<SimpleModel> _noresults;
        public ObservableCollection<SimpleModel> No_Results
        {
            get
            {
                return _noresults;
            }
            set
            {
                _noresults = value;
                OnPropertyChanged("No_Results");
            }
        }


        //При создании, коллекции пустые
        public MainVM()
        {
            All_Memory = 100;
            Use_Memory = 0;//надо где-то задать значение для memory 
            List = new ObservableCollection<SimpleModel>();
            for (int i = 0; i < 10; i++)
            {
                var A = new SimpleModel(i.ToString()+"procх1-"+i.ToString(), 12*i, 15 + i, 5 + i);
                var B = new SimpleModel(i.ToString()+"procx02"+i.ToString(), 2*i, 15 + i, 7 + i);
                List.Add(A);
                List.Add(B);

            }
            Active = new ObservableCollection<SimpleModel>();
            Results = new ObservableCollection<SimpleModel>();
            No_Results = new ObservableCollection<SimpleModel>();
        }

     
        //Метод для добавления в выполняемые коллекции
        public bool AddToEl()
        {
            if (List.Count > 0)
            {
                while (All_Memory - (Use_Memory + List.First().Size)>0)
                {

                    var el = List.First();
                    List.Remove(el);
                    Use_Memory += el.Size;
                    Active.Add(el);
                }
                return true;
            }
                return false;
            
        }


        //Метод для переноса элемента в завершенные коллекции
        public ObservableCollection<SimpleModel> SwitchtoResult()
        {
            ObservableCollection<SimpleModel> a = new();
            if (Active.Count > 0)
            {
                for (int i =0; i<Active.Count; i++)
                {
                    var el = Active.ToArray()[i];
                    if (el.Do_Time <= 0)
                    {
                        //Active.Remove(el);
                        Results.Add(el);
                        Use_Memory -= el.Size;
                    }
                    else
                    {
                        el.Do_Time--;
                        a.Add(el);
                    }
                       
                }
                return a;
            }
            return a;
        }
        public ObservableCollection<SimpleModel> SwitchtoNoResult(ref int time)
        {
            ObservableCollection<SimpleModel> a = new();
            if (List.Count > 0)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    var el = List.ToArray()[i];
                    if (el.Max_Time == 0)
                    {
                       No_Results.Add(el);
                    }
                    else
                    {
                        el.Max_Time--;
                        a.Add(el);
                    }

                }
                return a;
            }
            return a;
        }
     
        public void SetMemory(string a)
        {
            try { All_Memory= Int32.Parse(a); }
            catch 
            { 
                All_Memory = 100; 
            }
        }
        public void Clear()
        {
            List.Clear();
            Active.Clear();
            No_Results.Clear();
            Results.Clear();
            Use_Memory = 0;
        }

        public void Sort(SimpleModel a)
        {
            SimpleModel el;
            int i;
            for(i =0; i<List.Count; i++)
            {
                 el = List.ToArray()[i];
                if (el.Max_Time>=a.Max_Time)
                {
                    break;
                }
            }
            List.Insert(i, a);
        }

    }
}
