using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    // game buttons
    [SerializeField]
    private GameObject horseButton;
    [SerializeField]
    private GameObject lmsButton;

    // misc buttons like cancel search / 'back' PVPStartScene -> MainMenu
    [SerializeField]
    private GameObject cancelButton;
    [SerializeField]
    private GameObject backButton;

   
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
        
        horseButton.SetActive(true);
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
        
        horseButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("tried to join a room but failed, no open games (room) available");
        CreateRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("trying to create a new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.multiplayerSetting.maxPlayers };
        Debug.Log(randomRoomName);
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("tried to create a new room but failed, there must already be a room with same name");
        CreateRoom(); // try to create room again
    }

    // button functions , 1) HorseBtn for matchmaking 
    // 2) Cancel Button for cancel matchmaking 
    // 3) Back Button for PVPStartScene -> MainMenu
    public void OnHorseButtonClicked()
    {
        Debug.Log("Battle button was clicked");
        horseButton.SetActive(false);
        cancelButton.SetActive(true);
        backButton.SetActive(false);
        
        PhotonNetwork.JoinRandomRoom();

    }

    public void onCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        horseButton.SetActive(true);
        backButton.SetActive(true);
        PhotonRoom.room.noOfPlayerPanel.SetActive(false); // cancel matchmaking = remove 'Currently 2/4player' text
        
        PhotonNetwork.LeaveRoom();
        PhotonRoom.room.hostStartBtn.SetActive(false);  // set false
    }

    public void returnToMainMenu()
    {
        //PhotonNetwork.DestroyAll();
        Destroy(MultiplayerSetting.multiplayerSetting.gameObject);
        Destroy(PhotonRoom.room.gameObject);

        SceneManager.LoadScene("MainMenu");
    }
}
