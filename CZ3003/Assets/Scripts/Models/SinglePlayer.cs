using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class SinglePlayer
    {
    

        public List<StageProgress> AllStageProgress { get; set; }
        public int currStage { get; set; }
        public SinglePlayer()
        {

        }
        public SinglePlayer(List<StageProgress> AllStageProgress,int currStage)
        {
            this.AllStageProgress = AllStageProgress;
            this.currStage = currStage;

        }
    }
}
