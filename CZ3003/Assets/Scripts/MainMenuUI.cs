using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour
{
    public string accountType;
    public Button btnViewStats;
    public Button btnSummaryReport;
    public Button btnEditQuestions;

    // Start is called before the first frame update
    void Start()
    {
        accountType = PlayerPrefs.GetString("accountType");
        Debug.Log(accountType);
        checkAccountType(accountType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkAccountType (string accType)
    {
        if (accType == "T")
        {
            btnViewStats.gameObject.SetActive(true);
            btnSummaryReport.gameObject.SetActive(true);
            btnEditQuestions.gameObject.SetActive(true);
        }
        else if (accType == "S")
        {
            btnViewStats.gameObject.SetActive(false);
            btnSummaryReport.gameObject.SetActive(false);
            btnEditQuestions.gameObject.SetActive(false);
        }
    }
}
