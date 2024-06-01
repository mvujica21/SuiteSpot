using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucAddEmployee.xaml
    /// </summary>
    public partial class ucAddEmployee : UserControl
    {
        private EmployeeService employeeService;
        private Employee currentEmployee;

        public ucAddEmployee()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
        }

        public ucAddEmployee(Employee employee) : this()
        {
            currentEmployee = employee;
            PopulateEmployeeData(employee);
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
                    .FirstOrDefault(item => item.Content.ToString() == GetRoleNameById(employee.RoleId));
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
                string roleName = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                int roleId = GetRoleIdByName(roleName);

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
                    Debug.WriteLine($"Adding employee: {firstName} {lastName}, RoleId: {roleId}");
                    employeeService.AddEmployee(currentEmployee);
                }
                else
                {
                    Debug.WriteLine($"Updating employee: {firstName} {lastName}, RoleId: {roleId}");
                    employeeService.UpdateEmployee(currentEmployee);
                }

                MessageBox.Show("Employee saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GetRoleIdByName(string roleName)
        {
            switch (roleName)
            {
                case "Manager":
                    return 1;
                case "Reception":
                    return 2;
                case "Cleaning":
                    return 3;
                default:
                    throw new ArgumentException("Invalid role name");
            }
        }

        private string GetRoleNameById(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Manager";
                case 2:
                    return "Reception";
                case 3:
                    return "Cleaning";
                default:
                    throw new ArgumentException("Invalid role ID");
            }
        }
    }
}
