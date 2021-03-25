using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Question
    {
        public ObjectId id { get; set; }
        public String questionTitle { get; set; }
        public String questionType { get; set; }
        public int answerInt { get; set; }
        public String creatorEmail { get; set; }

        public Question() { }
        public Question (String questionTitle,String questionType,int answerInt,String creatorEmail)
        {
            this.questionTitle = questionTitle;
            this.questionType = questionType;
            this.answerInt = answerInt;
            this.creatorEmail = creatorEmail;
        }

    }
}
