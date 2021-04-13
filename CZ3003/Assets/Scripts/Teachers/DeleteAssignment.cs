using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAssignment : MonoBehaviour
{
    // Start is called before the first frame update
    public async void ButtonClickedAsync()
    {
        string buttonID = gameObject.transform.parent.name;
        Debug.Log(buttonID);
        Transform buttonTrans = gameObject.transform.parent.transform;
        Transform textTrans = buttonTrans.Find("Text");

        Assets.DatabaseInit dbInit = GameObject.FindGameObjectWithTag("DBinit").GetComponent<Assets.DatabaseInit>();
        Boolean questionDeleted;
        questionDeleted = await dbInit.deleteAssignmentAsync(buttonID, textTrans.GetComponent<Text>().text);

        Debug.Log(questionDeleted);

        if (questionDeleted.Equals(true))
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
