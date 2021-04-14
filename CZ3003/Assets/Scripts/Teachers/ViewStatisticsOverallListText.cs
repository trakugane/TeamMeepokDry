using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewStatisticsOverallListText : MonoBehaviour
{

    [SerializeField]
    private Text progressText;

    public void SetProgressText(string textString)
    {
        progressText.text = textString;
    }

}
