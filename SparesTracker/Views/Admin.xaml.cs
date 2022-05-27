using System.Windows;

namespace SparesTracker.Views
{
    public partial class Admin
    {
        public Admin()
        {
            InitializeComponent();
        } 
        private void CreateSpareClick(object sender, RoutedEventArgs e)
        {
            var addSpare = new SpareAdd();
            addSpare.Show();
            Close();
        }
        private void CreateWarehouseClick(object sender, RoutedEventArgs e)
        {
            var addWarehouse = new WarehouseAdd();
            addWarehouse.Show();
            Close();
        }
        private void ChangeUserClick(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }
    }
}