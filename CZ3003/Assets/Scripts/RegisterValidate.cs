using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class RegisterValidate : MonoBehaviour
{
    private Text txtErrorMessage;
    private InputField txtEmail, txtPassword , txtConfirmPassword,txtName;
    private DBManager validateDB;
    private InputsValidation validateInputs; 

    public void validateRegister()
    {
        validateInputs = new InputsValidation();
       // validateDB =GameObject.FindGameObjectWithTag("TagDB").GetComponent<DBManager>();
        txtEmail = GameObject.Find("Email").GetComponent<InputField>();
        txtPassword = GameObject.Find("Password").GetComponent<InputField>();
        txtConfirmPassword = GameObject.Find("ConfirmPassword").GetComponent<InputField>();
        txtName = GameObject.Find("Name").GetComponent<InputField>();
        txtErrorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();

        string Email, Password, cPassword, Name;
        Email = txtEmail.text;
        Password = txtPassword.text;
        cPassword = txtConfirmPassword.text;
        Name = txtName.text;
       
        if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(cPassword) || String.IsNullOrEmpty(Name))
        {
     
            //Display error message if any/all of the fields are left empty.
            txtErrorMessage.text = "Please fill in all the fields !";
   
        }
        else
        {
            string errorInputs,checkString = "";
            errorInputs = validateInputs.checkDBEmail(Email) + "\n" + validateInputs.checkName(Name) + "\n" + validateInputs.checkPassword(Password,cPassword);
            checkString = errorInputs;
            if (String.IsNullOrEmpty(checkString.Replace("\n","")))
            {
                //All Input Fields are Correct and Valid
                //Send to DBManager for storing into database and return back to login page.
                Assets.Models.User user = new Assets.Models.User(Password, null, 1, Name, Email, null);

                if (Assets.DatabaseInit.dbInit.createUser(user))
                {
                    txtErrorMessage.text = "Your Account Has Been Registered Successfully !";
                    System.Threading.Thread.Sleep(1000);
                    SceneManager.LoadScene("Login");
                }
                else
                    txtErrorMessage.text = "An issue was encountered ! Please contact the administrator.";
                
             }
             else
             {
                   txtErrorMessage.text = "The following fields are invalid: \n" + checkString;
             }
             
   
            }
  


        }


  

   

    

}
