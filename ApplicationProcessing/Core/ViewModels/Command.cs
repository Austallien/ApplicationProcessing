using System;
using System.Windows.Input;

namespace ApplicationProcessing.Core.ViewModels
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action m_action;

        public Command(Action action)
        {
            m_action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            m_action.Invoke();
        }
    }
}