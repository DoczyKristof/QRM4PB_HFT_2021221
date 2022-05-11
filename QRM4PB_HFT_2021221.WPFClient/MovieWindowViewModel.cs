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
                Movies = new RestCollection<Movie>("http://localhost:20463/", "Movie");

                #region Commands

                AddMovieCommand = new RelayCommand
                    (() =>
                    {
                        Movies.Add(new Movie()
                        {
                            Title = SelectedMovie.Title,
                            Price = SelectedMovie.Price,
                            Type = SelectedMovie.Type
                        });
                    });
                DeleteMovieCommand = new RelayCommand
                    (() =>
                    {
                        Movies.Delete(SelectedMovie.Id);
                        _selectedMovie = null;
                        (EditMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                    }, () => SelectedMovie != null
                    );
                EditMovieCommand = new RelayCommand
                    (() =>
                    {
                        Movies.Update(SelectedMovie);
                    }, () => SelectedMovie != null
                    );

                SelectedMovie = new Movie();

                #endregion

            }
        }

        #region Properties

        public RestCollection<Movie> Movies { get; set; }

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
                        Type = value.Type,
                        Id = value.Id
                    };
                }
                OnPropertyChanged();
                (DeleteMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                (EditMovieCommand as RelayCommand).NotifyCanExecuteChanged();
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
