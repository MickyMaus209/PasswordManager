using PasswordManager.scripts;
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

namespace PasswordManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data;
        private Utils utils;

        public MainWindow()
        {
            InitializeComponent();
            this.data = new Data(this);
            utils = new Utils(this, data);
            this.utils.InitLogins();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginList.Items.Contains(NameBox.Text))
            {
                utils.printWarining("This login already exists!", 3500);
                return;
            }

            NewLoginWarningLabel.Content = "Enter all informations!";
            if (NameBox.Text == "" || UsernameBox.Text == "" || Password.Password == "")
            {
                utils.printWarining("One of the entry is empty!", 3500);
                return;
            }

            this.data.WriteNewLogin(NameBox.Text, UsernameBox.Text, Password.Password);
            utils.AddToLoginList(new Login(NameBox.Text, UsernameBox.Text, Password.Password).name);
            utils.CloseNewLogin();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            utils.CloseNewLogin();
        }

        private void NewLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsEnabled = false;
            NewLoginGrid.Visibility = Visibility.Visible;
            Password.Password = "";
            UsernameBox.Text = "";
            NameBox.Text = "";
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginList.SelectedItem != null)
            {
                Login login = new Login(LoginList.SelectedItem.ToString()).GetObjectByName();

                if (login != null)
                    data.RemoveLogin(login.name);
            }
        }

        private void Login_DoubleClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login(LoginList.SelectedItem.ToString()).GetObjectByName();

            MainGrid.IsEnabled = false;
            CredentitalsGrid.Visibility = Visibility.Visible;

            NameText.Text = login.name;
            UserNameText.Text = login.userName;
            PasswordText.Text = login.password;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CredentitalsGrid.Visibility = Visibility.Hidden;
            MainGrid.IsEnabled = true;
        }
    }
}