using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StageSelectUI : MonoBehaviour
{

    public int currProg;
    public int stageVal;
    public Image imgMapLandColour;
    public Button btnStage1;
    public Button btnStage2;
    public Button btnStage3;
    public Button btnStage4;
    public Button btnStage5;

    // Start is called before the first frame update
    void Start()
    {
        stageVal = PlayerPrefs.GetInt("stageValue");
        currProg = PlayerPrefs.GetInt("currProg");
        
        changeStageColor(stageVal);
        addBtnStageListener();
        disableAllStages();
        if (loadStage(currProg, stageVal) == true)
            Debug.Log("Load Stage Successful");
        else
            Debug.Log("Error: currProg value is < 0 or > 55");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBtnStageListener()
    {
        btnStage1.onClick.AddListener(setSelectedStageValue);
        btnStage2.onClick.AddListener(setSelectedStageValue);
        btnStage3.onClick.AddListener(setSelectedStageValue);
        btnStage4.onClick.AddListener(setSelectedStageValue);
        btnStage5.onClick.AddListener(setSelectedStageValue);
    }

    public void setSelectedStageValue()
    {
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        int stageValue = (stageVal / 10) * 10;
        if (btnName == "Btn_Stage1")
        {
            PlayerPrefs.SetInt("selectedStageValue", stageValue + 1);
            Debug.Log("selectedStageValue: " + PlayerPrefs.GetInt("selectedStageValue"));
        }
        if (btnName == "Btn_Stage2")
        {
            PlayerPrefs.SetInt("selectedStageValue", stageValue + 2);
            Debug.Log("selectedStageValue: " + PlayerPrefs.GetInt("selectedStageValue"));
        }  
        if (btnName == "Btn_Stage3")
        {
            PlayerPrefs.SetInt("selectedStageValue", stageValue + 3);
            Debug.Log("selectedStageValue: " + PlayerPrefs.GetInt("selectedStageValue"));
        }
        if (btnName == "Btn_Stage4")
        {
            PlayerPrefs.SetInt("selectedStageValue", stageValue + 4);
            Debug.Log("selectedStageValue: " + PlayerPrefs.GetInt("selectedStageValue"));
        }  
        if (btnName == "Btn_Stage5")
        {
            PlayerPrefs.SetInt("selectedStageValue", stageValue + 5);
            Debug.Log("selectedStageValue: " + PlayerPrefs.GetInt("selectedStageValue"));
        }
    }

        public bool loadStage(int currentProgress, int stageValue)
    {
        if (currentProgress >= stageValue)
        {
            btnStage1.interactable = true;
            btnStage2.interactable = true;
            btnStage3.interactable = true;
            btnStage4.interactable = true;
            btnStage5.interactable = true;
            return true;
        }
        else
        {
            if ((currentProgress % 10) == 1)
            {
                btnStage1.interactable = true;
                return true;
            }
            else if ((currentProgress % 10) == 2)
            {
                btnStage1.interactable = true;
                btnStage2.interactable = true;
                return true;
            }
            else if ((currentProgress % 10) == 3)
            {
                btnStage1.interactable = true;
                btnStage2.interactable = true;
                btnStage3.interactable = true;
                return true;
            }
            else if ((currentProgress % 10) == 4)
            {
                btnStage1.interactable = true;
                btnStage2.interactable = true;
                btnStage3.interactable = true;
                btnStage4.interactable = true;
                return true;
            }
        }

        

        return false;

    }

    void disableAllStages()
    {
        btnStage1.interactable = false;
        btnStage2.interactable = false;
        btnStage3.interactable = false;
        btnStage4.interactable = false;
        btnStage5.interactable = false;
    }

    void changeStageColor(int stageValue)
    {
        if (stageValue == 15)
            imgMapLandColour.color = new Color32(113, 201, 109, 255);
        if (stageValue == 25)
            imgMapLandColour.color = new Color32(229, 237, 106, 255);
        if (stageValue == 35)
            imgMapLandColour.color = new Color32(250, 176, 0, 255);
        if (stageValue == 45)
            imgMapLandColour.color = new Color32(250, 100, 0, 255);
        if (stageValue == 55)
            imgMapLandColour.color = new Color32(235, 47, 47, 255);
    }
}
