

using Assets.Models;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AssignmentListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private List<GameObject> buttons;
    [SerializeField]
    static List<Assignment> assignmentList = new List<Assignment>();

    [SerializeField]
    static List<string> buttonIDList = new List<string>();

    

    void Start()
    {


        Invoke(nameof(GenerateAssignmentList), 0.01f);




    }



    /*public async void GenerateList() 
    {
        
        buttons = new List<GameObject>();
        var task = dbInit.retrieveAllQuestions();
        var questionList = await task;
        foreach (Assets.Models.Question question in questionList)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);
            //SetText should replace with question
            button.GetComponent<QuestionListButton>().SetText(question.questionTitle);
            button.name = question.id.ToString();
            Debug.Log(button.name);
          
            
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button.gameObject);

        }
    }*/
    public void GenerateAssignmentList()
    {


        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();


        assignmentList = dbInit.getAllAssignmentStaff();
        foreach (Assets.Models.Assignment assignment in assignmentList)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<Button>().interactable = true;
            //SetText should replace with question

            button.GetComponent<AssignmentListButton>().SetText(assignment.assignmentName);

            button.name = assignment.id.ToString();
            string buttonName = button.name;
            buttonIDList.Add(buttonName);


            button.transform.SetParent(buttonTemplate.transform.parent, false);


            buttons.Add(button);


        }


        foreach (string i in buttonIDList)
        {
            Debug.Log(i);
        }
    }

    

    /*public void AssignButtonName(string buttonID)
    {
        
        for(int i=0; i < questionList.Count; i++)
        {
            string buttonID = questionList[i].id.ToString();
            buttonIDList.Add(buttonID);
        }
    }
    */
    public void ButtonClicked(string myTextView)
    {

        Debug.Log(myTextView);

    }

}