using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBank : MonoBehaviour
{
    public Text qnText;
    public Button ans1;
    public Button ans2;
    public Button ans3;
    public Button ans4;

    // a x b = ans
    int a, b;
    int ans;
    string qnOperator;
    List<Button> btnList;

    private void Start()
    {
        
        var operatorList = new List<string> { "+", "-", "*" };  //, "/" remove divide 
        int index = UnityEngine.Random.Range(0,operatorList.Count);
        qnOperator = operatorList[index];   // should be + - * /

        btnList = new List<Button> { ans1, ans2, ans3, ans4 };
        a = UnityEngine.Random.Range(1, 25);
        b = UnityEngine.Random.Range(1, 25);

        setAns(qnOperator, a , b);
        setText(qnOperator, a, b);
        setBtnAns();
    }

    private void setAns(string oper, int a, int b)
    {
        if (oper.Equals("+"))
        {
            ans = a + b;

        }
        if (oper.Equals("-"))
        {
            ans = a - b;
        }
        if (oper.Equals("*"))
        {
            ans = a * b;
        }

        /*
        if (oper.Equals("/"))
        {
            ans = a / b;
            
        } */
    }
    private void setText(string oper, int a, int b)
    {
        string msg = "Question: " + a + " " + oper + " " + b + " = ?";
        qnText.text = msg;
    }


   
    
    public void Shuffle<T>(IList<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    private void setBtnAns()
    {
        Shuffle<Button>(btnList);
        // 4 ans , 1 correct ans  and 3 wrong ans
        int wrongans1 = a + b + UnityEngine.Random.Range(1,7);
        int wrongans2 = a * (b + 1) + a*UnityEngine.Random.Range(1,3) ;
        int wrongans3 = a - b - UnityEngine.Random.Range(1, 7);

        btnList[0].GetComponentInChildren<Text>().text = ans.ToString();
        btnList[1].GetComponentInChildren<Text>().text = wrongans1.ToString();
        btnList[2].GetComponentInChildren<Text>().text = wrongans2.ToString();
        btnList[3].GetComponentInChildren<Text>().text = wrongans3.ToString();

    }

    public void onButtonClicked(Button btn)
    {
        // 10/3
        Text btnText = btn.GetComponentInChildren<Text>();
        int noFromButton = Int32.Parse(btnText.text);
        Debug.Log("number from button is " + noFromButton);
        //int Answer = 9; // change to string manipulation of text
        if (noFromButton == ans)
        {
            //gameWin = true;
            Debug.Log("u clicked on the correct answer :" + ans);
            GameController.gameController.pointsFromGame++;       //increment points
            //updating Local
            String updatedUserPoints = GameController.gameController.pointsFromGame.ToString();
            GameController.gameController.userPoints.text = updatedUserPoints; // jus addedd
            //updating Other
            GameController.gameController.RPC_UpdateOnOtherClient();

            
            Destroy(GameController.gameController.questionPanel);
        }
        else
        {
            Debug.Log("u clicked on the wrong answer : " + noFromButton);
        }
    }


}
