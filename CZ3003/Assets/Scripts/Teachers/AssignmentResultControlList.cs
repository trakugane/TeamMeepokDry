using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentResultControlList : MonoBehaviour
{
    [SerializeField]
    private List<AssignmentRecord> assignmentRecordList;
    [SerializeField]
    private GameObject textTemplate;
    [SerializeField]
    private List<GameObject> buttons;
    
    
    [SerializeField]
    private float averageScore;



    void Start()
    {
        string buttonID = AssignmentIDController.assignmentID;
        Debug.Log(buttonID);
        
        this.Invoke(() => GenerateAssignmentReport(buttonID), 0.01f);
        
    }

    public void GenerateAssignmentReport(string buttonID)
    {
        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        Debug.Log(buttonID);
       
        Assets.Models.Assignment assignmentID = dbInit.getAssignemt(buttonID);

        assignmentRecordList = assignmentID.result;
        int numberOfStudents = 0;
        int totalScore = 0;
        
        

        assignmentRecordList.ForEach(delegate (AssignmentRecord student)
        {
            GameObject button = Instantiate(textTemplate) as GameObject;
            button.SetActive(true);

            
            int score = student.score;
            string email = student.userEmail;
            totalScore += score;
            numberOfStudents ++;
            Debug.Log("score:"+score+"email:"+email+"totalScore:"+score+"count:"+numberOfStudents);
            
            string userName = email.Substring(0, email.IndexOf('@'));
            button.GetComponent<AssignmentListButton>().SetText("Username: "+ userName + "   Score: " + score);
            button.transform.SetParent(textTemplate.transform.parent, false);
            buttons.Add(button);

        });

        
        averageScore = (float)totalScore/(float)numberOfStudents;
        Debug.Log(averageScore);
        AssignmentIDController.averageScore = averageScore;
    }

    
    

}

public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}