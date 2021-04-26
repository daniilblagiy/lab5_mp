using AvaloniaApplication1.Models;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string username;
        private User profile;
        private IReadOnlyList<Octokit.Repository> repo_list;

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

        public User Profile
        {
            get { return profile; }
            set
            {
                if (value != profile)
                {
                    profile = value;
                    OnPropertyChanged(nameof(Profile));
                }
            }
        }

        public IReadOnlyList<Octokit.Repository> RepoList
        {
            get { return repo_list; }
            set
            {
                if (value != repo_list)
                {
                    repo_list = value;
                    OnPropertyChanged(nameof(RepoList));
                }
            }
        }

        public async void OnSearch()
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));
            
            //GeneralProfileInfo = "Loading profile...";

            var user = await github.User.Get(Username);
            Profile = user;

            /*GeneralProfileInfo = 
                user.Name + " (" + user.Login + ")" + "\n" +
                "Bio: " + user.Bio + "\n" +
                "Location: " + user.Location + "\n" +
                "Followers: " + user.Followers + "\n";*/

            var repos = await github.Repository.GetAllForUser(Profile.Login);
            RepoList = repos;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
