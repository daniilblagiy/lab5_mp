using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string Greeting => "Welcome to Avalonia!";

        private int count;

        public int Count { 
            get { return count; }
            set {
                if (value != count)
                {
                    count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }

        public void OnClickCommand()
        {
            Count++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
