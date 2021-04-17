using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour, IMainMenuManager
{
    public int accountType;
    /*public Button btnViewStats;
    public Button btnSummaryReport;
    public Button btnEditQuestions;*/
    public GameObject btnMainMenuTeacher;
    public GameObject btnAssignment;


    // Start is called before the first frame update
    void Start()
    {
        /*accountType = Player.userPlayer.accountType;
        Debug.Log(Player.userPlayer.accountType);
        Debug.Log(Player.userPlayer.currProg);
        Debug.Log(Player.userPlayer.name);
        checkAccountType(accountType);*/
        //Debug.Log(UserPlayer.userPlayer.accountType);
        //Debug.Log(UserPlayer.userPlayer.currProg);
        checkAccountType(UserPlayer.userPlayer.accountType);
        checkAssignment();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool checkAccountType(int accType)
    {
        if (accType == 0)
        {
            /*btnViewStats.gameObject.SetActive(false);
            btnSummaryReport.gameObject.SetActive(false);
            btnEditQuestions.gameObject.SetActive(false);*/

            btnMainMenuTeacher.SetActive(false);
            return false;

        }
        else if (accType == 1)
        {
            /*btnViewStats.gameObject.SetActive(true);
            btnSummaryReport.gameObject.SetActive(true);
            btnEditQuestions.gameObject.SetActive(true);*/

            btnMainMenuTeacher.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checkAssignment()
    {
        Assets.Models.User usr = Assets.DatabaseInit.dbInit.retrieveUser(UserPlayer.userPlayer.email);
        List<string> tmp = usr.assignmentAssigned;
        if (tmp?.Count > 0)
        {
            btnAssignment.SetActive(true);
        }
        else
        {
            btnAssignment.SetActive(false);
        }
    }
}
