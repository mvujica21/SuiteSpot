using BusinessLogicLayer.Services;
using HotelManagement.Entities;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public partial class ucBills : UserControl
    {
        private readonly BillService _billService;

        public ucBills()
        {
            InitializeComponent();
            _billService = new BillService();
            LoadBills();
        }

        private async void LoadBills()
        {
            var activeBills = await _billService.GetActiveBillsAsync();
            BillsDataGrid.ItemsSource = activeBills;
        }

        private async void AddBill_Click(object sender, RoutedEventArgs e)
        {
            var newBill = await _billService.CreateBillAsync();
            var billDetailsControl = new ucBillDetails(newBill);
            var mainWindow = (Window.GetWindow(this) as MainWindow);
            mainWindow.contentControl.Content = billDetailsControl;
        }

        private void ModifyBill_Click(object sender, RoutedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem is Bill selectedBill)
            {
                var billDetailsControl = new ucBillDetails(selectedBill);
                (Window.GetWindow(this) as MainWindow).contentControl.Content = billDetailsControl;
            }
            else
            {
                MessageBox.Show("Please select a bill before proceeding.");
            }
        }

        private async void DeleteBill_Click(object sender, RoutedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem is Bill selectedBill)
            {
                selectedBill.Status = "Deleted";
                await _billService.FinalizeBillAsync(selectedBill);
                LoadBills();
            }
            else
            {
                MessageBox.Show("Please select a bill before proceeding.");
            }
        }
    }
}
