using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterScript : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    public static string descriptionParam;
    private string appStoreLink = "https://www.facebook.com/TeamMeePokDry/about/?ref=page_internal";

    public void ShareToTW(string linkParameter)
    {
        //Can modify by level or assignment
        string nameParameter = "ASSIGNMENT COMPLETED!";//this is limited in text length 
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + WWW.EscapeURL(nameParameter + "\n" + descriptionParam + "\n" + "Get the Game:\n" + appStoreLink +"\n#TeamMeePokDryGame #CZ3003"));
    }
}
