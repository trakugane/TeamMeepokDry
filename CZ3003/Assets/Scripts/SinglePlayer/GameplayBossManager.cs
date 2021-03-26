using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Linq;

using Random = UnityEngine.Random;

public class GameplayBossManager : MonoBehaviour
{

    public int selectedStageValue;
    public GameObject timerText;
    public GameObject resultPanel;
    public GameObject currentQn;
    public Button btnNextStage;
    public Button btnPlayAgain;
    public Button btnStageSelect;
    public float timerCurrent = 0;
    public bool playingGame;
    public bool resultPass;
    public Image imgMapLandColour;

    public GameObject QnText;

    public string currentQuestion; // + selectedstagevalue;

    public int gamewin = 3; //default, havent play before
    public int size = 5;

    public int a;
    public int b;
    public int ans;
    public char operandchar;
    public int equalindex;
    public int operatorindex;

    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;

    public GameObject ScoreText;
    int playerscore = 0;

    int curQn = 1;
    public int totalNoOfQn = 5;
    public int count = 5;

    public GameObject win;
    public GameObject lose;



    //Boss Levels
    public string[] QnA15 = new string[] { "18+12=", "5+14=", "7+54=", "13+32=", "34+45=" };
    public string[] QnA25 = new string[] { "46-32=", "14-5=", "75-2=", "61-39=", "9-5=" };
    public string[] QnA35 = new string[] { "4x12=", "14x3=", "2x7=", "6x9=", "9x5=" };
    public string[] QnA45 = new string[] { "95/5=", "36/2=", "45/5=", "56/7=", "9/3=" };
    public string[] copyQnA;
    public List<string> tempQnA = new List<string>();


    public bool stillcontinuing = false;


    // Start is called before the first frame update
    void Start()
    {
        selectedStageValue = Player1.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue" + selectedStageValue);
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        playingGame = true;
        resultPass = false;
        disableResultScreen();
        timerCurrent = 6;
        setTimer(timerCurrent);
        checkWorld(selectedStageValue);
        addBtnResultScreenListener();
        changeStageColor(selectedStageValue);
    }

    public void checkWorld(int selectedStage)
    {
        if (selectedStage == 15)
            copyQnA = QnA15;
        else if (selectedStage == 25)
            copyQnA = QnA25;
        else if (selectedStage == 35)
            copyQnA = QnA35;
        else
            copyQnA = QnA45;

        List<string> copyQnAList = new List<string>(copyQnA);

        for (int i = 0; i < size; i++)
        {

            Debug.Log("Before Question " + i + " " + copyQnAList[i]);
        }

        for (int i = 0; i < size; i++)
        {
            int rand = Random.Range(0, copyQnAList.Count);
            tempQnA.Add(copyQnAList[rand]);
            copyQnAList.RemoveAt(rand);

            Debug.Log("After Question " + i + " " + tempQnA[i]);
        }
        generateQuestion();

    }

    public void generateQuestion()
    {
        if (gamewin == 1)
        {
            size--;
        }
        else if (gamewin == 2)
            size--;

        for (int index = 0; index < size; index++)
        {
            currentQuestion = tempQnA[index];
            Debug.Log(currentQuestion);
            fetchQn();
        }

    }

    public void fetchQn()
    {

        for (int i = 0; i < currentQuestion.Length; i++)
        {
            if (currentQuestion[i] == '=')
            {
                equalindex = i;
            }
            else if ((currentQuestion[i] == '+') || (currentQuestion[i] == '-') || (currentQuestion[i] == 'x') || (currentQuestion[i] == '/'))
            {
                operandchar = currentQuestion[i];
                operatorindex = i;
            }
        }
        string str1 = "";
        string str2 = "";

        for (int i = 0; i < currentQuestion.Length; i++)
        {
            if (i < operatorindex)
            {

                str1 = str1 + currentQuestion[i];

            }
            if (i < equalindex && i > operatorindex)
            {
                str2 = str2 + currentQuestion[i];
            }
        }
        a = int.Parse(str1);
        b = int.Parse(str2);

        if (operandchar == '+')
        {
            ans = a + b;
        }
        else if (operandchar == '-')
        {
            ans = a - b;
        }
        else if (operandchar == 'x')
        {
            ans = a * b;
        }
        else
            ans = a / b;

        Debug.Log(ans);

        setText();
        setBtnAns();
    }

    public void setText()
    {
        string msg = "Question: " + a + operandchar + b + " ?";
        QnText.GetComponent<Text>().text = msg;
    }

    public void setBtnAns()
    {
        int random1 = UnityEngine.Random.Range(0, 99);
        int random2 = UnityEngine.Random.Range(0, 99);
        int random3 = UnityEngine.Random.Range(0, 99);

        string strans = ans.ToString();
        string strrandom1 = random1.ToString();
        string strrandom2 = random2.ToString();
        string strrandom3 = random3.ToString();


        //
        ans1.GetComponentInChildren<Text>().text = strans;
        ans2.GetComponentInChildren<Text>().text = strrandom1;
        ans3.GetComponentInChildren<Text>().text = strrandom2;
        ans4.GetComponentInChildren<Text>().text = strrandom3;

        //check for answer 
        ans1.onClick.RemoveAllListeners();
        ans1.onClick.AddListener(() => onButtonClicked(ans1));

        ans2.onClick.RemoveAllListeners();
        ans2.onClick.AddListener(() => onButtonClicked(ans2));


        ans3.onClick.RemoveAllListeners();
        ans3.onClick.AddListener(() => onButtonClicked(ans3));

        ans4.onClick.RemoveAllListeners();
        ans4.onClick.AddListener(() => onButtonClicked(ans4));


        Shuffle(strans, strrandom1, strrandom2, strrandom3);
    }

    public void Shuffle(string strans, string strrandomA, string strrandomB, string strrandomC) //shuffle ans boxes
    {
        var deck = new List<string>();
        deck.Add(strans);
        deck.Add(strrandomA);
        deck.Add(strrandomB);
        deck.Add(strrandomC);
        deck.Add(null);// nulls are allowed for reference type list

        for (int i = 0; i < 4; i++)
        {
            string temp = deck[i];
            int randomIndex = Random.Range(0, 4);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }


        ans1.GetComponentInChildren<Text>().text = deck[0];
        ans2.GetComponentInChildren<Text>().text = deck[1];
        ans3.GetComponentInChildren<Text>().text = deck[2];
        ans4.GetComponentInChildren<Text>().text = deck[3];


        Debug.Log("Listening");
        //check for answer 
        ans1.onClick.RemoveAllListeners();
        ans1.onClick.AddListener(() => onButtonClicked(ans1));

        ans2.onClick.RemoveAllListeners();
        ans2.onClick.AddListener(() => onButtonClicked(ans2));


        ans3.onClick.RemoveAllListeners();
        ans3.onClick.AddListener(() => onButtonClicked(ans3));

        ans4.onClick.RemoveAllListeners();
        ans4.onClick.AddListener(() => onButtonClicked(ans4));


    }

    public void onButtonClicked(Button btn)
    {
        Text btnText = btn.GetComponentInChildren<Text>();
        int noFromButton = int.Parse(btnText.text);
        Debug.Log("number from button is " + noFromButton);
        count = 5;

        curQn++;
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (curQn).ToString();

        if (noFromButton == ans)
        {
            gamewin = 1; //means win
            Debug.Log("gamewin = " + gamewin);
            Debug.Log("u clicked on the correct answer :" + ans);
            //GameController.gameController.points4game++;        //increment points
            playerscore++;
            ScoreText.GetComponent<Text>().text = playerscore.ToString();
            Debug.Log("Player score: " + playerscore);
            //Debug.Log(GameController.gameController.points4game);
            //Destroy(GameController.gameController.questionPanel);
            count--;
            if (count > 0)
            {
                timerCurrent = 6;
                Update();
                generateQuestion();
            }
            //else
            //Go to score page

        }
        else
        {
            gamewin = 2; //means lost

            Debug.Log("gamewin = " + gamewin);
            Debug.Log("u clicked on the wrong answer : " + noFromButton);

            Debug.Log("Player score: " + playerscore);
            count--;
            if (count > 0)
            {
                timerCurrent = 6;
                Update();
                generateQuestion();
            }
            //else
            //Go to score page
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (playingGame == true)
        { 
            timerCurrent -= Time.deltaTime;
            setTimer(timerCurrent);
            checkGameplay();
        }
    }


    public void setTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        timerText.GetComponent<Text>().text = time.ToString("ss");

        if (timerCurrent <= 0)
        {
            playingGame = false;
            Debug.Log("RESULT SCREEN APPEAR");
            // Result panel appear.
            enableResultScreen();
        }
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
            //GameObject.Find("BtnNextStage").SetActive(false);
            //GameObject.Find("BtnPlayAgain").SetActive(true);
            lose.SetActive(true);
            win.SetActive(false);
        }
        else if (int.Parse(GameObject.Find("CurrentScoreText").GetComponent<Text>().text) > (totalNoOfQn / 2))
        {
            resultPass = true;
            //GameObject.Find("BtnNextStage").SetActive(true);
            //GameObject.Find("BtnPlayAgain").SetActive(false);
            lose.SetActive(false);
            win.SetActive(true);
            updateUser();
        }
    }

    void updateUser()
    {
        if (selectedStageValue == Player1.userPlayer.currProg)
        {
            Player1.userPlayer.incrementProgress();
            // Send data to database here
            Assets.DatabaseInit.dbInit.updateCurrentStage(Player1.userPlayer.currProg, Player1.userPlayer.email);
        }
    }

    public void addBtnResultScreenListener()
    {
        // btnNextStage.onClick.AddListener(setNextStage);
        btnPlayAgain.onClick.AddListener(resetStage);
    }

    /*public void setNextStage()
    {
        Player.userPlayer.incrementStageValue();
        selectedStageValue = Player.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue" + selectedStageValue);
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        playingGame = true;
        curQn = 1;
        playerscore = 0;
        // Call Functions or write code here to setup next stage here,
        // i retrieve the next stage le

        disableResultScreen();
    }*/

    public void resetStage()
    {
        // Call Functions or write code here to reset stage here
        // Check if player current progress is the same as selected stage, increment attempts
        if ((Player1.userPlayer.currProg == Player1.userPlayer.selectedStageValue) && (gamewin == 2))
            Assets.DatabaseInit.dbInit.incrementCurrentStageAttempt(Player1.userPlayer.currProg, Player1.userPlayer.email);

        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        playingGame = true;
        curQn = 1;
        playerscore = 0;
        ScoreText.GetComponent<Text>().text = playerscore.ToString();
        count = 5;
        size = 5;
        gamewin = 3;

        tempQnA.Clear();
        //checkWorld(selectedStageValue);
        Start();
        disableResultScreen();
        timerCurrent = 6;
        setTimer(timerCurrent);
    }

    void changeStageColor(int stageValue)
    {
        if (stageValue / 10 == 1)
            imgMapLandColour.color = new Color32(113, 201, 109, 255);
        if (stageValue / 10 == 2)
            imgMapLandColour.color = new Color32(229, 237, 106, 255);
        if (stageValue / 10 == 3)
            imgMapLandColour.color = new Color32(250, 176, 0, 255);
        if (stageValue / 10 == 4)
            imgMapLandColour.color = new Color32(250, 100, 0, 255);
        if (stageValue / 10 == 5)
            imgMapLandColour.color = new Color32(235, 47, 47, 255);
    }
}
