using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Assets.Models;
using NUnit.Framework;
using UnityEngine.TestTools;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NSubstitute;


namespace Tests
{
    public class DBManagerTests
    {
        //private Assets.DatabaseInit dbInit;
        Assets.DatabaseInit dbInit;
        private IMongoDatabase db;
        private static MongoClient dc;
        private static string databaseName = "MPDry";
        IDBManager dbManager;

        [SetUp]
        public void SetUp()
        {
            dbManager = Substitute.For<IDBManager>();
        }

        // Test to validate account creation
        [Test]
        public void TestToValidateAccountCreation()
        {
            string email = "example@gmail.com";
            string password = "examplepw";
            string name = "HelloWorld";
            dbManager.validateAccountCreation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            Assert.IsTrue(dbManager.validateAccountCreation(email, password, name));
        }

        // Test to validate user
        [Test]
        public void TestToValidateUser()
        {
            string email = "example@gmail.com";
            string password = "examplepw";
            string name = "HelloWorld";
            Assets.Models.User usr = new User(password, null, 1, name, email, null, new List<string>());
            dbManager.validateUser(Arg.Any<string>(), Arg.Any<string>()).Returns(usr);
            Assert.AreEqual(usr, dbManager.validateUser(email, password));
        }

        // Test to check if email exists
        [Test]
        public void TestToCheckEmail()
        {
            string email = "t@gmail.com";
            dbManager.checkEmail(Arg.Any<string>()).Returns(true);
            Assert.IsTrue(dbManager.checkEmail(email));
        }
    }
}