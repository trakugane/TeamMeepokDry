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

namespace Tests
{
    public class InputValidationTests
    {

        private InputsValidation validateInputs;


        [SetUp]
        public void SetUp()
        {
            validateInputs = new InputsValidation();
        }


        // Test to check if email is in the correct format
        [Test]
        public void CheckIfEmailCorrectFormat()
        {
            string email = "t@gmail.com";
            Assert.IsTrue(validateInputs.checkEmail(email));
        }

        // Test to check if password is of correct length
        [Test]
        public void CheckPasswordLength()
        {
            string p = "password1";
            string cp = "password1";
            string nomatch = "password";

            // Passwords match and meet security criteria
            Assert.AreEqual("", validateInputs.checkPassword(p, cp));
            
            // Passwords do not match
            Assert.AreEqual("Password and Confirm Password does not match", validateInputs.checkPassword(p, nomatch));

            // Passwords match but do not meet security criteria
            string weakpw = "pw";
            string weakcpw = "pw";
            Assert.AreEqual("Password must be at least 8 characters.", validateInputs.checkPassword(weakpw, weakcpw));
        }

        // Test to check length of name
        [Test]
        public void CheckLengthOfName()
        {
            // Name meets criteria
            string name = "HelloWorld";
            Assert.AreEqual("", validateInputs.checkName(name));

            // Name do not meet criteria
            string shortname = "name";
            Assert.AreEqual("Name must be at least 8 characters.", validateInputs.checkName(shortname));
        }
    }
}
