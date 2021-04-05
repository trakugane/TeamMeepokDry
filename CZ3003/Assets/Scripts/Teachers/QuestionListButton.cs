using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private QuestionListControl questionControl;

    private string myTextString;

    public void SetText(string questionString)
    {
        myTextString = questionString;
        myText.text = questionString;
    }

    public void OnClick()
    {
        questionControl.ButtonClicked(myTextString);
    }
}
