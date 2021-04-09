﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
public class RoomButton : MonoBehaviour
{
    public Text nameText;
    public Text sizeText;

    public string roomName;
    public int roomSize;

    public void setRoom()
    {
        nameText.text = roomName;
        //sizeText.text = roomSize.ToString();
        sizeText.text = roomSize.ToString() + "/4";
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
