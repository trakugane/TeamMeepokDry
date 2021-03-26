using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Assets.Models;

public class DBManager : MonoBehaviour
{

    public static DBManager dbm;

    public string Username;
    public string Password;
    /*AccountManager am;*/

    public string email;
    public string password;
    public string personalName;
    Assets.DatabaseInit dbInit;


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
        dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        /*am = gameObject.AddComponent<AccountManager>();*/
        // AccountManager.accMgr.validateUser();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkLogin(string sceneName)
    {
        // Verify if username in database and password is correct
        

        /*if (validateUser(Username, Password) == true)
        {
            SceneManager.LoadScene(sceneName);
            *//*if (retrieveData(Username.text.ToString(), Password.text.ToString()) == true)
            {
                *//*Debug.Log(PlayerPrefs.GetString("name"));*//*

                SceneManager.LoadScene(sceneName);
            } 
            else
            {
                // Display Error (Account data not retrievable)
            }*//*
        }
        else
        {
            // Display Error (Account not valid)
        }*/
    }

    public bool checkRegister(string sceneName)
    {
        Debug.Log("Register Button Pressed!");
        bool codeMsg = validateAccountCreation(email, password, personalName);
        if (codeMsg)
            return true;
        else
        {
            // Need display error message to UI (10/3/21)
            return false;
        }
  

    }


    public object validateUser(string email, string password)
    {

        //Call DatabaseInit verifyAccount method which returns boolean
        //if Verify Account true then call DatabaseInit retireveUser method which returns Task<User> object
        bool vAccount = dbInit.verifyAccount(email, password);

       if (vAccount)
        {
            return dbInit.retrieveUser(email);
        }
       else
        {
            return null;
        }
        


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

    public bool validateAccountCreation(string email, string password, string personalName)
    {
        // Call createUser(User usr) method in DatabaseInit which will return boolean
        //Method with the following parameters: Password (String) , SinglePlayer spProgress (Object) , accountType (int), name (String),email(String), mpStatus (PVP)
        Debug.Log("Test");
        User user = new User(password,null,1,personalName,email,null);
        return dbInit.createUser(user);

    }


    public bool checkEmail(string email)
    {
        // Call DatabaseInit  checkEmailExist(email)
        bool EmailExist =dbInit.checkEmailExists(email);

        if (EmailExist)
        {
            Debug.Log("Email exists");
            return true;
        }
        else
        {
            Debug.Log("Email does not exists");
            return false;
        }

    }

}
