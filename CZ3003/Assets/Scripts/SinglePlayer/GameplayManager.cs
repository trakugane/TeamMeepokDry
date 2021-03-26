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
    public int winningCon;
    public GameObject currentQn;
    public Button btnNextStage;
    public Button btnPlayAgain;
    public Button btnStageSelect;
    public bool resultPass;
    public GameObject lose;
    public GameObject win;
    public Image imgMapLandColour;
    /*public GameObject Qntxt;*/

    public GameObject QnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;
    public GameObject ScoreText;
    int playerscore = 0;
    int count;

    //addition
    public string[] QnA11 = new string[] { "6+1=", "4+5=", "3+5=", "0+0=", "2+3=" };
    public string[] QnA12 = new string[] { "6+7=", "4+9=", "5+8=", "9+9=", "7+9=" };
    public string[] QnA13 = new string[] { "13+1=", "5+16=", "23+54=", "70+1=", "7+17=" };
    public string[] QnA14 = new string[] { "8+12=", "55+33=", "77+4=", "29+14=", "0+32=" };

    //subtraction
    public string[] QnA21 = new string[] { "4-3=", "8-5=", "10-2=", "1-0=", "9-4=" };
    public string[] QnA22 = new string[] { "18-8=", "14-11=", "25-8=", "17-8=", "24-7=" };
    public string[] QnA23 = new string[] { "35-16=", "45-29=", "16-4=", "88-32=", "60-32=" };
    public string[] QnA24 = new string[] { "18-12=", "55-43=", "37-23=", "85-39=", "92-42=" };

    //multiplication
    public string[] QnA31 = new string[] { "4x7=", "8x6=", "10x2=", "5x0=", "9x8=" };
    public string[] QnA32 = new string[] { "18x2=", "11x4=", "2x14=", "15x5=", "7x9=" };
    public string[] QnA33 = new string[] { "3x8=", "4x14=", "17x3=", "10x12=", "16x0=" };
    public string[] QnA34 = new string[] { "1x80=", "5x7=", "3x12=", "12x12=", "8x9=" };

    //division
    public string[] QnA41 = new string[] { "5/1=", "6/3=", "10/2=", "14/7=", "6/2=" };
    public string[] QnA42 = new string[] { "18/2=", "16/2=", "16/4=", "15/5=", "90/10=" };
    public string[] QnA43 = new string[] { "8/8=", "32/8=", "72/3=", "66/11=", "40/5=" };
    public string[] QnA44 = new string[] { "55/5=", "36/6=", "32/4=", "96/12=", "88/8=" };
    public string[] copyQnA;
    public List<string> tempQnA = new List<string>();

    /*public int index;*/
    public string currentQuestion; // + selectedstagevalue;

    public int ans;

    public char operandchar;
    public int equalindex;
    public int operatorindex;
    public int a;
    public int b;
    public int gamewin = 3; //default, havent play before
    public int size = 5;
    int curQn = 1;

    //TODO Fetch world
    public int world;

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
        selectedStageValue = Player1.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue: " + selectedStageValue);
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        print(GameObject.Find("ScoreText").GetComponent<Text>().text);
        playingGame = true;
        resultPass = false;
        winningCon = 3;
        disableAllStageImage();
        displaySelectedStageImage();
        disableResultScreen();
        addBtnResultScreenListener();
        setStageIndicator();
        checkWorld();
        changeStageColor(selectedStageValue);
        if ((selectedStageValue % 10) == 4)
            removeBtnNextStageListener();
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
        if (int.Parse(GameObject.Find("ScoreText").GetComponent<Text>().text) < winningCon)
        {
            btnNextStage.gameObject.SetActive(false);
            btnPlayAgain.gameObject.SetActive(true);
            lose.SetActive(true);
            win.SetActive(false);
        }
        else if (int.Parse(GameObject.Find("ScoreText").GetComponent<Text>().text) > winningCon)
        {
            resultPass = true;
            btnNextStage.gameObject.SetActive(true);
            btnPlayAgain.gameObject.SetActive(false);
            lose.SetActive(false);
            win.SetActive(true);
            updateUser();

            if ((selectedStageValue % 10) == 4)
                removeBtnNextStageListener();
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

    public void removeBtnNextStageListener()
    {
        btnNextStage.onClick.RemoveAllListeners();
        btnNextStage.gameObject.SetActive(false);
        btnPlayAgain.gameObject.SetActive(true);
    }

    public void addBtnResultScreenListener()
    {
        btnNextStage.onClick.AddListener(setNextStage);
        btnPlayAgain.onClick.AddListener(resetStage);
    }

    public void setNextStage()
    {
        Player1.userPlayer.incrementStageValue();
        selectedStageValue = Player1.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue" + selectedStageValue);
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        playingGame = true;
        // Call Functions or write code here to setup next stage here,
        // i retrieve the next stage le
        setStageIndicator();

        // Check if player current progress is the same as selected stage, increment attempts
        if (Player1.userPlayer.currProg == Player1.userPlayer.selectedStageValue)
            Assets.DatabaseInit.dbInit.incrementCurrentStageAttempt(Player1.userPlayer.currProg, Player1.userPlayer.email);

        curQn = 1;
        playerscore = 0;
        ScoreText.GetComponent<Text>().text = playerscore.ToString();
        count = 5;
        size = 5;

        if ((selectedStageValue % 10) == 4)
            removeBtnNextStageListener();

        disableAllStageImage();
        displaySelectedStageImage();
        disableResultScreen();
        checkWorld();
    }

    public void resetStage()
    {
        // Call Functions or write code here to reset stage here
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (1).ToString();
        playingGame = true;
        resultPass = false;

        // Check if player current progress is the same as selected stage, increment attempts
        if ((Player1.userPlayer.currProg == Player1.userPlayer.selectedStageValue) && (gamewin == 2))
            Assets.DatabaseInit.dbInit.incrementCurrentStageAttempt(Player1.userPlayer.currProg, Player1.userPlayer.email);

        curQn = 1;
        playerscore = 0;
        ScoreText.GetComponent<Text>().text = playerscore.ToString();
        count = 5;
        size = 5;
        gamewin = 3;

        disableResultScreen();
        checkWorld();
    }

    public void setStageIndicator()
    {
        GameObject.Find("CurrentWorldText").GetComponent<Text>().text = (selectedStageValue / 10).ToString();
        GameObject.Find("CurrentStageText").GetComponent<Text>().text = (selectedStageValue % 10).ToString();
    }

    public void checkWorld()
    {
        if (selectedStageValue < 20)
        {
            if (selectedStageValue == 11)
                copyQnA = QnA11;
            else if (selectedStageValue == 12)
                copyQnA = QnA12;
            else if (selectedStageValue == 13)
                copyQnA = QnA13;
            else
                copyQnA = QnA14;
        }
        else if (selectedStageValue < 30)
        {
            if (selectedStageValue == 21)
                copyQnA = QnA21;
            else if (selectedStageValue == 22)
                copyQnA = QnA22;
            else if (selectedStageValue == 23)
                copyQnA = QnA23;
            else
                copyQnA = QnA24;
        }
        else if (selectedStageValue < 40)
        {
            if (selectedStageValue == 31)
                copyQnA = QnA31;
            else if (selectedStageValue == 32)
                copyQnA = QnA32;
            else if (selectedStageValue == 33)
                copyQnA = QnA33;
            else
                copyQnA = QnA34;
        }
        else
        {
            if (selectedStageValue == 41)
                copyQnA = QnA41;
            else if (selectedStageValue == 42)
                copyQnA = QnA42;
            else if (selectedStageValue == 43)
                copyQnA = QnA43;
            else
                copyQnA = QnA44;
        }

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

    void generateQuestion()
    {
        if (gamewin == 1)
        {
            size--;
        }
        else if (gamewin == 2)
            size--;


        for (int index = 0; index < size; index++)
        {

            Debug.Log("current index " + index);
            currentQuestion = tempQnA[index];
            fetchQn();

            setText();
            setBtnAns();
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

        /*int ans;*/
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
            playerscore++;
            ScoreText.GetComponent<Text>().text = playerscore.ToString();
            Debug.Log("Player score: " + playerscore);
            count--;
            if (count > 0)
                generateQuestion();
        }
        else
        {
            gamewin = 2; //means lost

            Debug.Log("gamewin = " + gamewin);
            Debug.Log("u clicked on the wrong answer : " + noFromButton);

            Debug.Log("Player score: " + playerscore);
            count--;
            if (count > 0)
                generateQuestion();
        }

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

    /*public void resetStage()
    {
        curQn = 1;
        playerscore = 0;
        ScoreText.GetComponent<Text>().text = playerscore.ToString();
        count = 5;
        size = 5;

        selectedStageValue = Player.userPlayer.selectedStageValue;
        checkWorld();
    }*/
}
