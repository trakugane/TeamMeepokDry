

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



    public void GenerateList()
    {
        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        buttons = new List<GameObject>();
        List<Assets.Models.Question> questionList = new List<Assets.Models.Question>();
        questionList = dbInit.retrieveAllQuestion();
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
    }



    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }
}