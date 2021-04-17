using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NSubstitute;

namespace Tests
{
    public class WorldManagerTests
    {
        // Check that the correct world is loaded
        [Test]
        public void CheckWorldSelect()
        {
            IWorldSelectManager worldSelect = Substitute.For<IWorldSelectManager>();
            worldSelect.loadWorld(Arg.Any<int>()).Returns(x => 
                ((int)x[0] > 10 && (int)x[0] <= 15) ||
                ((int)x[0] > 20 && (int)x[0] <= 25) ||
                ((int)x[0] > 30 && (int)x[0] <= 35) ||
                ((int)x[0] > 40 && (int)x[0] <= 45)
                );
            
            Assert.IsTrue(worldSelect.loadWorld(13));
            Assert.IsTrue(worldSelect.loadWorld(23));
            Assert.IsTrue(worldSelect.loadWorld(33));
            Assert.IsTrue(worldSelect.loadWorld(43));
        }



    }
}
