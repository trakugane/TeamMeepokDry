using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
    
}
