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
        private RoleService roleService;

        public ucManageRoles()
        {
            InitializeComponent();
            roleService = new RoleService();
            loadRoles();
        }

        private void loadRoles()
        {
            dgRoles.ItemsSource = roleService.GetRoles();
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.contentControl.Content = new ucAddRole();
            }
        }

        private void btnUpdateRole_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRole = dgRoles.SelectedItem as Role;
            if (selectedRole != null)
            {
                MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.contentControl.Content = new ucAddRole(selectedRole);
                }
            }
            else
            {
                MessageBox.Show("Role needs to be selected before update!", "Error!");
            }
        }

        private void btnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRole = dgRoles.SelectedItem as Role;
            if (selectedRole != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this role?", "Delete?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    roleService.DeleteRole(selectedRole);
                    MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
                    if (mainWindow != null)
                    {
                        mainWindow.contentControl.Content = new ucManageEmployees();
                    }
                }
            }
            else
            {
                MessageBox.Show("Role needs to be selected before delete!", "Error!");
            }
        }

    }
}
