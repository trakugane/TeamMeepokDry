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
    public class LoginPerfTest
    {
        private LoginValidate loginValidate;
        
        UserPlayer usr;
        //GameplayManager gpm;
        //UserPlayer usrPlayer;
        GameObject ErrorMessage, Email, Password;
        //StageSelectManager stageSelectManager;
        //Button stage1;
        //Text errorText;
        GameObject gameObject;
        Assets.DatabaseInit dbInit;

        [OneTimeSetUp]
        public void SetUp()
        {
            
            gameObject = new GameObject();
            loginValidate = gameObject.AddComponent<LoginValidate>();
            dbInit = gameObject.AddComponent<Assets.DatabaseInit>();
            ErrorMessage = new GameObject();
            ErrorMessage.name = "ErrorMessage";
            ErrorMessage.AddComponent<Text>();
            ErrorMessage.GetComponent<Text>().text = "Wrong Password";
            Email = new GameObject();
            Email.name = "Email";
            Email.AddComponent<InputField>();
            Email.GetComponent<InputField>().text = "t@gmail.com";
            
            Password = new GameObject();
            Password.name = "Password";
            Password.AddComponent<InputField>();
            Password.GetComponent<InputField>().text = "abcd1234";

            usr = gameObject.AddComponent<UserPlayer>();
        }

        [UnityTest]
        public IEnumerator TestToCheckPerformanceOfLogin()
        {
            loginValidate.Login();
            yield return null;
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            //yield return null;
        }

        [UnityTearDown]
        public void TearDown()
        {
            GameObject.Destroy(gameObject);
            dbInit = null;

        }
    }
}
