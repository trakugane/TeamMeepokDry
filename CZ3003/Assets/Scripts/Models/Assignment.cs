using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Assignment
    {
        public ObjectId id { get; set; }
        public String assignmentName { get; set; }
        public String creatorEmail { get; set; }
        public List<AssignmentRecord> result { get; set; }
        public Assignment() { }

        public Assignment(String assignmentName,String assnCreatorEmail,List<AssignmentRecord> result,int score)
        {
            this.assignmentName = assignmentName;
            this.creatorEmail = assnCreatorEmail;
            this.result = result;
        }

    }
}
