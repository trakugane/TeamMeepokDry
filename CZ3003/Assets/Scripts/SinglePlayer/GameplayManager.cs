using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameplayManager : MonoBehaviour
{

    public int selectedStageValue;
    public GameObject imgStage1;
    public GameObject imgStage2;
    public GameObject imgStage3;
    public GameObject imgStage4;
    /*public GameObject Qntxt;*/


    /*public void onClick()
    {
        // Check if button is correct answer.

        // When correct
        Player.zhPlayer.incrementProgress();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log(Qntxt.GetComponent<Text>().text);*/
        selectedStageValue = Player.userPlayer.selectedStageValue;
        Debug.Log("selectedStageValue: " + selectedStageValue);
        disableAllStageImage();
        displaySelectedStageImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displaySelectedStageImage()
    {
        if ((selectedStageValue % 10) == 1)
            imgStage1.SetActive(true);
        if ((selectedStageValue % 10) == 2)
            imgStage2.SetActive(true);
        if ((selectedStageValue % 10) == 3)
            imgStage3.SetActive(true);
        if ((selectedStageValue % 10) == 4)
            imgStage4.SetActive(true);
    }

    void disableAllStageImage()
    {
        imgStage1.SetActive(false);
        imgStage2.SetActive(false);
        imgStage3.SetActive(false);
        imgStage4.SetActive(false);
    }
}
