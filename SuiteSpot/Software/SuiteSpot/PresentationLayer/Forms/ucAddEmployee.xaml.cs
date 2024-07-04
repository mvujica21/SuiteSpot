using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucAddEmployee : UserControl
    {
        private EmployeeService employeeService;
        private RoleService roleService;
        private Employee currentEmployee;

        public ucAddEmployee()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            roleService = new RoleService();
            PopulateRoleComboBox();
        }

        public ucAddEmployee(Employee employee) : this()
        {
            currentEmployee = employee;
            PopulateEmployeeData(employee);
        }

        private void PopulateRoleComboBox()
        {
            RoleComboBox.Items.Clear();
            var roles = roleService.GetRoles();
            foreach (var role in roles)
            {
                RoleComboBox.Items.Add(new ComboBoxItem { Content = role.Name, Tag = role.Id });
            }
        }

        private void PopulateEmployeeData(Employee employee)
        {
            if (employee != null)
            {
                FirstNameTextBox.Text = employee.FirstName;
                LastNameTextBox.Text = employee.LastName;
                EmailTextBox.Text = employee.Email;
                PhoneNumberTextBox.Text = employee.PhoneNumber;
                RoleComboBox.SelectedItem = RoleComboBox.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => (int)item.Tag == employee.RoleId);
                UsernameTextBox.Text = employee.Username;
                UsernameTextBox.IsEnabled = false;
                PasswordBox.Password = string.Empty;
            }
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string email = EmailTextBox.Text;
                string phoneNumber = PhoneNumberTextBox.Text;
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;
                var selectedRoleItem = RoleComboBox.SelectedItem as ComboBoxItem;
                int roleId = selectedRoleItem != null ? (int)selectedRoleItem.Tag : 0;

                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phoneNumber) ||
                    string.IsNullOrWhiteSpace(username) ||
                    roleId == 0)
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (currentEmployee == null)
                {
                    currentEmployee = new Employee();
                }

                currentEmployee.FirstName = firstName;
                currentEmployee.LastName = lastName;
                currentEmployee.Email = email;
                currentEmployee.PhoneNumber = phoneNumber;
                currentEmployee.RoleId = roleId;

                if (currentEmployee.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        MessageBox.Show("Password is required for new employees.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    employeeService.AddEmployee(username, password, firstName, lastName, email, phoneNumber, roleId);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        employeeService.AddEmployee(username, password, firstName, lastName, email, phoneNumber, roleId);
                    }
                    else
                    {
                        employeeService.UpdateEmployee(currentEmployee);
                    }
                }

                MessageBox.Show("Employee saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
