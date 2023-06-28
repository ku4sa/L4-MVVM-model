using System;
using System.Windows;
using System.Windows.Threading;

namespace L4_
{
    
        public partial class MainWindow : Window
        {
            internal MainVM MainVM { get; set; }
            private int _tick;
            private bool add_proc= false;
            private bool add_mem = false;

        //Увы, XAML не может в таймеры
        private readonly DispatcherTimer timer;
            public MainWindow()
            {
                MainVM = new MainVM();
                InitializeComponent();
                this.DataContext = MainVM;
                _tick = 0;

                //Создаются таймеры и присваиваются им методы
                timer = new();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(FirstTimerTick);
            }
            //Методы вызывают соответсвующую логику из VM и останавливают/запускают таймеры
            private void FirstTimerTick(object sender, EventArgs e)
            {
                
                MainVM.AddToEl();
                MainVM.Active = MainVM.SwitchtoResult();
                MainVM.List = MainVM.SwitchtoNoResult(ref _tick);
                free_memory.Text = (MainVM.All_Memory - MainVM.Use_Memory).ToString();
                Tim.Text = _tick.ToString();
                _tick++;
            }
            private void ChangeMemory_Click(object sender, RoutedEventArgs e)
            {
            if (!add_mem)
                set_memory.Visibility = Visibility.Visible;

            if (add_mem)
            {
                if (set_memory.Text.Length > 0)
                {
                    MainVM.SetMemory(set_memory.Text.ToString());
                    set_memory.Visibility = Visibility.Collapsed;
                }
            }
            add_mem = !add_mem;
          
            }

        private void Create_Procces(object sender, RoutedEventArgs e)
        {
            if (!add_proc)
                Add.Visibility = Visibility.Visible;
          
            if (add_proc)
            {
                if (pr_name.Text.Length * pr_size.Text.Length * pr_wait.Text.Length * pr_run.Text.Length > 0)
                    MainVM.Sort(new SimpleModel(pr_name.Text, pr_size.Text, pr_wait.Text, pr_run.Text));
                Add.Visibility = Visibility.Collapsed;
            } 

            add_proc = !add_proc;
        }
        private void Run(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void Stop(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            MainVM.Clear();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}


