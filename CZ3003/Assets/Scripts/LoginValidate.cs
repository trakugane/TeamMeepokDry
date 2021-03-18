using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;



public class LoginValidate : MonoBehaviour
{

    private Text txtErrorMessage;
    private InputField txtEmail,txtPassword;
    private DBManager validateLoginDB;
    private InputsValidation validateEmail;

    

    public void Login()
    {
        
        validateEmail = new InputsValidation();
        validateLoginDB =GameObject.FindGameObjectWithTag("TagDB").GetComponent<DBManager>();
        txtErrorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();
        txtEmail = GameObject.Find("Email").GetComponent<InputField>();
        txtPassword = GameObject.Find("Password").GetComponent<InputField>();


        string Email = txtEmail.text;
        string Password= txtPassword.text;
        
       
    
        if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password))    
        {
            //Display Error Message when Email and Password fields are empty.
            txtErrorMessage.text = "Please Enter your Email Address and Password !";
        }
        else if (!String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
        {

            //Check if Email is a valid email address
            if (validateEmail.checkEmail(Email)){
                //Valid Email Address, Call Database Method to Validate if record exist
                if (validateLoginDB.validateUser(Email,Password)) 
                {
                    txtErrorMessage.text = "Login is Successful !";
                    System.Threading.Thread.Sleep(1000);
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    //Display Error Message when inputs does not match record in database
                    txtErrorMessage.text = "Email address or Password is incorrect !";
                }
                
            }
            else
            {
                //Display Error Message when Email address is not in correct format
                txtErrorMessage.text = "Please Enter a valid Email Address !";
            }
  
        }

    }

   
}
