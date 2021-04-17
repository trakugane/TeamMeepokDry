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
    public class zLeaderboardDisplayPerfTest
    {
        [SerializeField]
        LoginValidate loginValidate;
        Assets.DatabaseInit dbInit;
        UserPlayer usr;
        ButtonManager buttonManager;
        //GameplayManager gpm;
        //UserPlayer usrPlayer;
        private GameObject ErrorMessage, Email, Password, textTemplate;
        //StageSelectManager stageSelectManager;
        //Button stage1;
        //Text errorText;
        LeaderboardManager leaderboardManager;
        GameObject leaderboardListText;
        GameObject emailText, scoreText;
        LeaderboardListText ll;
        GameObject gameObject = new GameObject();

        [SetUp]
        public void SetUp()
        {
            
            buttonManager = gameObject.AddComponent<ButtonManager>();
            leaderboardManager = gameObject.AddComponent<LeaderboardManager>();
            dbInit = gameObject.AddComponent<Assets.DatabaseInit>();
            //ll = gameObject.AddComponent<LeaderboardListText>();
            leaderboardListText = new GameObject();
            ll = leaderboardListText.AddComponent<LeaderboardListText>();

            emailText = new GameObject();
            scoreText = new GameObject();

            emailText.AddComponent<Text>();
            scoreText.AddComponent<Text>();

            ll.setText(emailText.GetComponent<Text>(), scoreText.GetComponent<Text>());

            leaderboardManager.setTextTemplate(leaderboardListText); 
        }

        [UnityTest]
        public IEnumerator TestToCheckPerformanceOfDisplayingLeaderboard()
        {
            //buttonManager.LoadScene("LeaderBoard");

            yield return null;
            //yield return new WaitForSeconds(1f);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(gameObject);
            GameObject.Destroy(leaderboardManager);
            GameObject.Destroy(emailText);
            GameObject.Destroy(scoreText);
            dbInit = null;



        }
    }
}
