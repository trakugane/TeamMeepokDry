using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameplayManager : MonoBehaviour
{

    public int selectedStageValue;
    public GameObject imgStage1;
    public GameObject imgStage2;
    public GameObject imgStage3;
    public GameObject imgStage4;
    public GameObject resultPanel;
    public bool playingGame;
    public int totalNoOfQn = 5;
    public GameObject currentQn;
    public Button btnNextStage;
    public Button btnPlayAgain;
    public Button btnStageSelect;
    public bool resultPass;
    /*public GameObject Qntxt;*/


    /*public void onClick()
    {
        // Check if button is correct answer.

        // When correct
        Player.zhPlayer.incrementProgress();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log(Qntxt.GetComponent<Text>().text);*/
        selectedStageValue = Player.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue: " + selectedStageValue);
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        print(GameObject.Find("ScoreText").GetComponent<Text>().text);
        playingGame = true;
        resultPass = false;
        disableAllStageImage();
        displaySelectedStageImage();
        disableResultScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame == true)
        {
            checkGameplay();
        }
    }

    void displaySelectedStageImage()
    {
        if ((selectedStageValue % 10) == 1)
            imgStage1.SetActive(true);
        if ((selectedStageValue % 10) == 2)
            imgStage2.SetActive(true);
        if ((selectedStageValue % 10) == 3)
            imgStage3.SetActive(true);
        if ((selectedStageValue % 10) == 4)
            imgStage4.SetActive(true);
    }

    void disableAllStageImage()
    {
        imgStage1.SetActive(false);
        imgStage2.SetActive(false);
        imgStage3.SetActive(false);
        imgStage4.SetActive(false);
    }

    void disableResultScreen()
    {
        resultPanel.SetActive(false);
    }

    void checkGameplay()
    {
        if (int.Parse(currentQn.GetComponent<Text>().text) > totalNoOfQn)
        {
            currentQn.GetComponent<Text>().text = (int.Parse(currentQn.GetComponent<Text>().text) - 1).ToString();
            playingGame = false;
            enableResultScreen();
        }
    }

    void enableResultScreen()
    {
        resultPanel.SetActive(true);
        GameObject.Find("CurrentScoreText").GetComponent<Text>().text = GameObject.Find("ScoreText").GetComponent<Text>().text;
        if (int.Parse(GameObject.Find("CurrentScoreText").GetComponent<Text>().text) < (totalNoOfQn / 2))
        {
            GameObject.Find("BtnNextStage").SetActive(false);
            GameObject.Find("BtnPlayAgain").SetActive(true);
        }
        else if (int.Parse(GameObject.Find("CurrentScoreText").GetComponent<Text>().text) > (totalNoOfQn / 2))
        {
            resultPass = true;
            GameObject.Find("BtnNextStage").SetActive(true);
            GameObject.Find("BtnPlayAgain").SetActive(false);
            updateUser();
        }
    }

    void updateUser()
    {
        Player.userPlayer.incrementProgress();
        // Send data to database here
    }
}
