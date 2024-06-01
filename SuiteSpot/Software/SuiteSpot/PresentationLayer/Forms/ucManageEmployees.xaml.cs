using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ucManageEmployees.xaml
    /// </summary>
    public partial class ucManageEmployees : UserControl
    {
        EmployeeService employeeService;

        public ucManageEmployees()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            LoadEmployees();
        }
        private void LoadEmployees()
        {
            List<Employee> employees = employeeService.GetEmployees();
            dgEmployees.ItemsSource = employees;
        }
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddEmployee();

        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = dgEmployees.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this employee?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    employeeService.DeleteEmployee(selectedEmployee);
                    (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();
                }
            }
            else
            {
                MessageBox.Show("Employee needs to be selected before delete!", "Error!");
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = dgEmployees.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddEmployee(selectedEmployee);
            }
            else
            {
                MessageBox.Show("Employee needs to be selected before update!", "Error!");
            }
        }

        private void btnManageRoles_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();

        }
    }
}
