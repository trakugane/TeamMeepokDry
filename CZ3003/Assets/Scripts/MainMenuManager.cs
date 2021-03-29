using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour
{
    public int accountType;
    public Button btnViewStats;
    public Button btnSummaryReport;
    public Button btnEditQuestions;

    // Start is called before the first frame update
    void Start()
    {
        /*accountType = Player.userPlayer.accountType;
        Debug.Log(Player.userPlayer.accountType);
        Debug.Log(Player.userPlayer.currProg);
        Debug.Log(Player.userPlayer.name);
        checkAccountType(accountType);*/
        Debug.Log(UserPlayer.userPlayer.accountType);
        Debug.Log(UserPlayer.userPlayer.currProg);
        checkAccountType(UserPlayer.userPlayer.accountType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkAccountType (int accType)
    {
        if (accType == 0)
        {
            btnViewStats.gameObject.SetActive(false);
            btnSummaryReport.gameObject.SetActive(false);
            btnEditQuestions.gameObject.SetActive(false);
            
        }
        else if (accType == 1)
        {
            btnViewStats.gameObject.SetActive(true);
            btnSummaryReport.gameObject.SetActive(true);
            btnEditQuestions.gameObject.SetActive(true);
        }
    }
}
