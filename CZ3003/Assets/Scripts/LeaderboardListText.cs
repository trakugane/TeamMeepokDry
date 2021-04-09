using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardListText : MonoBehaviour
{
    [SerializeField]
    private Text emailText;

    [SerializeField]
    private Text scoreText;

    private string myTextString;

    public void SetScoreText(int textString)
    {
        /*myTextString = textString.ToString();*/
        scoreText.text = textString.ToString();
    }

    public void SetEmailText(string textString)
    {
        emailText.text = textString;
    }
}
