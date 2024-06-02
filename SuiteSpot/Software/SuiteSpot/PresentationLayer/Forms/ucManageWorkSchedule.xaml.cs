using BusinessLogicLayer.Services;
using EntitiesLayer.ViewModel;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PresentationLayer.Forms
{
    public partial class ucManageWorkSchedule : UserControl
    {
        private ScheduleService scheduleService;
        private EmployeeService employeeService;
        private List<EmployeeScheduleViewModel> transformedSchedules;
        private List<Employee> employees;
        private List<string> dates;

        public ucManageWorkSchedule()
        {
            InitializeComponent();
            scheduleService = new ScheduleService();
            employeeService = new EmployeeService();
            LoadEmployees();
            LoadSchedules();
            PopulateDatesComboBox();
        }

        private void LoadSchedules()
        {
            transformedSchedules = scheduleService.GetTransformedSchedulesForNext7Days();

            dgWorkSchedule.Columns.Clear();
            dgWorkSchedule.Columns.Add(new DataGridTextColumn { Header = "Employee", Binding = new Binding("EmployeeName") });

            dates = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(i).ToString("yyyy-MM-dd")).ToList();

            foreach (var date in dates)
            {
                dgWorkSchedule.Columns.Add(new DataGridTextColumn
                {
                    Header = date,
                    Binding = new Binding($"Shifts[{date}]")
                });
            }

            dgWorkSchedule.ItemsSource = transformedSchedules;
        }

        private void LoadEmployees()
        {
            employees = employeeService.GetEmployees();
            cbEmployees.ItemsSource = employees;
            cbEmployees.DisplayMemberPath = "FullName";
        }

        private void PopulateDatesComboBox()
        {
            cbDates.ItemsSource = dates;
        }

        private void btnAddShift_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = cbEmployees.SelectedItem as Employee;
            var selectedDate = cbDates.SelectedItem as string;
            var selectedShift = (cbShifts.SelectedItem as ComboBoxItem)?.Content as string;

            if (selectedEmployee == null || selectedDate == null || selectedShift == null)
            {
                MessageBox.Show("Please select an employee, date, and shift.");
                return;
            }

            var date = DateTime.Parse(selectedDate);

            if (selectedShift == "Off")
            {
                scheduleService.DeleteSchedule(date, selectedEmployee.Id);
                MessageBox.Show($"Schedule for {selectedEmployee.FullName} on {date:yyyy-MM-dd} has been set to Off (removed).");
            }
            else
            {
                TimeSpan shiftStart, shiftEnd;
                int shiftId;

                switch (selectedShift)
                {
                    case "07:00 - 15:00":
                        shiftStart = new TimeSpan(7, 0, 0);
                        shiftEnd = new TimeSpan(15, 0, 0);
                        shiftId = 1;
                        break;
                    case "15:00 - 23:00":
                        shiftStart = new TimeSpan(15, 0, 0);
                        shiftEnd = new TimeSpan(23, 0, 0);
                        shiftId = 2;
                        break;
                    case "23:00 - 07:00":
                        shiftStart = new TimeSpan(23, 0, 0);
                        shiftEnd = new TimeSpan(7, 0, 0);
                        shiftId = 3;
                        break;
                    default:
                        MessageBox.Show("Invalid shift selected.");
                        return;
                }

                scheduleService.AddSchedule(date, shiftStart, shiftEnd, shiftId, selectedEmployee.Id);
            }

            LoadSchedules();
        }
    }
}
