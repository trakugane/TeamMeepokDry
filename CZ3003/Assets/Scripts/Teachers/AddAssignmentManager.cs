using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class AddAssignmentManager : MonoBehaviour
{
    public GameObject msgText;
    public GameObject panelResult;
    public InputField assignmentName;
    public InputField firstDigitQn1, firstDigitQn2, firstDigitQn3, firstDigitQn4, firstDigitQn5;
    public Dropdown operatorQn1, operatorQn2, operatorQn3, operatorQn4, operatorQn5;
    public InputField secondDigitQn1, secondDigitQn2, secondDigitQn3, secondDigitQn4, secondDigitQn5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendAssignmentToAllStudents()
    {
        bool validQn = validateQuestions();
        bool emptyQn = validateEmptyText();
        if (emptyQn == false)
        {
            msgText.SetActive(true);
            msgText.GetComponent<Text>().text = "Please fill in all the blanks!!";
        }
        else if (validQn == false)
        {
            msgText.SetActive(true);
            msgText.GetComponent<Text>().text = "Answers should be positive integer!";
        }
        else
        {
            addQuestionsIntoDatabase();
            Assets.Models.Assignment assignment = new Assets.Models.Assignment();
            assignment.assignmentName = assignmentName.text;
            assignment.creatorEmail = UserPlayer.userPlayer.email;
            Assets.DatabaseInit.dbInit.createAssignment(assignment);
            Assets.DatabaseInit.dbInit.assignAllStudentAssgn(assignmentName.text);
            panelResult.SetActive(true);
        }

    }

    public bool validateEmptyText()
    {

        Text[] textList = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in textList)
        {
            if (t.text == "")
            {
                Debug.Log("Empty text somewhere");
                return false;
            }
        }

        return true;
    }

    public bool validateQuestions()
    {
        if (operatorQn1.options[operatorQn1.value].text.Equals("-"))
        {
            if ((Int32.Parse(firstDigitQn1.text) - Int32.Parse(secondDigitQn1.text)) < 0)
                return false;
        }
        if (operatorQn2.options[operatorQn2.value].text.Equals("-"))
        {
            if ((Int32.Parse(firstDigitQn2.text) - Int32.Parse(secondDigitQn2.text)) < 0)
                return false;
        }
        if (operatorQn3.options[operatorQn3.value].text.Equals("-"))
        {
            if ((Int32.Parse(firstDigitQn3.text) - Int32.Parse(secondDigitQn3.text)) < 0)
                return false;
        }
        if (operatorQn4.options[operatorQn4.value].text.Equals("-"))
        {
            if ((Int32.Parse(firstDigitQn4.text) - Int32.Parse(secondDigitQn4.text)) < 0)
                return false;
        }
        if (operatorQn5.options[operatorQn5.value].text.Equals("-"))
        {
            if ((Int32.Parse(firstDigitQn5.text) - Int32.Parse(secondDigitQn5.text)) < 0)
                return false;
        }

        if (operatorQn1.options[operatorQn1.value].text.Equals("/"))
        {
            if (Int32.Parse(secondDigitQn1.text) == 0)
                return false;
            if ((Int32.Parse(firstDigitQn1.text) % Int32.Parse(secondDigitQn1.text)) != 0)
                return false;
        }
        if (operatorQn2.options[operatorQn2.value].text.Equals("/"))
        {
            if (Int32.Parse(secondDigitQn2.text) == 0)
                return false;
            if ((Int32.Parse(firstDigitQn2.text) % Int32.Parse(secondDigitQn2.text)) != 0)
                return false;
        }
        if (operatorQn3.options[operatorQn3.value].text.Equals("/"))
        {
            if (Int32.Parse(secondDigitQn3.text) == 0)
                return false;
            if ((Int32.Parse(firstDigitQn3.text) % Int32.Parse(secondDigitQn3.text)) != 0)
                return false;
        }
        if (operatorQn4.options[operatorQn4.value].text.Equals("/"))
        {
            if (Int32.Parse(secondDigitQn4.text) == 0)
                return false;
            if ((Int32.Parse(firstDigitQn4.text) % Int32.Parse(secondDigitQn4.text)) != 0)
                return false;
        }
        if (operatorQn5.options[operatorQn5.value].text.Equals("/"))
        {
            if (Int32.Parse(secondDigitQn5.text) == 0)
                return false;
            if ((Int32.Parse(firstDigitQn5.text) % Int32.Parse(secondDigitQn5.text)) != 0)
                return false;
        }

        return true;
    }

    public double returnAns(double firstDigit, double secondDigit, char op)
    {
        double answer = 0;

        if (op == '+')
            answer = firstDigit + secondDigit;
        else if (op == '-')
            answer = firstDigit - secondDigit;
        else if (op == 'x')
            answer = firstDigit * secondDigit;
        else if (op == '/')
            answer = firstDigit * secondDigit;

        return answer;
    }

    public void addQuestionsIntoDatabase()
    {
        /*List<Assets.Models.Question> assignmentQns = new List<Assets.Models.Question>();
        assignmentQns.Add();*/
        Assets.Models.Question assignmentQn1 = new Assets.Models.Question();
        assignmentQn1.questionTitle = firstDigitQn1.text + operatorQn1.options[operatorQn1.value].text + secondDigitQn1.text + "=";
        assignmentQn1.questionType = assignmentName.text;
        assignmentQn1.creatorEmail = UserPlayer.userPlayer.email;
        Assets.DatabaseInit.dbInit.addQuestion(assignmentQn1);
        Assets.Models.Question assignmentQn2 = new Assets.Models.Question();
        assignmentQn2.questionTitle = firstDigitQn2.text + operatorQn2.options[operatorQn2.value].text + secondDigitQn2.text + "=";
        assignmentQn2.questionType = assignmentName.text;
        assignmentQn2.creatorEmail = UserPlayer.userPlayer.email;
        Assets.DatabaseInit.dbInit.addQuestion(assignmentQn2);
        Assets.Models.Question assignmentQn3 = new Assets.Models.Question();
        assignmentQn3.questionTitle = firstDigitQn3.text + operatorQn3.options[operatorQn3.value].text + secondDigitQn3.text + "=";
        assignmentQn3.questionType = assignmentName.text;
        assignmentQn3.creatorEmail = UserPlayer.userPlayer.email;
        Assets.DatabaseInit.dbInit.addQuestion(assignmentQn3);
        Assets.Models.Question assignmentQn4 = new Assets.Models.Question();
        assignmentQn4.questionTitle = firstDigitQn4.text + operatorQn4.options[operatorQn4.value].text + secondDigitQn4.text + "=";
        assignmentQn4.questionType = assignmentName.text;
        assignmentQn4.creatorEmail = UserPlayer.userPlayer.email;
        Assets.DatabaseInit.dbInit.addQuestion(assignmentQn4);
        Assets.Models.Question assignmentQn5 = new Assets.Models.Question();
        assignmentQn5.questionTitle = firstDigitQn5.text + operatorQn5.options[operatorQn5.value].text + secondDigitQn5.text + "=";
        assignmentQn5.questionType = assignmentName.text;
        assignmentQn5.creatorEmail = UserPlayer.userPlayer.email;
        Assets.DatabaseInit.dbInit.addQuestion(assignmentQn5);
    }
}
