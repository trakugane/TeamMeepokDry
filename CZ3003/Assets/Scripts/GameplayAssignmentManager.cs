using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameplayAssignmentManager : MonoBehaviour
{
    public bool playingGame;

    public List<string> assignmentQns;
    public List<string> assignmentQnsShuffled;
    public string assignmentTitle;
    public string currentQuestion;
    int qnSize = 5;

    public int a;
    public int b;
    public int ans;
    public char operandchar;
    public int equalindex;
    public int operatorindex;

    public GameObject currentQn;
    public GameObject QnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;

    public int gamewin = 3; //default, havent play before
    int curQn = 1;
    public int totalNoOfQn = 5;
    public int count = 5;
    public GameObject ScoreText;
    int playerscore = 0;

    public GameObject resultPanel;
    public GameObject win;
    public GameObject lose;
    
    public Button btnBack;
    public GameObject backPanel;
    public Button btnReturnToGame;
    public Button btnExitGamePremature;
    public GameObject assignmentTitleText;

    // Start is called before the first frame update
    void Start()
    {
        retrieveAssignment();
        addBtnBackScreenListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retrieveAssignment()
    {
        retrieveAssignmentQns();
    }

    public void retrieveAssignmentQns()
    {
        Assets.Models.User usr = Assets.DatabaseInit.dbInit.retrieveUser(UserPlayer.userPlayer.email);
        List<string> tmp = usr.assignmentAssigned;
        List<Assets.Models.Question> assignmentQuestion = Assets.DatabaseInit.dbInit.getQuestionsByType(tmp[0]);
        assignmentTitle = tmp[0];
        foreach (Assets.Models.Question qn in assignmentQuestion)
        {
            assignmentQns.Add(qn.questionTitle);
        }
        assignmentTitleText.GetComponent<Text>().text = assignmentTitle;

        shuffleAssignmentQns();
    }

    public void shuffleAssignmentQns()
    {
        
        for (int i = 0; i < qnSize; i++)
        {
            int rand = Random.Range(0, assignmentQns.Count);
            assignmentQnsShuffled.Add(assignmentQns[rand]);
            assignmentQns.RemoveAt(rand);
        }
        generateQuestion();
    }

    public void generateQuestion()
    {
        if (gamewin == 1)
        {
            qnSize--;
        }
        else if (gamewin == 2)
            qnSize--;

        for (int index = 0; index < qnSize; index++)
        {
            currentQuestion = assignmentQnsShuffled[index];
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
        int random1 = UnityEngine.Random.Range(0, ans + 10);
        int random2 = UnityEngine.Random.Range(0, random1 + 15);
        int random3 = UnityEngine.Random.Range(0, random2 + 20);

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
        count = 5;

        curQn++;
        GameObject.Find("CurrentQuestionNoText").GetComponent<Text>().text = (curQn).ToString();

        if (noFromButton == ans)
        {
            gamewin = 1; //means win
            playerscore++;
            ScoreText.GetComponent<Text>().text = playerscore.ToString();
            count--;
            if (count > 0)
            {
                generateQuestion();
            }
            //else
            //Go to score page

        }
        else
        {
            gamewin = 2; //means lost

            count--;
            if (count > 0)
            {
                generateQuestion();
            }
            //else
            //Go to score page
        }

        checkGameplay();

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
            lose.SetActive(true);
            win.SetActive(false);
        }
        else if (int.Parse(GameObject.Find("CurrentScoreText").GetComponent<Text>().text) > (totalNoOfQn / 2))
        {
            lose.SetActive(false);
            win.SetActive(true);
        }
        updateUser();
    }

    void updateUser()
    {
        print("Updated User");
        print(UserPlayer.userPlayer.email);
        print(int.Parse(GameObject.Find("CurrentScoreText").GetComponent<Text>().text));
    }

    public void addBtnBackScreenListener()
    {
        btnBack.onClick.AddListener(enableBackScreen);
        btnReturnToGame.onClick.AddListener(disableBackScreen);
        btnExitGamePremature.onClick.AddListener(updateUser);
    }

    void disableBackScreen()
    {
        backPanel.SetActive(false);
    }
    void enableBackScreen()
    {
        backPanel.SetActive(true);
    }
}
