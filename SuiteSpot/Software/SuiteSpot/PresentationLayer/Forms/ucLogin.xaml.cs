using BusinessLogicLayer.Services;
using System.Windows;
using System.Windows.Controls;

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
                SessionManager.UserRole = employee.Role.Name;
                MessageBox.Show("Login successful!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();

            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}