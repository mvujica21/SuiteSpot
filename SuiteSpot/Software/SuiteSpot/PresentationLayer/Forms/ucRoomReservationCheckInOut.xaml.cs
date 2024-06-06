using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.Forms
{
    public partial class ucRoomReservationCheckInOut : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<ReservationDay> Days { get; } = new ObservableCollection<ReservationDay>();
        public ICommand SelectDateRangeCommand { get; private set; }

        private DateTime _currentMonth = DateTime.Now;

        public string CurrentMonthDisplay => _currentMonth.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

        private DateTime? _checkInDate = null;
        private DateTime? _checkOutDate = null;
        public int RoomCount { get; }
        public int GuestCount { get; }

        private readonly RoomService _roomService;
        private readonly RoomReservationService _reservationService;

        public ucRoomReservationCheckInOut(RoomService roomService, RoomReservationService reservationService, int roomCount, int guestCount, DateTime? checkInDate = null, DateTime? checkOutDate = null)
        {
            InitializeComponent();
            _roomService = roomService;
            _reservationService = reservationService;
            RoomCount = roomCount;
            GuestCount = guestCount;
            _checkInDate = checkInDate;
            _checkOutDate = checkOutDate;
            PopulateDays(_currentMonth);
            SelectDateRangeCommand = new RelayCommand(SelectDateRange);
            DataContext = this;
        }

        private void PopulateDays(DateTime month)
        {
            Days.Clear();
            DateTime firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            int daysToMonday = (int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysToMonday < 0) daysToMonday += 7;

            for (int i = 0; i < daysToMonday; i++)
            {
                Days.Add(new ReservationDay());
            }

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime currentDay = firstDayOfMonth.AddDays(i);
                Days.Add(new ReservationDay
                {
                    Day = currentDay,
                    DayString = currentDay.ToString("MMMM d", CultureInfo.InvariantCulture),
                    ReservationStatus = "Available",
                    IsToday = currentDay.Date == DateTime.Today,
                    IsEnabled = currentDay >= DateTime.Today
                });
            }
        }

        private void SelectDateRange(object parameter)
        {
            if (parameter is DateTime selectedDate)
            {
                if (selectedDate < DateTime.Today)
                {
                    Debug.WriteLine($"Date {selectedDate.ToShortDateString()} is before today and cannot be selected.");
                    return; // Ignore dates before today
                }

                if (_checkInDate == null || _checkOutDate != null)
                {
                    _checkInDate = selectedDate;
                    _checkOutDate = null;
                }
                else
                {
                    _checkOutDate = selectedDate;
                    if (_checkOutDate < _checkInDate)
                    {
                        DateTime temp = _checkInDate.Value;
                        _checkInDate = _checkOutDate;
                        _checkOutDate = temp;
                    }
                }
                UpdateCalendarDays();
            }
        }

        private void UpdateCalendarDays()
        {
            foreach (var day in Days)
            {
                bool shouldBeSelected = (_checkInDate.HasValue && _checkOutDate.HasValue &&
                                         day.Day >= _checkInDate.Value && day.Day <= _checkOutDate.Value);
                if (day.IsSelected != shouldBeSelected)
                {
                    day.IsSelected = shouldBeSelected;
                }
            }
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            PopulateDays(_currentMonth);
            OnPropertyChanged(nameof(CurrentMonthDisplay));
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(1);
            PopulateDays(_currentMonth);
            OnPropertyChanged(nameof(CurrentMonthDisplay));
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var checkInOutControl = new ucRoomReservationCheckInOut(_roomService, _reservationService, RoomCount, GuestCount, _checkInDate, _checkOutDate);
            mainWindow.contentControl.Content = checkInOutControl;
        }

        private async void ProceedToConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (_checkInDate.HasValue && _checkOutDate.HasValue)
            {
                var availableRooms = await FetchAvailableRooms(_checkInDate.Value, _checkOutDate.Value, RoomCount, GuestCount);
                DisplayAvailableRooms(availableRooms, _checkInDate.Value, _checkOutDate.Value);
            }
        }

        private Task<List<Room>> FetchAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int roomCount, int guestCount)
        {
            return _roomService.GetAvailableRooms(checkInDate, checkOutDate, roomCount, guestCount);
        }

        private void DisplayAvailableRooms(List<Room> availableRooms, DateTime checkInDate, DateTime checkOutDate)
        {
            var availableRoomsControl = new ucAvailableRoomsControl(availableRooms, RoomCount, GuestCount, checkInDate, checkOutDate);
            NavigateToAvailableRooms(availableRoomsControl);
        }

        private void NavigateToAvailableRooms(UserControl availableRoomsControl)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.contentControl.Content = availableRoomsControl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ReservationDay : INotifyPropertyChanged
    {
        private bool _isSelected;
        private bool _isEnabled;

        public DateTime Day { get; set; }
        public string DayString { get; set; }
        public bool IsToday { get; set; }
        public string ReservationStatus { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
