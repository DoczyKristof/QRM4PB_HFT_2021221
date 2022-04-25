using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QRM4PB_HFT_2021221.WPFClient.ViewModels
{
    class MainMenuViewModel
    {
        public ICommand ExitCommand { get; set; }

        public MainMenuViewModel()
        {

            ExitCommand = new RelayCommand(() => Environment.Exit(0));
        }
    }
}
