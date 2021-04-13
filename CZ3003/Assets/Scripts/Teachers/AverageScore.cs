using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AverageScore : MonoBehaviour
{
    [SerializeField]
    private Text avgScore;

    private void Start()
    {
        Invoke(nameof(SetText), 0.1f);
    }

    public void SetText()
    {
        avgScore.text = "Average Score: "+ AssignmentIDController.averageScore.ToString();
    }

}
