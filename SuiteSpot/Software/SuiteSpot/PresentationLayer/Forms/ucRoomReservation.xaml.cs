using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ucRoomReservation : UserControl
    {
        public ObservableCollection<ReservationDay> Days { get; } = new ObservableCollection<ReservationDay>();

        public ICommand SelectDateCommand { get; }

        public ucRoomReservation()
        {
            InitializeComponent();
            PopulateDays();
            SelectDateCommand = new RelayCommand(SelectDate);
            DataContext = this;
        }

        private void PopulateDays()
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime currentDay = firstDayOfMonth.AddDays(i);
                string reservationInfo = ""; 
                Days.Add(new ReservationDay
                {
                    Day = currentDay,
                    DayString = currentDay.ToString("MMMM d", CultureInfo.InvariantCulture),
                    ReservationStatus = string.IsNullOrEmpty(reservationInfo) ? "No reservation" : "Reservation"
                });
            }
        }

        private void SelectDate(object selectedDate)
        {
            if (selectedDate is DateTime date)
            {
                MessageBox.Show($"Selected date: {date}");
            }
        }
    }

    public class ReservationDay
    {
        public DateTime Day { get; set; }
        public string DayString { get; set; }
        public string ReservationStatus { get; set; }
    }
}