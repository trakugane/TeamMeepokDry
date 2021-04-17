using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using Assets.Models;
using NSubstitute;

namespace Tests
{
    public class GameControllerTests
    {
        // A Test to display playes in game room
        [Test]
        public void TestToDisplayPlayerInGameRoom()
        {
            int i = 0;
            IGameController gameController = Substitute.For<IGameController>();
            gameController.When(x => x.DisplayPlayersInGame()).Do(x =>
            {
                Dictionary<string, string> playerList = new Dictionary<string, string>
                {
                    {"Player1", "user" },
                    {"Player2", "opponent" },
                    {"Player3", "opponent" },
                    {"Player4", "opponent" }
                };
                foreach (KeyValuePair<string, string> kvp in playerList)
                {
                    if (kvp.Value != "user")
                    {
                        if (i == 0)
                        {
                            Debug.Log(kvp.Key);
                        }
                        else if (i == 1)
                        {
                            Debug.Log(kvp.Key);
                        }
                        else if (i == 2)
                        {
                            Debug.Log(kvp.Key);
                        }
                        i++;
                    }
                }

            });
            gameController.DisplayPlayersInGame();
            Assert.AreEqual(i, 3);
        }

        // Test to simulate leave game room
        [Test]
        public void TestToSimulateLeaveGameRoom()
        {
            int accumulatedPoints = 3;
            IGameController gameController = Substitute.For<IGameController>();
            gameController.When(x => x.LeaveGame()).Do(x =>
            {
                string winnerName = "Player1";
                string myName = "Player1";
                if (winnerName == myName)
                {
                    accumulatedPoints = accumulatedPoints + 3;
                }

            });
        }
    }
}
