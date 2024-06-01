using BusinessLogicLayer.Services;
using EntitiesLayer.ViewModel;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace PresentationLayer.Forms
{
    public partial class ucManageWorkSchedule : UserControl
    {
        private ScheduleService scheduleService;

        public ucManageWorkSchedule()
        {
            InitializeComponent();
            scheduleService = new ScheduleService();
            LoadSchedules();
        }

        private void LoadSchedules()
        {
            var transformedSchedules = scheduleService.GetTransformedSchedulesForNext7Days();

            dgWorkSchedule.Columns.Clear();
            dgWorkSchedule.Columns.Add(new DataGridTextColumn { Header = "Employee", Binding = new Binding("EmployeeName") });

            var dates = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(i).ToString("yyyy-MM-dd")).ToList();

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
    }
}
