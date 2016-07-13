using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataGridCustomDemo
{
    public class Student : INotifyPropertyChanged
    {
        public string StudentNumber { set; get; }
        public DateTime? EnrollmentTime { set; get; }
        public string Name { set; get; }
        public string ClassNo { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
