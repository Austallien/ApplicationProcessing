using ApplicationProcessing.Services;
using Database.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ApplicationProcessing.Core.ViewModels.Pages
{
    internal class OperatorMainPageViewModel : INotifyPropertyChanged
    {
        private NavigationService m_navigationService;

        // Summary:
        //      Applications

        #region Applications

        private IList<Application> m_applications;

        public IList<Application> Applications
        {
            get => m_applications;
            set
            {
                m_applications = value;
                OnPropertyChanged(nameof(Applications));
            }
        }

        #endregion Applications

        // Summary:
        //      Selected application

        #region SelectedApplication

        private Application m_selectedApplication;

        public Application SelectedApplication
        {
            get => m_selectedApplication;
            set
            {
                m_selectedApplication = value;
                IsApplicationSelected = true;
                OnPropertyChanged(nameof(SelectedApplication));
            }
        }

        #endregion SelectedApplication

        // Summary:
        //      Is application selected

        #region IsApplicationSelected

        private bool m_isApplicationSelected;

        public bool IsApplicationSelected
        {
            get => m_isApplicationSelected;
            set
            {
                m_isApplicationSelected = value;
                OnPropertyChanged(nameof(IsApplicationSelected));
            }
        }

        #endregion IsApplicationSelected

        // Summary:
        //      Key string

        #region KeyString

        private string m_keyString;

        public string KeyString
        {
            get => m_keyString;
            set
            {
                m_keyString = value;
                Keys = m_keyString.Split(' ').ToList();
            }
        }

        #endregion KeyString

        // Summary:
        //      Keys

        #region Keys

        private List<string> m_keys = new();

        public List<string> Keys
        {
            get => m_keys;
            set => m_keys = value;
        }

        #endregion Keys

        public ICommand CreateCommand => new Command(
            () =>
                {
                    m_navigationService.Navigate(new Core.Views.Pages.OperatorApplicationPage(new Application()));
                }
            );

        public ICommand OpenCommand => new Command(
            () =>
                {
                    if (IsApplicationSelected)
                        m_navigationService.Navigate(new Core.Views.Pages.OperatorApplicationPage(SelectedApplication));
                }
            );

        public ICommand DeleteCommand => new Command(
            async () =>
                {
                    using (var context = new Database.Core.Context())
                    {
                        if (IsApplicationSelected)
                        {
                            var application = context.Applications.Find(SelectedApplication.Id);

                            if (application is not null)
                            {
                                application.Updated = DateTime.UtcNow;
                                application.StatusId = (long)Status.Statuses.Cancelled;

                                await context.SaveChangesAsync();

                                await HistoryService.Add(application, Operation.Operations.Delete);

                                ReloadData();
                            }
                        }
                    }
                }
            );

        public ICommand SearchCommand => new Command(
            () =>
                {
                    ReloadData();
                }
            );

        public OperatorMainPageViewModel(NavigationService navigationService)
        {
            m_navigationService = navigationService;

            ReloadData();
        }

        private async void ReloadData()
        {
            await Task.Run(() =>
            {
                using (var context = new Database.Core.Context())
                {
                    if (Keys.Count > 0)
                        Applications = context.Applications
                            .AsNoTracking()
                            .Include(item => item.Client)
                            .Include(item => item.Client.User)
                            .Include(item => item.Status)
                            .AsEnumerable()
                            .Where(item => item.StatusId != (long)Status.Statuses.Cancelled)
                            .Where(item =>
                            {
                                List<string> keys = new List<string>(Keys);

                                foreach (var key in Keys)
                                    if (item.Id.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)
                                        || item.Title.Contains(key, StringComparison.OrdinalIgnoreCase)
                                        || item.Created.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)
                                        || item.Updated.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)
                                        || item.Status.Name.Contains(key, StringComparison.OrdinalIgnoreCase))
                                        keys.Remove(key);

                                if (keys.Count > 0)
                                    return false;

                                return true;
                            })
                            .ToList();
                    else
                        Applications = context.Applications
                        .AsNoTracking()
                        .Include(item => item.Client)
                        .Include(item => item.Client.User)
                        .Include(item => item.Status)
                        .Where(item => item.StatusId != (long)Status.Statuses.Cancelled)
                        .ToList();

                    if (IsApplicationSelected
                        && Applications.FirstOrDefault(item => item.Id != SelectedApplication.Id) is null)
                    {
                        IsApplicationSelected = false;
                        SelectedApplication = null;
                    }
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}