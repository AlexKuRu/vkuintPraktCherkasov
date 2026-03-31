using Chem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chem.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            var product = App.Context.Product;
            LoadProducts();        
        }
        private void LoadProducts()
        {
            // Базовый запрос с Include для связанных данных
            var query = App.Context.Product
                .Include("Category")
                .Include("Manufacturer")
                .Include("Suplier")
                .AsQueryable();

            // Выполнение запроса и привязка к DataGrid
            var products = query.ToList();
            dgProducts.ItemsSource = products;

            // Обновление счетчика товаров
            tbCount.Text = $"Найдено товаров: {products.Count}";
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
