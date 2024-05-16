using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucRoomReservationCheckInOut.xaml
    /// </summary>
    public partial class ucRoomReservationCheckInOut : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<ReservationDay> Days { get; } = new ObservableCollection<ReservationDay>();
        public ICommand SelectDateRangeCommand { get; private set; }

        private DateTime _currentMonth = DateTime.Now;

        public string CurrentMonthDisplay => _currentMonth.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

        private DateTime? _checkInDate = null;
        private DateTime? _checkOutDate = null;

        public ucRoomReservationCheckInOut()
        {
            InitializeComponent();
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
                    IsToday = currentDay.Date == DateTime.Today
                });
            }
        }

        private void SelectDateRange(object parameter)
        {
            if (parameter is DateTime selectedDate)
            {
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
            // Add your logic here to navigate back to the previous view or step
        }

        private void ProceedToConfirmation_Click(object sender, RoutedEventArgs e)
        {
            // Add your logic here to proceed to confirmation or next step of the booking process
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
