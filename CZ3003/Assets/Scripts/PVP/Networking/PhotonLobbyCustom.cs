using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonLobbyCustom : MonoBehaviourPunCallbacks , ILobbyCallbacks
{
    public static PhotonLobbyCustom lobby;

    public string roomName;
    public int roomSize;
    public GameObject roomListingPrefab;
    public Transform roomsPanel;
    [SerializeField]
    private GameObject backButton;

    public List<RoomInfo> roomListings;

    public Text roomNameField;
    public GameObject msgtoDisplay;

   
    private void Awake()
    {
        lobby = this;  // creates the singleton, lives within the main menu scene;
    }

    // Update is called once per frame
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); // connects to master photon server
        }
        roomListings = new List<RoomInfo>();
        
       
    }
    
    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Player has connected from OnConnected()");
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Player has connected from OnConnectedToMaster()");
        PhotonNetwork.AutomaticallySyncScene = true; // when master client load the scene, all player connected to master client will load same scene
        //PhotonNetwork.NickName = "user" + Random.Range(0, 1000);
        PhotonNetwork.NickName = UserPlayer.userPlayer.userName;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        //RemoveRoomListings();

        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsPanel.GetChild(tempIndex).gameObject);
            }
            else
            {
                roomListings.Add(room);
                ListRoom(room);

            }
            
        } 
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void RemoveRoomListings()
    {
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.PlayerCount; // get no of players in room now
            tempButton.setRoom();
        }
    }

  
    public void CreateRoom()
    {
        Debug.Log("trying to create a new room");

        if (roomNameField.text.Equals(""))
        {
            msgtoDisplay.SetActive(true);
        }
        else
        {
            RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)4 }; // changed to 4 from roomSize
            PhotonNetwork.CreateRoom(roomName, roomOps);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        
    }

    // button functions , 1) HorseBtn for matchmaking 
    // 2) Cancel Button for cancel matchmaking 
    // 3) Back Button for PVPStartScene -> MainMenu
    


    public void returnToMainMenu()
    {
        //PhotonNetwork.DestroyAll();
        Destroy(MultiplayerSetting.multiplayerSetting.gameObject);
        Destroy(PhotonRoomCustom.room.gameObject);

        SceneManager.LoadScene("MainMenu");
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    public void JoinLobbyClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
