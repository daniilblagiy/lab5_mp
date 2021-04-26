using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string Greeting => "Welcome to Avalonia!";

        private string username;
        private string entered_value;

        public string Username { 
            get { return username; }
            set {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string EnteredValue
        {
            get { return entered_value; }
            set
            {
                if (value != entered_value)
                {
                    entered_value = value;
                    OnPropertyChanged(nameof(EnteredValue));
                }
            }
        }

        public async void OnSearch()
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
            var user = await github.User.Get(Username);

            EnteredValue = user.Followers + " folks love " + Username + "!";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
