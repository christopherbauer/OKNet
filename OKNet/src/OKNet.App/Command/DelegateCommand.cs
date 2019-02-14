using System;
using System.Windows.Input;

namespace OKNet.App.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<object, bool> _canExecute;

        public DelegateCommand(Action action, Func<object, bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}