using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player userPlayer;

    public string name;
    public int currProg;
    public int accountType;
    public int stageValue;
    public int selectedStageValue;

    private void Awake()
    {
        if (Player.userPlayer == null)
        {
            Player.userPlayer = this;

        }
        DontDestroyOnLoad(Player.userPlayer);     
    }

    public void incrementProgress()
    {
        currProg++;
        Debug.Log(currProg);
        if ((currProg % 10) == 6)
        {
            currProg = currProg + 5;
            Debug.Log(currProg);
        }
            
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currProg = 31; // Change this 11 to db.getCurrentProgress();
        name = "John";
        accountType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
