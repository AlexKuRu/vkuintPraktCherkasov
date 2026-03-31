using Chem.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chem.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DbSet<User> Users { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var currentuser = App.Context.User
                .FirstOrDefault(p => p.Login == tbLogin.Text && p.Password == pbPassword.Password);
            if (currentuser != null) {
                App.CurrentUser = currentuser;
                MainWindow main = new MainWindow(currentuser);
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа");
            }
        }
    }
}
