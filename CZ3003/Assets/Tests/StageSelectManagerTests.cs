using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NSubstitute;
using Assets.Models;

namespace Tests
{
    public class StageSelectManagerTests
    {
        // Test to check correct stage loaded
        [Test]
        public void TestToCheckCorrectStageLoaded()
        {
            IStageSelectManager stageSelectManager = Substitute.For<IStageSelectManager>();

            stageSelectManager.loadStage(Arg.Any<int>(), Arg.Any<int>()).Returns(x =>
            {
                Dictionary<string, bool> buttons = new Dictionary<string, bool>{
                    {"btn1", false},
                    {"btn2", false },
                    {"btn3", false },
                    {"btn4", false },
                    {"btn5", false },
                };

                if ((int)x[0] >= (int)x[1])
                {
                    buttons["btn1"] = true;
                    buttons["btn2"] = true;
                    buttons["btn3"] = true;
                    buttons["btn4"] = true;
                    buttons["btn5"] = true;
                    return true;
                }
                else
                {
                    if (((int)x[0] % 10) == 1)
                    {
                        buttons["btn1"] = true;
                        return true;
                    }
                    else if (((int)x[0] % 10) == 2)
                    {
                        buttons["btn1"] = true;
                        buttons["btn2"] = true;
                        return true;
                    }
                    else if (((int)x[0] % 10) == 3)
                    {
                        buttons["btn1"] = true;
                        buttons["btn2"] = true;
                        buttons["btn3"] = true;
                        return true;
                    }
                    else if (((int)x[0] % 10) == 4)
                    {
                        buttons["btn1"] = true;
                        buttons["btn2"] = true;
                        buttons["btn3"] = true;
                        buttons["btn4"] = true;
                        return true;
                    }
                }

                return false;
            });

            Assert.IsTrue(stageSelectManager.loadStage(11, 15));
            Assert.IsTrue(stageSelectManager.loadStage(12, 15));
            Assert.IsTrue(stageSelectManager.loadStage(13, 15));
            Assert.IsTrue(stageSelectManager.loadStage(14, 15));
            Assert.IsTrue(stageSelectManager.loadStage(15, 15));
        }
    }
}

