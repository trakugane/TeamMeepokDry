using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    

    public void LoadScene(string sceneName)
    {
        /*if (Player.zhPlayer.currProg >= 11)*/
        SceneManager.LoadScene(sceneName);
        //AccountManager.accMgr.retrieveData();
    }

    
    
}
