using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.scripts
{
    internal class Utils
    {
        public MainWindow mainWindow { get; }
        private Data data;

        public Utils(MainWindow mainWindow, Data data)
        {
            this.mainWindow = mainWindow;
            this.data = data;
        }

        public void CloseNewLogin()
        {
            this.mainWindow.MainGrid.IsEnabled = true;
            this.mainWindow.NewLoginGrid.Visibility = Visibility.Hidden;
            this.mainWindow.UsernameBox.Text = "";
            this.mainWindow.NameBox.Text = "";
            this.mainWindow.Password.Password = "";
        }

        public void InitLogins()
        {
            string[] line = File.ReadAllLines(this.data.fileName);

            for (int i = 0; i < line.Length; i++)
            {
                string[] s = line[i].Split(' ');
                Login l = new Login(s[0], s[1], s[2]);
                Login.logins.Add(l);
            }

            Login.logins.ForEach(login =>
            {
                this.AddToLoginList(login.name);
            });
        }

        public void AddToLoginList(string name)
        {
            this.mainWindow.LoginList.Items.Add(name);
        }

        public async void printWarining(string text, int time)
        {
            this.mainWindow.NewLoginWarningLabel.Content = text;
            this.mainWindow.NewLoginWarningLabel.Visibility = Visibility.Visible;
            await Task.Delay(time);
            this.mainWindow.NewLoginWarningLabel.Visibility = Visibility.Hidden;
        }
    }
}