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
    public class RoomWindowViewModel : ObservableRecipient
    {
        public RoomWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Rooms = new RestCollection<Room>("http://localhost:20463/", "room", "hub");
                Cinemas = new RestCollection<Cinema>("http://localhost:20463/", "cinema", "hub");

                #region Commands

                AddRoomCommand = new RelayCommand
                   (() =>
                   {
                       Rooms.Add(new Room()
                       {
                           RoomNumber = SelectedRoom.RoomNumber,
                           CinemaId = SelectedCinema.Id
                       });
                   }
                   );

                EditRoomCommand = new RelayCommand
                    (() =>
                    {
                        SelectedRoom.CinemaId = SelectedCinema.Id;
                        Rooms.Update(SelectedRoom);
                    }, () => SelectedRoom != null && SelectedRoom.RoomNumber != 0 && SelectedCinema != null
                    );

                DeleteRoomCommand = new RelayCommand
                    (() =>
                    {
                        Rooms.Delete(SelectedRoom.Id);
                        SelectedRoom = new Room();
                        //(DeleteRoomCommand as RelayCommand).NotifyCanExecuteChanged();
                        //(EditRoomCommand as RelayCommand).NotifyCanExecuteChanged();
                    }, () => SelectedRoom != null && SelectedRoom.RoomNumber != 0
                    );

                SelectedCinema = new Cinema();
                SelectedRoom = new Room();
                #endregion
            }
        }

        #region Properties

        public RestCollection<Cinema> Cinemas { get; set; }
        public RestCollection<Room> Rooms { get; set; }

        public ICommand DeleteRoomCommand { get; set; }
        public ICommand EditRoomCommand { get; set; }
        public ICommand AddRoomCommand { get; set; }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                //SetProperty(ref _selectedRoom, value);
                if (value != null)
                {
                    _selectedRoom = new Room()
                    {
                        RoomNumber = value.RoomNumber,
                        Id = value.Id,
                        CinemaId = value.CinemaId
                    };
                    OnPropertyChanged();
                    (DeleteRoomCommand as RelayCommand).NotifyCanExecuteChanged();
                    (EditRoomCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Cinema _selectedCinema;
        public Cinema SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                SetProperty(ref _selectedCinema, value);
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
