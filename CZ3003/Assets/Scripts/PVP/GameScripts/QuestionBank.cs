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

    private void Start()
    {

        a = UnityEngine.Random.Range(0, 10);
        b = UnityEngine.Random.Range(0, 10);
        ans = a * b;
        setText();
        setBtnAns();
    }

    private void setText()
    {
        string msg = "Question : " + a + " X " + b + " ?";
        qnText.text = msg;
    }

    private void setBtnAns()
    {
        ans1.GetComponentInChildren<Text>().text = ans.ToString();
        ans2.GetComponentInChildren<Text>().text = 123.ToString();
        ans3.GetComponentInChildren<Text>().text = 456.ToString();
        ans4.GetComponentInChildren<Text>().text = 789.ToString();
    }

    public void onButtonClicked(Button btn)
    {
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
