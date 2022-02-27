using System;
using System.Windows.Input;

namespace WpfApp1.Command
{
    class MyCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;


        public MyCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;

            }
            else
            {
                return canExecute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {

                CommandManager.RequerySuggested += value;

            }
            remove
            {

                CommandManager.RequerySuggested -= value;

            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
