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
    public float timerCurrent = 0;
    public bool playingGame;


    public GameObject QnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;
    public GameObject ScoreText;
    int playerscore = 0;

    //Boss Levels
    public string[] QnA15 = new string[] { "18+12=", "5+14=", "7+54=", "13+32=", "34+45=" };
    public string[] QnA25 = new string[] { "46-32=", "14-5=", "75-2=", "61-39=", "9-5=" };
    public string[] QnA35 = new string[] { "4x12=", "14x3=", "2x7=", "6x9=", "9x5=" };
    public string[] QnA45 = new string[] { "95/5=", "36/2=", "45/5=", "56/7=", "9/3=" };
    public string[] tempQnA;


    public int index;
    public string currentQuestion; // + selectedstagevalue;

    public int ans;

    public char operandchar;
    public int equalindex;
    public int operatorindex;
    public int a;
    public int b;
    public int gamewin = 3; //default, havent play before
    public int size = 5;

    public bool stillcontinuing = false;


    // Start is called before the first frame update
    void Start()
    {
        selectedStageValue = Player.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue" + selectedStageValue);
        
        playingGame = true;
        timerCurrent = 6;
        setTimer(timerCurrent);
        checkWorld();
    }
    
    public void checkWorld()
    {
        if (selectedStageValue == 15)
            tempQnA = QnA15;
        else if (selectedStageValue == 25)
            tempQnA = QnA25;
        else if (selectedStageValue == 35)
            tempQnA = QnA35;
        else
            tempQnA = QnA45;

        Debug.Log(tempQnA);
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

        for (index = 0; index < size; index++)
        {
            Random random = new Random();
            int value = Random.Range(0, tempQnA.Length);

            Debug.Log("current index " + value);

            currentQuestion = tempQnA[value];
            Debug.Log(currentQuestion);
            fetchQn();

            //tempQnAList.Remove(value);
            //tempQnA = list.ToArray();

            Debug.Log("current array " + tempQnA);
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
            int randomIndex =Random.Range(0, 4);
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
        int count = 5;


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
        }
    }
}
