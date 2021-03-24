using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class StageProgress
    {
        public int Stage { get; set; }
        public int Attempt { get; set; }
        public StageProgress()
        {

        }
        public StageProgress(int Stage,int Attempt)
        {
            this.Stage=Stage;
            this.Attempt = Attempt;
        }
    }
}
