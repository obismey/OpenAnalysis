using System;
using System.Windows.Input;

namespace AnalysisTesteur.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _command;
        private readonly Action<object> _command2;
        private readonly Func<bool> _canExecute;
        private readonly Func<object,bool> _canExecute2;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;  
            _command = command;
        }
        public DelegateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute2 = canExecute;
            _command2 = command;
        }

        public void Execute(object parameter)
        {
            if (_command != null) { _command(); return; }

            if (_command2 != null) { _command2(parameter); return; }

        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null) return this._canExecute();

            if (_canExecute2 != null) return this._canExecute2(parameter);

            return true;
        }

    }

   
}