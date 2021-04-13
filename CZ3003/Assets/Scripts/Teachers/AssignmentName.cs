using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentName : MonoBehaviour
{
    [SerializeField]
    private Text assignmentTitle;

    void Start()
    {
        string buttonID = AssignmentIDController.assignmentID;
        this.Invoke(() => LoadInfo(buttonID), 0.05f);
    }

    public void LoadInfo(string buttonID)
    {


        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        Assets.Models.Assignment asnID = dbInit.getAssignemt(buttonID);
        assignmentTitle.text = "Assignment:" + "\n" + asnID.assignmentName;
    }
}
