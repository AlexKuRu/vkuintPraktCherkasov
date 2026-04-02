using Chem.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Chem.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Product _product;          // редактируемый товар (если null – добавление)
        private bool _isEditMode;
        public EditWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            _isEditMode = product != null;

            LoadComboBoxData();
            if (_isEditMode)
            {
            }
            else
            {
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка названия
                if (string.IsNullOrWhiteSpace(tbProductName.Text))
                {
                    MessageBox.Show("Введите наименование товара", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbProductName.Focus();
                    return;
                }

                // Проверка остатка
                if (!int.TryParse(tbQuantity.Text, out int quantity) || quantity < 0)
                {
                    MessageBox.Show("Остаток должен быть целым неотрицательным числом", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbQuantity.Focus();
                    return;
                }

                if (!byte.TryParse(tbDiscount.Text, out byte discount) || discount > 100)
                {
                    MessageBox.Show("Скидка должна быть целым числом от 0 до 100", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbDiscount.Focus();
                    return;
                }


                // Получение ID из ComboBox (могут быть null, если не выбраны)
                int? categoryId = cbCategory.SelectedValue as int?;
                int? manufacturerId = cbManufacturer.SelectedValue as int?;
                int? suplierId = cbSuplier.SelectedValue as int?;

                if (_isEditMode)
                {
                    // Редактирование
                    _product.ProductName = tbProductName.Text.Trim();
                    _product.Description = tbDescription.Text?.Trim();
                    _product.Quantity = quantity;
                    _product.Discount = discount;
                    _product.id_category = categoryId;
                    _product.id_manufacturer = manufacturerId;
                    _product.id_suplier = suplierId;

                    // Для EF Core (Microsoft.EntityFrameworkCore)
                    App.Context.Entry(_product).State = EntityState.Modified;
                }
                else
                {
                    // Добавление
                    var newProduct = new Product
                    {
                        ProductName = tbProductName.Text.Trim(),
                        Description = tbDescription.Text?.Trim(),
                        Quantity = quantity,
                        Discount = discount,
                        id_category = categoryId,
                        id_manufacturer = manufacturerId,
                        id_suplier = suplierId,
                        Image = null
                    };
                    App.Context.Product.Add(newProduct);
                }

                App.Context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadComboBoxData()
        {
            // Загрузка категорий
            cbCategory.ItemsSource = App.Context.Category.OrderBy(c => c.CategoryName).ToList();
            cbCategory.SelectedValuePath = "Id";
            cbCategory.DisplayMemberPath = "CategoryName";

            // Загрузка производителей
            cbManufacturer.ItemsSource = App.Context.Manufacturer.OrderBy(m => m.Name).ToList();
            cbManufacturer.SelectedValuePath = "Id";
            cbManufacturer.DisplayMemberPath = "Name";

            // Загрузка поставщиков
            cbSuplier.ItemsSource = App.Context.Suplier.OrderBy(s => s.Name).ToList();
            cbSuplier.SelectedValuePath = "Id";
            cbSuplier.DisplayMemberPath = "Name";
        }
        private void LoadProductData()
        {
            // Принудительно загружаем связанные данные (если они ещё не загружены)
            App.Context.Entry(_product).Reference(p => p.Category).Load();
            App.Context.Entry(_product).Reference(p => p.Manufacturer).Load();
            App.Context.Entry(_product).Reference(p => p.Suplier).Load();

            tbProductName.Text = _product.ProductName;
            tbDescription.Text = _product.Description;
            tbQuantity.Text = _product.Quantity.ToString();
            tbDiscount.Text = _product.Discount.ToString();

            if (_product.Category != null)
                cbCategory.SelectedValue = _product.Category.id_category;
            if (_product.Manufacturer != null)
                cbManufacturer.SelectedValue = _product.Manufacturer.id_manufacturer;
            if (_product.Suplier != null)
                cbSuplier.SelectedValue = _product.Suplier.id_suplier;

            // Отображение изображения, если есть
            if (_product.Image != null && _product.Image.Length > 0)
            {
                using (var ms = new MemoryStream(_product.Image))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgProduct.Source = bitmap;
                }
            }
        }

    }
}
