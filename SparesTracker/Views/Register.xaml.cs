using System.Windows;

namespace SparesTracker.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Register
    {
        public Register()
        {
            InitializeComponent();
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var login = new Login(); 
            login.Show(); 
            Close();
        }
    }
}