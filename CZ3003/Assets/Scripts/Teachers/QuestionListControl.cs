

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private int[] intArray;
    private List<Assets.Models.Question> questionList = new List<Assets.Models.Question>();
    private List<GameObject> buttons;


    void Start()
    {

        Invoke("GenerateList", 2f);
        
        
    } 



    public async void GenerateList() 
    {
        buttons = new List<GameObject>();
        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        var task = dbInit.retrieveAllQuestions();
        var questionList = await task;
        

        
        foreach (Assets.Models.Question question in questionList)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            //SetText should replace with question
            button.GetComponent<QuestionListButton>().SetText("Button " + question.questionTitle);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button.gameObject);

        }
    }



    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}