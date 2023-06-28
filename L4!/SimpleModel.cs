using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4_
{
    //имя, размер (в байтах), максимально допустимое  время начала выполнения задачи, длительность выполнения.
    internal class SimpleModel : NotifyBase
    {
        private string _name; //имя задачи
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private int _size; //размер задачи 
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }
        private int _mtime; //максимальное время начала
        public int Max_Time
        {
            get
            {
                return _mtime;
            }
            set
            {
                _mtime = value;
                OnPropertyChanged("Max_Time");
            }
        }
        private int _dotime; // время выполнения
        public int Do_Time
        {
            get
            {
                return _dotime;
            }
            set
            {
                _dotime = value;
                OnPropertyChanged("Do_Time");
            }
        }
        public SimpleModel(string name, int s, int mt, int dt)
        {
            Name = name;
            Size = s;
            Max_Time = mt;
            Do_Time = dt;
        }
        public SimpleModel(string name, string s, string mt, string dt)
        {
            Name = name;
            try { Size = Int32.Parse(s); }
            catch { Size = 5; }
            try { Max_Time = Int32.Parse(mt); }
            catch { Max_Time = 10; }
            try { Do_Time = Int32.Parse(s); }
            catch { Do_Time= 7; }
        }

        public override string ToString()
        {
            return string.Format($"{Name, -10} {Size, 4} байт {Max_Time, 3} cек. {Do_Time, 3} сек.");
        }
    }
}
