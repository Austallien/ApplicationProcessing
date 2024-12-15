using Database.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApplicationProcessing.Core.ViewModels.Windows
{
    internal class SelectClientWindowViewModel : INotifyPropertyChanged
    {
        private Action<Person> m_onSelect;

        private Action m_onClose;

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

        private List<string> m_keys = new();

        public List<string> Keys
        {
            get => m_keys;
            set => m_keys = value;
        }

        private List<Person> m_clients;

        public List<Person> Clients
        {
            get => m_clients;
            set
            {
                m_clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        private Person m_selectedClient;

        public Person SelectedClient
        {
            get => m_selectedClient;
            set
            {
                m_selectedClient = value;
                IsSelected = true;
            }
        }

        private bool m_isSelected;

        public bool IsSelected
        {
            get => m_isSelected;
            set
            {
                m_isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public ICommand SearchCommand => new Command(
            () =>
                {
                    if (Keys is null || Keys.Count == 0)
                        return;

                    ReloadData();
                }
            );

        public ICommand CancelCommand => new Command(
            () =>
                {
                    m_onClose.Invoke();
                }
            );

        public ICommand SelectCommand => new Command(
            () =>
                {
                    if (SelectedClient is not null)
                        m_onSelect.Invoke(SelectedClient);
                }
            );

        public SelectClientWindowViewModel(Action<Person> onSelect, Action onClose)
        {
            m_onSelect = onSelect;
            m_onClose = onClose;

            ReloadData();
        }

        public async void ReloadData()
        {
            await Task.Run(() =>
            {
                using (var context = new Database.Core.Context())
                {
                    Clients = context.Persons
                        .Include(p => p.User)
                        .Include(p => p.Role)
                        .AsEnumerable()
                        .Where(item => item.RoleId == (int)Role.Roles.Client
                                && item.Id != Person.Default.Id)
                        .Where(item =>
                        {
                            List<string> keys = new List<string>(Keys);

                            foreach (var key in Keys)
                                if (item.Id.ToString().Contains(key)
                                    || item.User.Username.Contains(key)
                                    || item.FirstName.Contains(key)
                                    || item.MiddleName.Contains(key)
                                    || item.LastName.Contains(key)
                                    || item.Birth.ToString().Contains(key))
                                    keys.Remove(key);

                            if (keys.Count > 0)
                                return false;

                            return true;
                        }).ToList();

                    if (IsSelected)
                        if (Clients.FirstOrDefault(item => item.Id == SelectedClient.Id) is null)
                        {
                            IsSelected = false;
                            SelectedClient = null;
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