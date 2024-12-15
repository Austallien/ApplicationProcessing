using Database.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ApplicationProcessing.Core.ViewModels.Windows
{
    internal class SelectTagWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Represents tag with selection functionality
        /// </summary>
        public class SelectTag
        {
            public bool IsSelected { get; set; }
            public Tag Tag { get; set; }
        }

        private Action m_onCancel;

        private Action<List<Tag>> m_onConfirm;

        // Summary:
        //      Collection of tags

        #region SelectedTags

        private List<SelectTag> m_selectedTags;

        public List<SelectTag> SelectedTags
        {
            get => m_selectedTags;
            set
            {
                m_selectedTags = value;
                OnPropertyChanged(nameof(SelectedTags));
            }
        }

        #endregion SelectedTags

        public ICommand CancelCommand => new Command(
            () =>
                {
                    m_onCancel.Invoke();
                }
            );

        public ICommand ConfirmCommand => new Command(
            () =>
                {
                    List<Tag> tags = SelectedTags.Where(item => item.IsSelected).Select(item => item.Tag).ToList();
                    m_onConfirm.Invoke(tags);
                }
            );

        public SelectTagWindowViewModel(List<Tag> selectedTags, Action onCancel, Action<List<Tag>> onConfirm)
        {
            m_onCancel = onCancel;
            m_onConfirm = onConfirm;

            using (var context = new Database.Core.Context())
            {
                SelectedTags = context.Tags.Where(item => item.Id != (long)Tag.Tags.Null)
                                           .Select(item =>
                                                        new SelectTag
                                                        {
                                                            IsSelected = selectedTags.Select(item => item.Id).Contains(item.Id),
                                                            Tag = item
                                                        }).ToList();
            }
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