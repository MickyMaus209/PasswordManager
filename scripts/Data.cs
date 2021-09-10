using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.scripts
{
    internal class Data
    {
        public string directory { get; }
        public string fileName { get; }
        private MainWindow mainWindow;

        public Data(MainWindow mainWindow)
        {
            this.directory = $@"{Directory.GetCurrentDirectory()}\Data";
            this.fileName = $@"{this.directory}\logins.txt";
            this.Setup();
            this.mainWindow = mainWindow;
        }

        private void Setup()
        {
            if (Directory.Exists(this.directory))
            {
                return;
            }

            Directory.CreateDirectory(this.directory);

            if (File.Exists(this.fileName))
            {
                return;
            }

            File.Create(this.fileName).Close();
        }

        public void WriteNewLogin(string name, string user, string password)
        {
            string[] line = File.ReadAllLines(this.fileName);

            using (StreamWriter sw = new StreamWriter(this.fileName))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    sw.WriteLine(line[i]);
                }
                sw.WriteLine(name + " " + user + " " + password);

                sw.Flush();
                sw.Close();
            }
        }

        public void RemoveLogin(string name)
        {
            string[] line = File.ReadAllLines(this.fileName);

            using (StreamWriter sw = new StreamWriter(this.fileName))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].StartsWith(name))
                    {
                        continue;
                    }
                    sw.WriteLine(line[i]);
                }

                sw.Flush();
                sw.Close();
            }

            Login.login.Remove(name);
            Login lo = new Login(name).GetObjectByName();
            Login.logins.Remove(lo);
            this.mainWindow.LoginList.Items.Remove(name);
        }
    }
}