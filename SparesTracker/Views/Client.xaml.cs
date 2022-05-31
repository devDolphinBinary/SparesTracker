using System.Windows;
using System.Windows.Controls;

namespace SparesTracker.Views
{
    public partial class Client : Window
    {
        public Client(Users client)
        {
            InitializeComponent();
        }
        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // _currentSearch = ((TextBox)sender).Text;
            // ChangePage();
            // GeneratePageButtons();
        }
        
        private void ChangeUserClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите смнеить пользователя?", "Выход", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            var login = new Login();
            login.Show();
            Close();
        }
    }
}