using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SparesTracker.Entities;


namespace SparesTracker.Views
{
    public partial class Admin
    {
        private List<Spares> SparesList { get; set; }
        private List<Spares> FilteredSparesList { get; set; }

        private const int ItemsPerPage = 5;

        private int _currentPageIndex;

        public Admin()
        {
            InitializeComponent();
            DataContext = this;

            UpdateAdminListView();
        }

        private void UpdateAdminListView()
        {
            SparesList = DatabaseOperations.GetAllSpares();
            FilteredSparesList = SparesList;

            FilterSpares();
        }

        private void FilterSpares()
        {
            if (SparesList is null)
                return;

            var searchText = SearchTextBox.Text.ToLower();

            var spares = SparesList.Where(s => s.name.ToLower().Contains(searchText));

            if (SearchTextBox.Text != "")
                spares = spares.Where(s => s.name == SearchTextBox.Text);

            switch (SortComboBox.SelectedValue.ToString())
            {
                case "Наименование по возрастанию":
                    spares = spares.OrderBy(s => s.name);
                    break;
                case "Наименование по убыванию":
                    spares = spares.OrderByDescending(s => s.name);
                    break;
                case "Остаток на складе по возрастанию":
                    spares = spares.OrderBy(s => s.amount);
                    break;
                case "Остаток на складе по убыванию":
                    spares = spares.OrderByDescending(s => s.amount);
                    break;
                default:
                    spares = spares.OrderBy(s => s.id);
                    break;
            }

            AdminListView.ItemsSource = spares.ToList();

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

            AdminListView.ItemsSource =
                FilteredSparesList.Skip(_currentPageIndex * ItemsPerPage).Take(ItemsPerPage);

            NoItemsFoundTextBlock.Visibility =
                FilteredSparesList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GeneratePageButtons()
        {
            ButtonStackPanel.Children.Clear();

            for (var i = 0; i < 5; i++)
            {
                if (ItemsPerPage * i > FilteredSparesList.Count)
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
            if ((_currentPageIndex + 1) * ItemsPerPage < FilteredSparesList.Count)
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
            ItemAmountText.Text = $"Выведено {FilteredSparesList.Count} из {SparesList.Count}";
        }

        private void MaterialListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeAmountButton.Visibility =
                AdminListView.SelectedItems.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CreateSpareClick(object sender, RoutedEventArgs e)
        {
            var addSpare = new SpareAdd();
            addSpare.Show();
            Close();
        }

        private void WarehousesClick(object sender, RoutedEventArgs e)
        {
            var warehouses = new GetListWarehouses();
            warehouses.Show();
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