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
        private IReadOnlyList<Octokit.GitHubCommit> commits;
        private bool profile_is_loading = false;
        private bool repos_are_loading = false;
        private bool commits_are_loading = false;
        private bool repos_fetched = false;
        private bool commits_fetched = false;
        private string repo_to_view = "";

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

        public IReadOnlyList<Octokit.GitHubCommit> Commits
        {
            get { return commits; }
            set
            {
                if (value != commits)
                {
                    commits = value;
                    OnPropertyChanged(nameof(Commits));
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

        public bool AreCommitsLoading
        {
            get { return commits_are_loading; }
            set
            {
                if (value != commits_are_loading)
                {
                    commits_are_loading = value;
                    OnPropertyChanged(nameof(AreCommitsLoading));
                }
            }
        }

        public bool ReposFetched
        {
            get { return repos_fetched; }
            set
            {
                if (value != repos_fetched)
                {
                    repos_fetched = value;
                    OnPropertyChanged(nameof(ReposFetched));
                }
            }
        }

        public bool CommitsFetched
        {
            get { return commits_fetched; }
            set
            {
                if (value != commits_fetched)
                {
                    commits_fetched = value;
                    OnPropertyChanged(nameof(CommitsFetched));
                }
            }
        }

        public string RepoToView
        {
            get { return repo_to_view; }
            set
            {
                if (value != repo_to_view)
                {
                    repo_to_view = value;
                    OnPropertyChanged(nameof(RepoToView));
                }
            }
        }

        public async void OnSearch()
        {
            ReposFetched = false;
            CommitsFetched = false;
            RepoList = null;
            Commits = null;
            Profile = null;

            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

            IsProfileLoading = true;

            var user = await github.User.Get(Username);
            Profile = user;

            IsProfileLoading = false;


            IsRepoListLoading = true;

            var repos = await github.Repository.GetAllForUser(Profile.Login);
            RepoList = repos;

            IsRepoListLoading = false;
            ReposFetched = true;
        }

        public async void OnView(string repo_name)
        {
            var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

            AreCommitsLoading = true;

            var commits = await github.Repository.Commit.GetAll(Profile.Login, repo_name);
            Commits = commits;

            AreCommitsLoading = false;
            RepoToView = repo_name;
            CommitsFetched = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
