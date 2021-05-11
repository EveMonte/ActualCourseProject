using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Курсач.ViewModels;

namespace Курсач.Commands
{

    class OpenWorkFrameCommand : ICommand
    {
        Action<object> execute;
        Func<object, bool> canExecute;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (execute != null)
                execute(parameter);
        }

        public OpenWorkFrameCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            execute = executeAction;
            canExecute = canExecuteFunc;

        }
    }
}
