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
                var client = _context.Users.ToList()
                    .FirstOrDefault(i => i.login == LoginBox.Text && i.password == PasswordBox.Password);
                if (LoginBox.Text == "" || PasswordBox.Password == "")
                {
                    MessageBox.Show("Логин или пароль не указаны");
                }
                else if (client != null)
                {
                    Entities.DataUser.User = client;

                    switch (client.roleId)
                    {
                        case 1:
                        {
                            var actionWin = new Admin();
                            Hide();
                            actionWin.ShowDialog();
                            break;
                        }
                        case 2:
                        {
                            var actionWin = new Client(client);
                            Hide();
                            actionWin.ShowDialog();
                            break;
                        }
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
            var registration = new Register();
            Hide();
            registration.ShowDialog();
        }
    }
}