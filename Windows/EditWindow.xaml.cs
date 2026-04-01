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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private AppContext _context;
        private Product _product;          
        private bool _isEditMode;
        public EditWindow()
        {
            InitializeComponent();
            _context = new ChemShopContext();
            _product = product;
            _isEditMode = product != null;

            LoadComboBoxData();
            if (_isEditMode)
            {
                LoadProductData();
                Title = "Редактирование товара";
                BtnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                Title = "Добавление товара";
                BtnDelete.Visibility = Visibility.Collapsed;
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

        }
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadComboBoxData()
        {
            // Загрузка категорий
            cbCategory.ItemsSource = _context.Categories.OrderBy(c => c.CategoryName).ToList();
            cbCategory.SelectedValuePath = "Id";
            cbCategory.DisplayMemberPath = "CategoryName";

            // Загрузка производителей
            cbManufacturer.ItemsSource = _context.Manufacturers.OrderBy(m => m.Name).ToList();
            cbManufacturer.SelectedValuePath = "Id";
            cbManufacturer.DisplayMemberPath = "Name";

            // Загрузка поставщиков
            cbSuplier.ItemsSource = _context.Supliers.OrderBy(s => s.Name).ToList();
            cbSuplier.SelectedValuePath = "Id";
            cbSuplier.DisplayMemberPath = "Name";
        }
    }
}
