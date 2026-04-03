using Chem.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Xml.Linq;

namespace Chem.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Product _product;         
        public EditWindow(Product product)
        {
            InitializeComponent();
            LoadComboBoxData();
            _product = product;
            if (_product != null)
                LoadProductData();
            else
                Title = "Новый товар";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbProductName.Text))
                {
                    MessageBox.Show("Введите наименование");
                    return;
                }

                int quantity = 0;
                int.TryParse(tbQuantity.Text, out quantity);
                byte discount = 0;
                byte.TryParse(tbDiscount.Text, out discount);

                if (_product == null)
                {
                    _product = new Product();
                    App.Context.Product.Add(_product);
                }

                _product.ProductName = tbProductName.Text.Trim();
                _product.Description = tbDescription.Text?.Trim();
                _product.Quantity = quantity;
                _product.Discount = discount;

                App.Context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите изображение товара";
            openFileDialog.Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = ms;
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        imgProduct.Source = bitmap;
                    }

                    var _tempImage = imageBytes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void LoadComboBoxData()
        {
            string connString = App.Context.Database.Connection.ConnectionString; // берём строку из EF

            // Загрузка категорий
            var categories = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_category, CategoryName FROM Category", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        categories.Add(new Category { id_category = reader.GetInt32(0), CategoryName = reader.GetString(1) });
                }
            }

            // Загрузка производителей
            var manufacturers = new List<Manufacturer>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_manufacturer, Name FROM Manufacturer", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        manufacturers.Add(new Manufacturer { id_manufacturer = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }

            // Загрузка поставщиков
            var supliers = new List<Suplier>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_suplier, Name FROM Suplier", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        supliers.Add(new Suplier { id_suplier = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }
        }
        private void LoadProductData()
        {
            tbProductName.Text = _product.ProductName;
            tbDescription.Text = _product.Description;
            tbQuantity.Text = _product.Quantity.ToString();
            tbDiscount.Text = _product.Discount.ToString();
        }

    }
}
