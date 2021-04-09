using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class AssignmentRecord
    {

        public int score { get; set; }
        public String userEmail { get; set; }

        public AssignmentRecord() { }
        public AssignmentRecord(int score, String userEmail)
        {
            this.score = score;
            this.userEmail = userEmail;
        }
    }
}
