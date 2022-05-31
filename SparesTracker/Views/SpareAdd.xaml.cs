using System;
using System.Linq;
using System.Windows;

namespace SparesTracker.Views
{
    public partial class SpareAdd
    {
        private readonly RepairPartsEntities _context = new RepairPartsEntities();

        public SpareAdd()
        {
            InitializeComponent();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            var logBd = _context.Spares.Where(i => i.name == NameBox.Text).Select(j => j.name).FirstOrDefault();

            if (NameBox.Text == "" || WarehouseIdBox.Text == "" || AmountBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }

            else if (logBd != null)
            {
                MessageBox.Show("Запчасть уже создана!");
            }
            else
            {
                try
                {
                    _context.Spares.Add(new Spares()
                    {
                        name = NameBox.Text,
                        amount = int.Parse(AmountBox.Text),
                        warehouseId = int.Parse(WarehouseIdBox.Text),
                    });
                    _context.SaveChanges();

                    Admin admin = new Admin();
                    Hide();
                    admin.ShowDialog();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            var admin = new Admin();
            admin.Show();
            Close();
        }
    }
}