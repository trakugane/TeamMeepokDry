﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class User
    {
        public String Password { get; set; }
        public int accountType { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public SinglePlayer spProgress { get; set; }
        public PVP mpStatus { get; set; }

        public User() { }
        public User(String Password,SinglePlayer spProgress,int accountType,String name,String email,PVP mpStatus)
        {
            this.Password = Password;
            this.spProgress = spProgress;
            this.accountType = accountType;
            this.name = name;
            this.email = email;
            this.mpStatus = mpStatus;
        }
    }
}
