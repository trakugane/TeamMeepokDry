using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RegisterUI : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public InputField confirmPassword;
    public InputField personalName;

    public Button btnRegister;

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

    /*public void checkRegister(string sceneName)
    {
        Debug.Log("Register Button Pressed!");
        int codeMsg = am.validateAccountCreation(email, password, confirmPassword, personalName);
        if (codeMsg == 0)
            SceneManager.LoadScene(sceneName);
        else
        {
            // Need display error message to UI (10/3/21)
            Debug.Log("Error: Account Creation");
            if (codeMsg == 1)
            {
                Debug.Log("Error: Email already exists");
            }
            else if (codeMsg == 2)
            {
                Debug.Log("Error: Passwords don't match");
            }
            else if (codeMsg == 3)
            {
                Debug.Log("Error: Storing in database unsuccessful");
            }
            else
            {
                Debug.Log("Error: Unknown Error");
            }
        }
            
    }*/

    
}
