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
    internal class OperatorApplicationPageViewModel : INotifyPropertyChanged
    {
        private NavigationService m_navigationService;

        // Summary:
        //      Displaying application

        #region Application

        private Application m_application;

        public Application Application
        {
            get => m_application;
            set
            {
                m_application = value;
                OnPropertyChanged(nameof(Application));
            }
        }

        #endregion Application

        // Summary:
        //      Editing application

        #region EditingApplication

        private Application m_editingApplication;

        public Application EditingApplication
        {
            get => m_editingApplication;
            set
            {
                m_editingApplication = value;
                OnPropertyChanged(nameof(EditingApplication));
            }
        }

        #endregion EditingApplication

        // Summary:
        //      Is application editing

        #region IsEditMode

        private bool m_isEditMode = false;

        public bool IsEditMode
        {
            get => m_isEditMode;
            set
            {
                m_isEditMode = value;
                IsCancellationEnabled = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        #endregion IsEditMode

        // Summary:
        //      Is application creating (and editing at the same time)

        #region IsCreation

        private bool m_isCreation = false;

        public bool IsCreation
        {
            get => m_isCreation;
            set
            {
                m_isCreation = value;
                IsEditMode = value;
                IsCancellationEnabled = false;
                OnPropertyChanged(nameof(IsCreation));
            }
        }

        #endregion IsCreation

        // Summary:
        //      Is editing cancellation enabled

        #region IsCancellationEnabled

        private bool m_isCancellationEnabled;

        public bool IsCancellationEnabled
        {
            get => m_isCancellationEnabled;
            set
            {
                m_isCancellationEnabled = value;
                OnPropertyChanged(nameof(IsCancellationEnabled));
            }
        }

        #endregion IsCancellationEnabled

        // Summary:
        //      Is client selected

        #region IsClientSelected

        private bool m_isClientSelected;

        public bool IsClientSelected
        {
            get => m_isClientSelected;
            set
            {
                m_isClientSelected = value;
                OnPropertyChanged(nameof(IsClientSelected));
            }
        }

        #endregion IsClientSelected

        public ICommand SelectClientCommand => new Command(
            () =>
                {
                    Views.Windows.SelectClientWindow window = new();
                    var result = window.ShowDialog();

                    if (result is true)
                    {
                        EditingApplication.Client = window.Client;
                        EditingApplication.ClientId = window.Client.Id;
                        IsClientSelected = true;
                        OnPropertyChanged(nameof(EditingApplication));
                    }
                }
            );

        public ICommand RemoveClientCommand => new Command(
            () =>
                {
                    EditingApplication.Client = null;
                    EditingApplication.ClientId = Person.Default.Id;
                    IsClientSelected = false;
                    OnPropertyChanged(nameof(EditingApplication));
                }
            );

        public ICommand SelectTagsCommand => new Command(
            () =>
                {
                    Views.Windows.SelectTagWindow window = new(EditingApplication.Tags?.ToList() ?? new List<Tag>());
                    var result = window.ShowDialog();

                    if (result is true)
                    {
                        EditingApplication.Tags = window.SelectedTags;
                        OnPropertyChanged(nameof(EditingApplication));
                    }
                }
            );

        public ICommand BackCommand => new Command(
            () =>
                {
                    m_navigationService.GoBack();
                    m_navigationService.RemoveBackEntry();
                }
            );

        public ICommand EditCommand => new Command(
            async () =>
                {
                    if (IsCreation)
                        return;

                    if (IsEditMode)
                        return;

                    IsEditMode = true;
                    EditingApplication = await new Database.Core.Context().Applications
                                                                            .AsNoTracking()
                                                                            .Include(item => item.Client)
                                                                            .Include(item => item.Tags)
                                                                            .FirstAsync(item => item.Id == Application.Id);

                    if (EditingApplication.Client.Id != Person.Default.Id)
                        IsClientSelected = true;
                }
            );

        public ICommand CancelCommand => new Command(
            () =>
                {
                    if (IsCreation)
                        return;

                    if (!IsEditMode)
                        return;

                    IsEditMode = false;
                    EditingApplication = null;
                }
            );

        public ICommand ApplyCommand => new Command(
            async () =>
                {
                    if (IsCreation)
                    {
                        using (var context = new Database.Core.Context())
                        {
                            try
                            {
                                EditingApplication.Created = DateTime.UtcNow;
                                EditingApplication.Updated = EditingApplication.Created;

                                if (EditingApplication.Title.Length == 0 || EditingApplication.Content.Length == 0)
                                    return;

                                EditingApplication.Client = null;
                                EditingApplication.ClientId = EditingApplication.ClientId == 0 ? Person.Default.Id : EditingApplication.ClientId;
                                EditingApplication.StatusId = (long)Status.Statuses.New;

                                EditingApplication.Tags = EditingApplication.Tags is { } ? context.Tags.Where(item => EditingApplication.Tags.Select(item => item.Id).Contains(item.Id)).ToList()
                                                                                         : new List<Tag>();

                                await context.Applications.AddAsync(EditingApplication);

                                await context.SaveChangesAsync();
                            }
                            catch { }
                        }

                        await HistoryService.Add(EditingApplication, Operation.Operations.Create);

                        IsCreation = false;
                        await ReloadData(EditingApplication.Id);
                        EditingApplication = null;
                    }
                    else if (IsEditMode)
                    {
                        using (var context = new Database.Core.Context())
                        {
                            try
                            {
                                if (EditingApplication.Client is null)
                                    EditingApplication.Client = Person.Default;

                                // Stop command executing if nothing has been changed
                                if (Application.Title.Equals(EditingApplication.Title)
                                    && Application.Content.Equals(EditingApplication.Content)
                                    && Application.ClientId.Equals(EditingApplication.ClientId)
                                    && Application.OperatorId.Equals(EditingApplication.OperatorId)
                                    && Application.StatusId.Equals(EditingApplication.StatusId)
                                    && new Func<bool>(
                                        () =>
                                        {
                                            var oldTags = Application.Tags.Select(item => item.Id);
                                            var tags = EditingApplication.Tags.Select(item => item.Id);

                                            for (int i = 0; i < tags.Count(); i++)
                                                if (oldTags.ElementAt(i) != tags.ElementAt(i))
                                                    return false;
                                            return true;
                                        }).Invoke())
                                    return;

                                EditingApplication.Updated = DateTime.UtcNow;
                                context.Applications.Update(EditingApplication);

                                await context.SaveChangesAsync();
                            }
                            catch { }
                        }

                        // Passing application before changes
                        await HistoryService.Add(Application, Operation.Operations.Update);

                        IsEditMode = false;
                        await ReloadData(EditingApplication.Id);
                        EditingApplication = null;
                    }
                }
            );

        public OperatorApplicationPageViewModel(NavigationService navigationService, Application application = null)
        {
            m_navigationService = navigationService;

            if (application is null || application.Id == 0)
            {
                EditingApplication = Application.GetBlank();
                IsCreation = true;
            }
            else
                ReloadData(application.Id);
        }

        private async Task<bool> ReloadData(long? applicationId)
        {
            if (applicationId is null && Application.Id == 0 || applicationId == 0 && Application.Id == 0)
                return false;

            using (var context = new Database.Core.Context())
            {
                Application = await context.Applications
                    .Include(item => item.Client)
                    .Include(item => item.Client.User)
                    .Include(item => item.Tags)
                    .Include(item => item.Status)
                    .Include(item => item.History)
                    .Include(item => (item.History as History).Operation)
                    .FirstOrDefaultAsync(item => item.Id == (applicationId ?? Application.Id));
            }

            return true;
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