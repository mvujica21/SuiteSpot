using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.Forms
{
    /*
    public partial class ucRoomReservation : UserControl
    {
        public ObservableCollection<ReservationDay> Days { get; } = new ObservableCollection<ReservationDay>();
        public ICommand SelectDateCommand { get; private set; }

        private DateTime _currentMonth = DateTime.Now;

        public string CurrentMonthDisplay => _currentMonth.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

        public ucRoomReservation()
        {
            InitializeComponent();
            PopulateDays(_currentMonth);
            SelectDateCommand = new RelayCommand(SelectDate);
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
                    ReservationStatus = "Available",  // Assuming default status
                    IsToday = currentDay.Date == DateTime.Today
                });
            }
        }

        private void SelectDate(object selectedDate)
        {
            if (selectedDate is DateTime date)
            {
                MessageBox.Show($"Selected date: {date.ToShortDateString()}", "Date Selected");
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ReservationDay
    {
        public DateTime Day { get; set; } = DateTime.MinValue;
        public string DayString { get; set; } = "";
        public string ReservationStatus { get; set; } = "";
        public bool IsToday { get; set; } = false;
    }
    */
}
