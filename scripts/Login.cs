using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.scripts
{
    internal class Login
    {
        public string name { get; }
        public string userName { get; }
        public string password { get; }
        public static List<Login> logins = new List<Login>();
        public static Dictionary<string, Login> login = new Dictionary<string, Login>();

        public Login(string name, string userName, string password)
        {
            this.name = name;
            this.userName = userName;
            this.password = password;
            login.Add(this.name, this);
        }

        public Login(string name)
        {
            this.name = name;
        }

        public Login GetObjectByName()
        {
            Login l;
            if (login.TryGetValue(this.name, out l))
            {
                return l;
            }
            return null;
        }
    }
}