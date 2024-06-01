using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucManageRoles.xaml
    /// </summary>
    public partial class ucManageRoles : UserControl
    {
        public RoleService roleService;
        public ucManageRoles()
        {
            InitializeComponent();
            roleService = new RoleService();
            loadRoles();
        }

        private void loadRoles()
        {
            List<Role> roles = roleService.GetRoles();
            dgRoles.ItemsSource = roles;
        }
        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateRole_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRole = dgRoles.SelectedItem as Role;
            if (selectedRole != null)
            {
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucAddRole(selectedRole);
            }
            else
            {
                MessageBox.Show("Employee needs to be selected before update!", "Error!");
            }
        }

        private void btnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRole= dgRoles.SelectedItem as Role;
            if (selectedRole != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this role?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    roleService.DeleteRole(selectedRole);
                    (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageEmployees();
                }
            }
            else
            {
                MessageBox.Show("Employee needs to be selected before delete!", "Error!");
            }
        }
    }
}
