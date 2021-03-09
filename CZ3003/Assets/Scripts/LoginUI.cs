using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class LoginUI : MonoBehaviour
{
    /*EventSystem system;*/
    /*public Button LoginBtn;*/
    public InputField Username;
    public InputField Password;
    AccountManager am;

    // Start is called before the first frame update
    void Start()
    {
        am = gameObject.AddComponent<AccountManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkLogin(string sceneName)
    {
        Debug.Log("Login Button Pressed!");
        // Verify if username in database and password is correct
        if (am.validateUser(Username, Password) == true)
        {
            if (am.retrieveData(Username.text.ToString(), Password.text.ToString()) == true)
            {
                Debug.Log(PlayerPrefs.GetString("name"));
                SceneManager.LoadScene(sceneName);
            } 
            else
            {
                // Display Error (Account data not retrievable)
            }
        }
        else
        {
            // Display Error (Account not valid)
        }
    }

    

    
}
