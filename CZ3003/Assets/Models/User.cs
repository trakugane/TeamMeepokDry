using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public int currProgress { get; set; }
        public int accountType { get; set; }
        public String name { get; set; }
        public String email { get; set; }

        public User() { }
        public User(String Username,String Password,int currProgress,int accountType,String name,String email)
        {
            this.Username = Username;
            this.Password = Password;
            this.currProgress = currProgress;
            this.accountType = accountType;
            this.name = name;
            this.email = email;
        }
    }
}
