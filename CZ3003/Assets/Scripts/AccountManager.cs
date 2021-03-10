using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AccountManager : MonoBehaviour
{

    public static AccountManager am;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (AccountManager.am == null)
        {
            AccountManager.am = this;

        }
        /*DontDestroyOnLoad(AccountManager.am);*/
    }


    public bool validateUser(InputField email, InputField password)
    {
        // If username in database and password is correct, return true
        /*if (db.checkEmailExist(email.text, password.text) == true)
            // Set up here
            return false*/
        // Else, Return false
        Debug.Log("Validated");

        return true;
    }

    /*public bool retrieveData(string user, string password)
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
    }*/

    /*public int validateAccountCreation(InputField email, InputField password, InputField confirmPassword, InputField personalName)
    {
        // Check Email with database
        // Check Password & Confirm Password is the same
        if (password.text.ToString() != confirmPassword.text.ToString())
            return 2;

        // Store all details into database
        int currProg = 11;
        // Check if email is student or teacher email
        string accountType = "S";
        // int tutorial = 0; check if havent done tutorial?

        // If store unsuccessful, return 3


        Debug.Log("Error: Account Creation");
        return 0;
    }*/
}
