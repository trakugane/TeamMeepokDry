using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WorldSelect : MonoBehaviour
{

    public int progStatus = 0;

    public Button btnWorld1;
    public Button btnWorld2;
    public Button btnWorld3;
    public Button btnWorld4;
    public Button btnWorld5;
    public Player player;
    public PlayerData playerData;


    // Start is called before the first frame update
    void Start()
    {
        btnWorld1.interactable = false;
        btnWorld2.interactable = false;
        btnWorld3.interactable = false;
        btnWorld4.interactable = false;
        btnWorld5.interactable = false;

        loadPlayerData();

    }

    public void loadPlayerData()
    {
        player = new Player();
        playerData = new PlayerData(player);
        playerData.level = 35;
        /*PlayerData playerData = new PlayerData(player);*/
        checkProgress(playerData.level);
        /*checkProgress(progStatus);*/
    }

    public void checkProgress(int progressStatus)
    {
        if (progressStatus >= 10 && progressStatus < 20)
        {
            btnWorld1.interactable = true;
        }
        else if (progressStatus >= 20 && progressStatus < 30)
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
        }
        else if (progressStatus >= 30 && progressStatus < 40)
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
        }
        else if (progressStatus >= 40 && progressStatus < 50)
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
            btnWorld4.interactable = true;
        }
        else if (progressStatus >= 50)
        {
            btnWorld1.interactable = true;
            btnWorld2.interactable = true;
            btnWorld3.interactable = true;
            btnWorld4.interactable = true;
            btnWorld5.interactable = true;
        }
    }

        // Update is called once per frame
        void Update()
        {
        
        }
}
