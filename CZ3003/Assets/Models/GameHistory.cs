using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    class GameHistory
    {
        public int Rank { get; set; }
        public String Category { get; set; }
        public int Points { get; set; }

        public DateTime Date { get; set; }

        public GameHistory() { }
        public GameHistory( int Rank, String Category, int Points, DateTime Date)
        {
            this.Rank = Rank;
            this.Category = Category;
            this.Points = Points;
            this.Date = Date;
        }
    }
}
