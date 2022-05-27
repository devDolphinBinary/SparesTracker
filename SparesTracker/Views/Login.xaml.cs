using System;
using System.Linq;
using System.Windows;

namespace SparesTracker.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        private readonly RepairPartsEntities _context = new RepairPartsEntities();
        
        public Login()
        {
            InitializeComponent();
        }

        private void LoginSystemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Users client = _context.Users.ToList()
                    .FirstOrDefault(i => i.login == LoginTextBox.Text && i.password == PasswordBox.Password);

                if (client != null)
                {
                    Entities.DataUser.User = client;

                    if (client.roleId == 1)
                    {
                        Admin actionWin = new Admin();
                        Hide();
                        actionWin.ShowDialog();
                    }
                    else if (client.roleId == 2)
                    {
                        Client actionWin = new Client(client);
                        Hide();
                        actionWin.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Логин или пароль введены неверно");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Register registration = new Register();
            this.Hide();
            registration.ShowDialog();
        }
    }
}