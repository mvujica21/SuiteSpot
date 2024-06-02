using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Services;

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

            if (employeeService.ValidateCredentials(username, password))
            {
                MessageBox.Show("Login successful!");
                // Proceed to the next step, such as opening the main application window
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
