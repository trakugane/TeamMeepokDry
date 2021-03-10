using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    class SinglePlayer
    {
        public int Stage { get; set; }
        public int Attempt { get; set; }

        public SinglePlayer()
        {

        }
        public SinglePlayer(int Stage,int Attempt)
        {
            this.Stage=Stage;
            this.Attempt = Attempt;

        }
    }
}
