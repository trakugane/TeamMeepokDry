using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldSelectUI : MonoBehaviour
{

    public int currProg;
    public Button btnWorld1;
    public Button btnWorld2;
    public Button btnWorld3;
    public Button btnWorld4;
    public Button btnWorld5;


    // Start is called before the first frame update
    void Start()
    {
        currProg = PlayerPrefs.GetInt("currProg");
        addBtnWorldListener();
        disableAllWorlds();
        if (loadWorld(currProg) == true)
            Debug.Log("Load World Successful");
        else
            Debug.Log("Error: currProg value is < 0 or > 55");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBtnWorldListener()
    {
        btnWorld1.onClick.AddListener(setStageValue);
        btnWorld2.onClick.AddListener(setStageValue);
        btnWorld3.onClick.AddListener(setStageValue);
        btnWorld4.onClick.AddListener(setStageValue);
        btnWorld5.onClick.AddListener(setStageValue);
    }

    public void setStageValue()
    {
        /*PlayerPrefs.SetInt("stageValue", stageValue);*/
        
        string btnName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        Debug.Log(btnName == "BtnWorld1");
        if (btnName == "BtnWorld1")
            PlayerPrefs.SetInt("stageValue", 15);
        if (btnName == "BtnWorld2")
            PlayerPrefs.SetInt("stageValue", 25);
        if (btnName == "BtnWorld3")
            PlayerPrefs.SetInt("stageValue", 35);
        if (btnName == "BtnWorld4")
            PlayerPrefs.SetInt("stageValue", 45);
        if (btnName == "BtnWorld5")
            PlayerPrefs.SetInt("stageValue", 55);

    }

    public bool loadWorld(int currentProgress)
    {

        if ((currentProgress > 10) && (currentProgress <= 15))
        {
            btnWorld1.interactable = true;
            return true;
        }
        else if ((currentProgress > 20) && (currentProgress <= 25))
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            return true;
        }
        else if ((currentProgress > 30) && (currentProgress <= 35))
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
            return true;
        }
        else if ((currentProgress > 40) && (currentProgress <= 45))
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
            btnWorld4.interactable = true;
            return true;
        }
        else if ((currentProgress > 50) && (currentProgress <= 55))
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
            btnWorld4.interactable = true;
            btnWorld5.interactable = true;
            return true;
        }

        return false;
            
    }

    void disableAllWorlds()
    {
        btnWorld1.interactable = false;
        btnWorld2.interactable = false;
        btnWorld3.interactable = false;
        btnWorld4.interactable = false;
        btnWorld5.interactable = false;
    }
}
