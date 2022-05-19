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
    class MovieWindowViewModel : ObservableRecipient
    {
        public MovieWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Movies = new RestCollection<Movie>("http://localhost:20463/", "Movie", "hub");
                Rooms = new RestCollection<Room>("http://localhost:20463/", "Room", "hub");

                #region Commands

                AddMovieCommand = new RelayCommand
                    (() =>
                    {
                        Movies.Add(new Movie()
                        {
                            Title = SelectedMovie.Title,
                            Price = SelectedMovie.Price,
                            Type = SelectedMovie.Type,
                            Length = SelectedMovie.Length,
                            RoomId = SelectedRoom.Id
                        });
                    });
                DeleteMovieCommand = new RelayCommand
                    (() =>
                    {
                        Movies.Delete(SelectedMovie.Id);
                        SelectedMovie = new Movie();
                        //(EditMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                    }, () => SelectedMovie.Title != null &&
                            SelectedMovie.Price != null &&
                            SelectedMovie.Length != null
                    );
                EditMovieCommand = new RelayCommand
                    (() =>
                    {
                        SelectedMovie.RoomId = SelectedRoom.Id;
                        Movies.Update(SelectedMovie);
                    }, () => SelectedMovie.Title != null &&
                            SelectedMovie.Price != null &&
                            SelectedMovie.Length != null &&
                            SelectedMovie.RoomId != 0 &&
                            SelectedRoom != null
                    );

                SelectedRoom = new Room();
                SelectedMovie = new Movie();

                #endregion

            }
        }

        #region Properties

        public RestCollection<Movie> Movies { get; set; }
        public RestCollection<Room> Rooms { get; set; }

        public ICommand AddMovieCommand { get; set; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand EditMovieCommand { get; set; }

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                if (value != null)
                {
                    _selectedMovie = new Movie()
                    {
                        Title = value.Title,
                        Price = value.Price,
                        Length = value.Length,
                        Type = value.Type,
                        Id = value.Id,
                        RoomId = value.RoomId
                    };
                }
                OnPropertyChanged();
                (DeleteMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditMovieCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                SetProperty(ref _selectedRoom, value);
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
