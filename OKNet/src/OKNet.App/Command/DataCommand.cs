using System;
using System.Windows.Input;

namespace OKNet.App.Command
{
    public class DataCommand<T> : ICommand
    {
        private readonly Action<T> _toExecute;
        private readonly Func<object, bool> _canExecute;

        public DataCommand(Action<T> toExecute, Func<object, bool> canExecute)
        {
            _toExecute = toExecute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            var argument = (T) parameter;
            _toExecute.Invoke(argument);
        }

        public event EventHandler CanExecuteChanged;
    }
}