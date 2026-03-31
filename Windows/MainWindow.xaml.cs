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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Entities.User Role)
        {
            InitializeComponent();
            User currentUser = App.CurrentUser;
            if (currentUser != null) {
                tbFio.Text = currentUser.FirstName;
            }
            else
            {
                tbFio.Text = "Пользователь не определен";
            }
            DataContext = this;

        }

        private void ConfigureUIByRole()
        {
            //switch (CurrentUserRole)
            //{
            //    case "Admin":
            //        AdminPanel.Visibility = Visibility.Visible;
            //        ManagerPanel.Visibility = Visibility.Visible;
            //        break;
            //    case "Manager":
            //        AdminPanel.Visibility = Visibility.Collapsed;
            //        ManagerPanel.Visibility = Visibility.Visible;
            //        break;
            //    case "User":
            //        AdminPanel.Visibility = Visibility.Collapsed;
            //        ManagerPanel.Visibility = Visibility.Collapsed;
            //        break;
            //}

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
