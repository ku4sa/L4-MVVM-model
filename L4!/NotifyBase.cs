using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_
{
    //Служебный класс, обеспечивающий Data Binding для всех классов моделей и VM

    internal class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propname)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propname));
        }
    }
}
