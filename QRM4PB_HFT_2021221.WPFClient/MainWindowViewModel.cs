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
        public ICommand DeleteCinemaCommand { get; set; }
        public ICommand EditCinemaCommand { get; set; }
        public ICommand AddCinemaCommand { get; set; }


        public RestCollection<Cinema> Cinemas { get; set; }
        public RestCollection<Room> Rooms { get; set; }
        public RestCollection<Movie> Movies { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Cinemas = new RestCollection<Cinema>("http://localhost:20463/", "Cinema");
                Rooms = new RestCollection<Room>("http://localhost:20463/", "Room");
                Movies = new RestCollection<Movie>("http://localhost:20463/", "Movie");

                AddCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Add(new Cinema()
                        { Name = "*Give it a name*" });
                    }
                    );

                EditCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Update(SelectedCinema);
                    }, () => SelectedCinema != null
                    );

                DeleteCinemaCommand = new RelayCommand
                    (() =>
                    {
                        Cinemas.Delete(SelectedCinema.Id);
                    }, () => SelectedCinema != null
                    );
            }
        }

        #region Properties

        private Cinema _selectedCinema;

        public Cinema SelectedCinema
        {
            get { return _selectedCinema; }
            set 
            {
                if (value != null) 
                {
                    //SetProperty(ref _selectedCinema, value);
                    _selectedCinema = new Cinema()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteCinemaCommand as RelayCommand).NotifyCanExecuteChanged();
                    (EditCinemaCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { SetProperty(ref _selectedRoom, value); }

        }

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set { SetProperty(ref _selectedMovie, value); }
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
