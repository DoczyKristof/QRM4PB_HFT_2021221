using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QRM4PB_HFT_2021221.WPFClient
{
    class CinemaWindowViewModel : ObservableRecipient
    {
        public CinemaWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Cinemas = new RestCollection<Cinema>("http://localhost:20463/", "cinema");

                #region Commands

                AddCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Add(new Cinema()
                        {
                            Name = SelectedCinema.Name
                        });
                    });
                DeleteCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Delete(SelectedCinema.Id);
                        _selectedCinema = null;
                        (EditCinemaCommand as RelayCommand).NotifyCanExecuteChanged();
                    }, () => SelectedCinema != null
                    );
                EditCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Update(SelectedCinema);
                    }, () => SelectedCinema != null
                    );

                SelectedCinema = new Cinema();

                #endregion

            }
        }

        #region Properties

        public RestCollection<Cinema> Cinemas { get; set; }

        public ICommand AddCinemaCommand { get; set; }
        public ICommand DeleteCinemaCommand { get; set; }
        public ICommand EditCinemaCommand { get; set; }

        private Cinema _selectedCinema;
        public Cinema SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                if (value != null)
                {
                    _selectedCinema = new Cinema()
                    {
                        Name = value.Name,
                        Id = value.Id
                    }; 
                }
                //SetProperty(ref _selectedCinema, value);
                OnPropertyChanged();
                (DeleteCinemaCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditCinemaCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        #endregion

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

    }
}
