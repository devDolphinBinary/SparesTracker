using System.Windows;

namespace SparesTracker.Views
{
    public partial class Client : Window
    {
        public Client(Users client)
        {
            InitializeComponent();
        }
        private void ChangeUserClick(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }
    }
}