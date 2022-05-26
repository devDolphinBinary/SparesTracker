using System.Linq;
using System.Windows;

namespace SparesTracker.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Register
    {
        private readonly RepairPartsEntities _context = new RepairPartsEntities();

        public Register()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            string logBd = _context.Users.Where(i => i.login == LoginBox.Text).Select(j => j.login).FirstOrDefault();

            if (LoginBox.Text == "" || PasswordBox.Password == "" || ConfirmPasswordBox.Password == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else if (logBd != null)
            {
                MessageBox.Show("Логин уже используется!");
            }
            else
            {
                _context.Users.Add(new Users()
                {
                    name = "Worker",
                    login = LoginBox.Text,
                    password = PasswordBox.Password,
                    roleId = 2,
                    warehouseId = 1,
                });
                _context.SaveChanges();

                Login auth = new Login();
                Hide();
                auth.ShowDialog();
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }
    }
}