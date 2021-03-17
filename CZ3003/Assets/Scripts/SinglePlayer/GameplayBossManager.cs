using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class GameplayBossManager : MonoBehaviour
{

    public int selectedStageValue;
    public GameObject timerText;
    public float timerCurrent = 0;
    public bool playingGame;
    // Start is called before the first frame update
    void Start()
    {
        selectedStageValue = Player.userPlayer.selectedStageValue;
        playingGame = true;
        timerCurrent = 60;
        setTimer(timerCurrent);
    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame == true)
        {
            timerCurrent -= Time.deltaTime;
            setTimer(timerCurrent);
        }
    }

    public void setTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        timerText.GetComponent<Text>().text = time.ToString("ss");

        if (timerCurrent <= 0)
        {
            playingGame = false;
            Debug.Log("RESULT SCREEN APPEAR");
            // Result panel appear.
        }
    }
}
