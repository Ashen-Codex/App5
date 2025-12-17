using System;
using System.Collections.Generic;
using App5.Models;
using App5.Services;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page3 : Page
    {
        private ProductService _service = new ProductService();
        private List<Product> _products = new List<Product>();

        public Page3()
        {
            InitializeComponent();
            Loaded += (s, e) => LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsListView.Items.Clear();
            _products = _service.GetProducts();

            foreach (var product in _products)
            {
                var grid = new Grid
                {
                    Margin = new Thickness(0, 5, 0, 5),
                    Height = 45
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

                var nameText = new TextBlock
                {
                    Text = product.Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 14,
                    Padding = new Thickness(10, 0, 0, 0),
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 240
                };

                var priceText = new TextBlock
                {
                    Text = product.Price.ToString("C2").Replace("руб.", "₽"),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 14
                };

                var quantityText = new TextBlock
                {
                    Text = product.Quantity.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 14
                };

                grid.Children.Add(nameText);
                Grid.SetColumn(priceText, 1);
                grid.Children.Add(priceText);
                Grid.SetColumn(quantityText, 2);
                grid.Children.Add(quantityText);

                ProductsListView.Items.Add(grid);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text)) return;
            if (!decimal.TryParse(PriceTextBox.Text, out decimal price)) return;
            if (!int.TryParse(QuantityTextBox.Text, out int quantity)) return;

            var newProduct = new Product
            {
                Name = NameTextBox.Text,
                Price = price,
                Quantity = quantity
            };

            _service.AddProduct(newProduct);

            LoadProducts();

            NameTextBox.Text = "";
            PriceTextBox.Text = "";
            QuantityTextBox.Text = "";
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = ProductsListView.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _products.Count)
            {
                _service.DeleteProduct(_products[selectedIndex]);
                LoadProducts();
            }
        }
    }
}
