using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputsValidation : MonoBehaviour
{
    //DBManager validateDB = GameObject.FindGameObjectWithTag("TagDB").GetComponent<DBManager>();
    // Start is called before the first frame update


    public bool checkEmail(string e)
    {
        bool isMail = false;

        isMail = Regex.IsMatch(e, @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$", RegexOptions.IgnoreCase);
    
        return isMail;

    }
    public string checkPassword(string p, string cP)
    {
        if (p == cP)
        {
            //Check if Password is in correct format.
            if (p.Length >= 8)
                return "";
            else
                return "Password must be at least 8 characters.";
        }
        else
            return "Password and Confirm Password does not match";
    }

 
    public string checkDBEmail(string e)
    {

        if (checkEmail(e))
        {
       
            //Check with Database if Email exist.
            if (Assets.DatabaseInit.dbInit.checkEmailExists(e))
            {
                //Email Exist
                return "Email is already in used.";
            }
              
                
            else
            {
                return "";
            }
                
        }


        else
            return "Email Address is in incorrect format.";
    }

    public string checkName(string n)
    {

        if (n.Length >= 8)
            return "";
        else
            return "Name must be at least 8 characters.";
    }


}
