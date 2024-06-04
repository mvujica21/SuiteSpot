using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;
using HotelManagement.Entities;

namespace PresentationLayer.Forms
{
    public partial class ucLogin : UserControl
    {
        private EmployeeService employeeService;

        public ucLogin()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var employee = employeeService.ValidateCredentials(username, password);
            if (employee != null)
            {
                SessionManager.EmployeeId = employee.Id;
                MessageBox.Show("Login successful!");          
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}