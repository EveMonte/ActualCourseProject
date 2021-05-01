using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.ViewModels;

namespace Курсач.Commands
{

    public class OpenRegisterControlCommand : ICommand
    {
        private MainFrameViewModel viewModel;
        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.SelectedViewModel = new RegistrationViewModel();
        }

        public OpenRegisterControlCommand(MainFrameViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
    }
}
