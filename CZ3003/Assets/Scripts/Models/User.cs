using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using UnityEngine;

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

        //tba to constructor
        public List<String> assignmentAssigned { get; set; }

        public User() { }
        public User(String Password,SinglePlayer spProgress,int accountType,String name,String email,PVP mpStatus,List<String> assignmentAssigned)
        {
            this.Password = Password;
            this.spProgress = spProgress;
            this.accountType = accountType;
            this.name = name;
            this.email = email;
            this.mpStatus = mpStatus;
            this.assignmentAssigned = assignmentAssigned;
        }
    }
}
