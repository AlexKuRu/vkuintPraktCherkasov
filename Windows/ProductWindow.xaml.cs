using Chem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            tbRole.Text = Role;
            if(tbRole.Text == "Админ")
            {
                panelAdminButtons.Visibility = Visibility.Visible;
            }
            else
            {
                panelAdminButtons.Visibility = Visibility.Collapsed;
            }
            LoadProducts();
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

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для редактирования.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dynamic selected = dgProducts.SelectedItem;
            int productId = selected.id_product; 

            var product = App.Context.Product.Find(productId);
            if (product != null)
            {
                EditWindow edit = new EditWindow(product);
                if (edit.ShowDialog() == true)
                    LoadProducts();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для удаления.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dynamic selected = dgProducts.SelectedItem;
            int productId = selected.id_product;

            var product = App.Context.Product.Find(productId);
            if (product == null) return;

            if (MessageBox.Show($"Удалить товар \"{product.ProductName}\"?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Context.Product.Remove(product);
                App.Context.SaveChanges();
                LoadProducts();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(null);

            if (editWindow.ShowDialog() == true)
            {
                LoadProducts(); 
            }
        }

    }
}
