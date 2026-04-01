using Chem.Entities;
using System;
using Chem.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string Role)
        {
            InitializeComponent();
            User currentUser = App.CurrentUser;
            if (currentUser != null) {
                tbFio.Text = currentUser.FirstName;
                tbRole.Text = Role.ToString();
            }
            else
            {
                tbFio.Text = "Пользователь не определен";
            }
            DataContext = this;

        }


        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var role = tbRole.Text;
            LoginWindow log = new LoginWindow();
            this.Close();
            log.Show();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            var role = tbRole.Text;
            ProductWindow prod = new ProductWindow(role);
            this.Close();
            prod.Show();
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
