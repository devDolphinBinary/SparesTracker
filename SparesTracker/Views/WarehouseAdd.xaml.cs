using System;
using System.Linq;
using System.Windows;

namespace SparesTracker.Views
{
    public partial class WarehouseAdd
    {
        
        private readonly RepairPartsEntities _context = new RepairPartsEntities();

        public WarehouseAdd()
        {
            InitializeComponent();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            string logBd = _context.Spares.Where(i => i.name == NameBox.Text).Select(j => j.name).FirstOrDefault();

            if (NameBox.Text == "" || AddressBox.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }

            else if (logBd != null)
            {
                MessageBox.Show("Склад уже создан!");
            }
            else
            {
                try
                {
                    _context.Warehouses.Add(new Warehouses()
                    {
                        name = NameBox.Text,
                        address = AddressBox.Text,
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