using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadAssignmentButton : MonoBehaviour
{

    AssignmentListControl assignmentControl;
    


    /*public string GetButtonID()
        {
            string buttonID = EventSystem.current.currentSelectedGameObject.name;
            return buttonID;
        }*/


    public void ButtonClicked()
    {
        string buttonID = gameObject.transform.parent.name;

        Debug.Log(buttonID);

        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        Boolean questionDeleted;
        // string email = UserPlayer.userPlayer.email;
        //Debug.Log(email);
        questionDeleted = dbInit.deleteAssignmentStudent("t@gmail.com", buttonID);

        Debug.Log(questionDeleted);

        if (questionDeleted.Equals(true))
        {
            //gameObject.SetActive(false);
            Destroy(GameObject.Find(buttonID));
        }



    }

    // TODO: button click but opens up assignmentrecord instead and displays the result instead - each student score and email

    public void OpenAssignmentRecord()
    {
        string buttonID = gameObject.transform.parent.name;

        Debug.Log(buttonID);

       
        AssignmentIDController.assignmentID = buttonID;
        Debug.Log(AssignmentIDController.assignmentID);
        SceneManager.LoadScene("AssignmentResults");
        
        

    }
}



