using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QRM4PB_HFT_2021221.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ICommand CinemaCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand MovieCommand { get; set; }
        public ICommand NonCrudCommand { get; set; }
        public ICommand ExiCommand { get; set; }

        public MainWindowViewModel()
        {
            #region Commands

            CinemaCommand = new RelayCommand
                    (() =>
                    {
                        _ = new CinemaWindow().ShowDialog();
                    }
                    );

            RoomCommand = new RelayCommand
                    (() =>
                    {
                        _ = new RoomWindow().ShowDialog();
                    }
                    );

            MovieCommand = new RelayCommand
                    (() =>
                    {
                        _ = new MovieWindow().ShowDialog();
                    }
                    );

            NonCrudCommand = new RelayCommand
                    (() =>
                    {
                        _ = new NonCrudWindow().ShowDialog();
                    }
                    );

            ExiCommand = new RelayCommand
                    (() =>
                    {
                        Environment.Exit(0);
                    }
                    );

            #endregion
        }
    }
}
