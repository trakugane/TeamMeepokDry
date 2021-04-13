using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssignmentListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private AssignmentListControl assignmentControl;

    [SerializeField]
    private AssignmentResultControlList assignmentResultControl;

    [SerializeField]
    private string myTextString;

    public void SetText(string questionString)
    {

        myTextString = questionString;
        myText.text = questionString;


    }




    public void OnClick(string myTextString)
    {

        Debug.Log(myTextString);


    }
}
