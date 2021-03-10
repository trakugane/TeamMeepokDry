using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    class PVP
    {
        public int AccumulatedPoints { get; set; }
        public List<GameHistory> PastGame { get; set; }

        public PVP()
        {

        }
        public PVP(int AccumulatedPoints,List<GameHistory> PastGame)
        {
            this.AccumulatedPoints = AccumulatedPoints;
            this.PastGame = PastGame;
        }
    }
}
