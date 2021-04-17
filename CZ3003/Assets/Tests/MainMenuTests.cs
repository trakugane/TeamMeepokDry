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
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NSubstitute;

namespace Tests
{

    public class MainMenuTests
    {

        // Test to check if account type is Teacher or Student
        [Test]
        public void TestCheckAccountType()
        {
            IMainMenuManager mainMenu = Substitute.For<IMainMenuManager>();

            // Return true if and only if argument given is 1
            mainMenu.checkAccountType(Arg.Any<int>()).Returns(x => (int)x[0] == 1);
            
            // Teacher Account
            Assert.IsTrue(mainMenu.checkAccountType(1));
            
            // Student Account
            Assert.IsFalse(mainMenu.checkAccountType(0));
        }
    }
}
