using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    /// <summary>
    /// Interaction logic for ucAddRole.xaml
    /// </summary>
    public partial class ucAddRole : UserControl
    {
        private RoleService roleService;
        private Role currentRole;
        public ucAddRole()
        {
            InitializeComponent();
            roleService = new RoleService();
        }
        public ucAddRole(Role role) : this()
        {
            currentRole = role;
            if (currentRole != null)
            {
                txtRoleName.Text = role.Name;
            }
        }
        private void btnSaveRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string role = txtRoleName.Text;


                if (currentRole == null)
                {
                    currentRole = new Role();
                }

                currentRole.Name = role;


                if (currentRole.Id == 0)
                {
                    Debug.WriteLine($"Adding employee: {Name}");
                    roleService.AddRole(currentRole);
                }
                else
                {
                    Debug.WriteLine($"Updating role: {Name}");
                    roleService.UpdateRole(currentRole);
                }

                MessageBox.Show("Role saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = new ucManageRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving role: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
