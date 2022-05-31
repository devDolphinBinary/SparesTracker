using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SparesTracker.Entities;


namespace SparesTracker.Views
{
    public partial class GetListWarehouses
    {
        private List<Warehouses> WarehousesList { get; set; }
        private List<Warehouses> FilteredWarehousesList { get; set; }
        
        private const int ItemsPerPage = 5;

        private int _currentPageIndex;

        public GetListWarehouses()
        {
            InitializeComponent();
            DataContext = this;

            UpdateAdminListView();
        }

        private void UpdateAdminListView()
        {
            WarehousesList = DatabaseOperations.GetAllWarehouses();
            FilteredWarehousesList = WarehousesList;

            FilterSpares();
        }

        private void FilterSpares()
        {
            if (WarehousesList is null)
                return;

            var searchText = SearchTextBox.Text.ToLower();

            var warehouses = WarehousesList.Where(s => s.name.ToLower().Contains(searchText));

            if (SearchTextBox.Text != "")
                warehouses = warehouses.Where(s => s.name == SearchTextBox.Text);

            switch (SortComboBox.SelectedValue.ToString())
            {
                case "Наименование по возрастанию":
                    warehouses = warehouses.OrderBy(s => s.name);
                    break;
                case "Наименование по убыванию":
                    warehouses = warehouses.OrderByDescending(s => s.name);
                    break;
                default:
                    warehouses = warehouses.OrderBy(s => s.id);
                    break;
            }

            FilteredWarehousesList = warehouses.ToList();

            UpdateItemAmountText();
            GeneratePageButtons();
        }

        private void UpdateCurrentPage()
        {
            var pageButtonList = ButtonStackPanel.Children.OfType<Button>().ToList();

            if ((int)pageButtonList.Last().Content == _currentPageIndex)
            {
                foreach (var button in pageButtonList)
                {
                    button.Content = (int)button.Content + 1;
                }
            }

            if ((int)pageButtonList.First().Content - 1 == _currentPageIndex + 1 && _currentPageIndex != -1)
            {
                foreach (var button in pageButtonList)
                {
                    button.Content = (int)button.Content - 1;
                }
            }

            foreach (var pageButton in pageButtonList)
            {
                pageButton.BorderThickness = new Thickness(0);

                if ((int)pageButton.Content == _currentPageIndex + 1)
                {
                    pageButton.BorderThickness = new Thickness(0, 0, 0, 5);
                }
            }

            WarehousesListView.ItemsSource =
                FilteredWarehousesList.Skip(_currentPageIndex * ItemsPerPage).Take(ItemsPerPage);

            NoItemsFoundTextBlock.Visibility =
                FilteredWarehousesList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GeneratePageButtons()
        {
            ButtonStackPanel.Children.Clear();

            for (var i = 0; i < 5; i++)
            {
                if (ItemsPerPage * i > FilteredWarehousesList.Count)
                    continue;

                var pageButton = new Button
                {
                    Content = i + 1,
                    MinWidth = 25,
                    MaxHeight = 25,
                    Background = Brushes.Transparent,
                    BorderThickness = new Thickness(0),
                    BorderBrush = Brushes.Black
                };

                if (i == _currentPageIndex)
                {
                    pageButton.BorderThickness = new Thickness(0, 0, 0, 5);
                }

                pageButton.Click += PageButton_Click;

                ButtonStackPanel.Children.Add(pageButton);
            }

            UpdateCurrentPage();
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPageIndex = (int)((Button)sender).Content - 1;

            UpdateCurrentPage();
        }

        private void PageLeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPageIndex != 0)
                _currentPageIndex--;

            UpdateCurrentPage();
        }

        private void PageRightButton_Click(object sender, RoutedEventArgs e)
        {
            if ((_currentPageIndex + 1) * ItemsPerPage < FilteredWarehousesList.Count)
                _currentPageIndex++;

            UpdateCurrentPage();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _currentPageIndex = 0;
            FilterSpares();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentPageIndex = 0;

            FilterSpares();
        }

        private void UpdateItemAmountText()
        {
            ItemAmountText.Text = $"Выведено {FilteredWarehousesList.Count} из {WarehousesList.Count}";
        }

        private void MaterialListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeAmountButton.Visibility =
                WarehousesListView.SelectedItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CreateSpareClick(object sender, RoutedEventArgs e)
        {
            var addSpare = new SpareAdd();
            addSpare.Show();
            Close();
        }
        
        private void AdminClick(object sender, RoutedEventArgs e)
        {
            var admin = new Admin();
            admin.Show();
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
            if (MessageBox.Show("Вы действительно хотите смнеить пользователя?", "Выход", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            var login = new Login();
            login.Show();
            Close();
        }
    }
}