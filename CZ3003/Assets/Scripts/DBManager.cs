using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class DBManager : MonoBehaviour
{

    public static DBManager dbm;

    public InputField Username;
    public InputField Password;
    /*AccountManager am;*/

    private void Awake()
    {
        if (DBManager.dbm == null)
        {
            DBManager.dbm = this;

        }
        DontDestroyOnLoad(DBManager.dbm);
    }


    // Start is called before the first frame update
    void Start()
    {
        /*am = gameObject.AddComponent<AccountManager>();*/
        // AccountManager.accMgr.validateUser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkLogin(string sceneName)
    {
        Debug.Log("Login Button Pressed!");
        // Verify if username in database and password is correct
        if (validateUser(Username, Password) == true)
        {
            SceneManager.LoadScene(sceneName);
            /*if (retrieveData(Username.text.ToString(), Password.text.ToString()) == true)
            {
                *//*Debug.Log(PlayerPrefs.GetString("name"));*//*

                SceneManager.LoadScene(sceneName);
            } 
            else
            {
                // Display Error (Account data not retrievable)
            }*/
        }
        else
        {
            // Display Error (Account not valid)
        }
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
