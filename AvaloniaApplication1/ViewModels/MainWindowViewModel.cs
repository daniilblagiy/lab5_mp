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
        private bool profile_is_loading = false;
        private bool repos_are_loading = false;

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

        public bool IsProfileLoading
        {
            get { return profile_is_loading; }
            set
            {
                if (value != profile_is_loading)
                {
                    profile_is_loading = value;
                    OnPropertyChanged(nameof(IsProfileLoading));
                }
            }
        }

        public bool IsRepoListLoading
        {
            get { return repos_are_loading; }
            set
            {
                if (value != repos_are_loading)
                {
                    repos_are_loading = value;
                    OnPropertyChanged(nameof(IsRepoListLoading));
                }
            }
        }

        public async void OnSearch()
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

            IsProfileLoading = true;

            var user = await github.User.Get(Username);
            Profile = user;

            IsProfileLoading = false;


            IsRepoListLoading = true;

            var repos = await github.Repository.GetAllForUser(Profile.Login);
            RepoList = repos;

            IsRepoListLoading = false;
        }

        public async void OnView(string repo_name)
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

            var commits = await github.Repository.Commit.GetAll(Profile.Login, repo_name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
