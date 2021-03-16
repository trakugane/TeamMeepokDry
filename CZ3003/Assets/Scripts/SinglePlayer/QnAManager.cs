using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class QnAManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject QnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;
    public GameObject ScoreText;
    int playerscore = 0;
    
    //addition
    public string[] QnA11 = new string[] { "6+1=", "4+5=", "3+5=" , "0+0=" , "2+3="};
    public string[] QnA12 = new string[] { "6+7=", "4+9=", "5+8=", "9+9=", "7+9=" };
    public string[] QnA13 = new string[] { "13+1=", "5+16=", "23+54=", "70+1=", "7+17=" };
    public string[] QnA14 = new string[] { "8+12=", "55+33=", "77+4=", "29+14=", "0+32=" };
    public string[] QnA15 = new string[] { "18+12=", "5+14=", "7+54=", "13+32=", "34+45=" };

    //subtraction
    public string[] QnA21 = new string[] { "4-3=", "8-5=", "10-2=", "1-0=", "9-4=" };
    public string[] QnA22 = new string[] { "18-8=", "14-11=", "25-8=", "17-8=", "24-7=" };
    public string[] QnA23 = new string[] { "35-16=", "45-29=", "16-4=", "88-32=", "60-32=" };
    public string[] QnA24 = new string[] { "18-12=", "55-43=", "37-23=", "85-39=", "92-42=" };
    public string[] QnA25 = new string[] { "46-32=", "14-5=", "75-2=", "61-39=", "9-5=" };

    //multiplication
    public string[] QnA31 = new string[] { "4x7=", "8x6=", "10x2=", "5x0=", "9x8=" };
    public string[] QnA32 = new string[] { "18x2=", "11x4=", "2x14=", "15x5=", "7x9=" };
    public string[] QnA33 = new string[] { "3x8=", "4x14=", "17x3=", "10x12=", "16x0=" };
    public string[] QnA34 = new string[] { "1x80=", "5x7=", "3x12=", "12x12=", "8x9=" };
    public string[] QnA35 = new string[] { "4x12=", "14x3=", "2x7=", "6x9=", "9x5=" };

    //division
    public string[] QnA41 = new string[] { "5/1=", "6/3=", "10/2=", "14/7=", "6/2=" };
    public string[] QnA42 = new string[] { "18/2=", "16/2=", "16/4=", "15/5=", "90/10=" };
    public string[] QnA43 = new string[] { "8/8=", "32/8=", "72/3=", "66/11=", "40/5=" };
    public string[] QnA44 = new string[] { "55/5=", "36/6=", "32/4=", "96/12=", "88/8=" };
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

    //TODO Fetch world
    public int world = Player.userPlayer.selectedStageValue;

    void Start()
    {
        checkWorld();
    }

    public void checkWorld()
    {
        if (world < 20)
        {
            if (world == 11)
                tempQnA = QnA11;
            else if (world == 12)
                tempQnA = QnA12;
            else if (world == 13)
                tempQnA = QnA13;
            else if (world == 14)
                tempQnA = QnA14;
            else
                tempQnA = QnA15;

        }
        else if (world<30)
        {
            if (world == 21)
                tempQnA = QnA21;
            else if (world == 22)
                tempQnA = QnA22;
            else if (world == 23)
                tempQnA = QnA23;
            else if (world == 24)
                tempQnA = QnA24;
            else
                tempQnA = QnA25;
        }
        else if (world < 40)
        {
            if (world == 31)
                tempQnA = QnA31;
            else if (world == 32)
                tempQnA = QnA32;
            else if (world == 33)
                tempQnA = QnA33;
            else if (world == 34)
                tempQnA = QnA34;
            else
                tempQnA = QnA35;
        }
        else
        {
            if (world == 41)
                tempQnA = QnA41;
            else if (world == 42)
                tempQnA = QnA42;
            else if (world == 43)
                tempQnA = QnA43;
            else if (world == 44)
                tempQnA = QnA44;
            else
                tempQnA = QnA45;
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


        for (index = 0; index < size; index++)
        {

            Debug.Log("current index " + index);
            currentQuestion = tempQnA[index];
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
        int count = 5;

        if (noFromButton == ans)
        {
            gamewin = 1; //means win
            Debug.Log("gamewin = " + gamewin);
            Debug.Log("u clicked on the correct answer :" + ans);
            //GameController.gameController.points4game++;        //increment points
            playerscore++;
            ScoreText.GetComponent<Text>().text = playerscore.ToString();
            Debug.Log("Player score: " +playerscore);
            //Debug.Log(GameController.gameController.points4game);
            //Destroy(GameController.gameController.questionPanel);
            count--;
            if (count >0)
                generateQuestion();
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
                generateQuestion();
            //else
                //Go to score page
        }
    }



    
}
