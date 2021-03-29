using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : MonoBehaviour
{
    public static UserPlayer userPlayer;

    public string userName;
    public string email;
    public int currProg;
    public int accountType;
    public int stageValue;
    public int selectedStageValue;

    private void Awake()
    {
        if (UserPlayer.userPlayer == null)
        {
            UserPlayer.userPlayer = this;

        }
        DontDestroyOnLoad(UserPlayer.userPlayer);     
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

    public void incrementStageValue()
    {
        selectedStageValue++;
        if ((selectedStageValue % 10) == 6)
        {
            selectedStageValue = selectedStageValue + 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*currProg = 31; // Change this 11 to db.getCurrentProgress();
        name = "John";
        accountType = 1;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
