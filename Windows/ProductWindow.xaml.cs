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
        public ProductWindow(string Role)
        {
            InitializeComponent();
            LoadProducts();
            tbRole.Text = Role;
            if(tbRole.Text == "Админ")
            {
                panelAdminButtons.Visibility = Visibility.Visible;
            }
            else
            {
                panelAdminButtons.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadProducts()
        {
            var query = App.Context.Product
                .Include("Category")
                .Include("Manufacturer")
                .Include("Suplier")
                .AsQueryable();

            var products = query.ToList();
            dgProducts.ItemsSource = products;

            tbCount.Text = $"Найдено товаров: {products.Count}";
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow(null);
            edit.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
