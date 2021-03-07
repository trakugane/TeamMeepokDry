using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 0;

    public void editProgress (int value)
    {
        level += value;
    }

    public void SavePlayer()
    {

    }

    public void LoadPlayer()
    {

    }
}
