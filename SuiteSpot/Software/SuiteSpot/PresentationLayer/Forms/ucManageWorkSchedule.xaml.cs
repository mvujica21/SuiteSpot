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
    /// <summary>
    /// Interaction logic for ucManageWorkSchedule.xaml
    /// </summary>
    public partial class ucManageWorkSchedule : UserControl
    {
        public ObservableCollection<Dictionary<string, string>> Employees { get; set; }

        public ucManageWorkSchedule()
        {
            InitializeComponent();

            InitializeComponent();

            Employees = new ObservableCollection<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "Name", "John Doe" },
                    { "May 16", "8am-4pm" },
                    { "May 17", "1pm-9pm" },
                    { "May 18", "10pm-6am" },
                    { "May 19", "Off" },
                    { "May 20", "8am-4pm" },
                    { "May 21", "Off" },
                    { "May 22", "1pm-9pm" }
                },
                new Dictionary<string, string>
                {
                    { "Name", "Jane Smith" },
                    { "May 16", "Off" },
                    { "May 17", "8am-4pm" },
                    { "May 18", "Off" },
                    { "May 19", "1pm-9pm" },
                    { "May 20", "10pm-6am" },
                    { "May 21", "8am-4pm" },
                    { "May 22", "Off" }
                }
            };

            this.DataContext = this;

            GenerateColumnsForWeek();
        }
        private void GenerateColumnsForWeek()
        {
            DateTime startOfWeek = new DateTime(2023, 5, 16); // Assuming the week starts from May 16
            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = startOfWeek.AddDays(i);
                string dayHeader = currentDay.ToString("MMM dd (ddd)", CultureInfo.InvariantCulture);

                DataGridTextColumn column = new DataGridTextColumn
                {
                    Header = dayHeader,
                    Binding = new Binding($"[{currentDay.ToString("MMM dd")}]"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };

                dgWorkSchedule.Columns.Add(column);
            }
        }
    }
}
