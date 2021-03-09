using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AccountManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool validateUser(InputField user, InputField pwd)
    {
        // If username in database and password is correct, return true
        // Else, Return false
        Debug.Log("Validated");

        return true;
    }

    public bool retrieveData(string user, string password)
    {
        // Declare user information variable to store temporary
        string name = "Paul";
        int currProg = 51;
        string accountType = "T";

        // Retrieve user information from database

        // Set user information into PlayerPref
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.SetInt("currProg", currProg);
        PlayerPrefs.SetString("accountType", accountType);

        return true;
    }
}
