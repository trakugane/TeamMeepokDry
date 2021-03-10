using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    class SinglePlayer
    {
    

        public List<StageProgress> AllStageProgress { get; set; }

        public SinglePlayer()
        {

        }
        public SinglePlayer(List<StageProgress> AllStageProgress)
        {
            this.AllStageProgress = AllStageProgress;

        }
    }
}
