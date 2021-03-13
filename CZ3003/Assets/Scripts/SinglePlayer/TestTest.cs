using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TestTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject QnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;

    public string qn11 = "2+3=5";//question followed by world i.e 1-1

    public string temp = "2+3=5"; // + selectedstagevalue;

    public int ans;

    public char operandchar;
    public int equalindex;
    public int operatorindex;
    public int a;
    public int b;

    void Start()
    {

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == '=')
            {
                equalindex = i;
            }
            else if ((temp[i] == '+') || (temp[i] == '-') || (temp[i] == 'x') || (temp[i] == '/'))
            {
                operandchar = temp[i];
                operatorindex = i;
            }
        }
        string str1 = "";
        string str2 = "";

        for (int i = 0; i < temp.Length; i++)
        {
            if (i < operatorindex)
            {

                str1 = str1 + temp[i];

            }
            if (i < equalindex && i > operatorindex)
            {
                str2 = str2 + temp[i];
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

    public void Shuffle(string strans, string strrandomA, string strrandomB, string strrandomC)
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
            Debug.Log(randomIndex);
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

        if (noFromButton == ans)
        {
            //gameWin = true;
            Debug.Log("u clicked on the correct answer :" + ans);
            /*GameController.gameController.points4game++;        //increment points
            Debug.Log(GameController.gameController.points4game);
            Destroy(GameController.gameController.questionPanel);*/
        }
        else
        {
            Debug.Log("u clicked on the wrong answer : " + noFromButton);
        }
    }

}
