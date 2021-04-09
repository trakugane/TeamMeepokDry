using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PhotonRoomCustom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoomCustom room;
    private PhotonView PV;
    
    private bool isGameLoaded;
    public int currentScene;

    //Player info + display CurrentPlayer Panel
    Player[] photonPlayers;
    private int numberOfPlayersInRoom;
    private int playerInGame;
    
    


    //Delayed start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float timerAtMaxPlayer;
    private float timeToStart;

    // Custom
    public GameObject lobbyGO;
    public GameObject roomGO;
    public Transform playersPanel;
    public GameObject playerListingPrefab;
   
    // host startBtn , click to make hostStart = true;
    [SerializeField]
    public GameObject hostStartBtn;
    private bool hostStart;

    private void Awake()
    {
        if (PhotonRoomCustom.room == null)
        {
            PhotonRoomCustom.room = this;
        }
        else
        {
            if (PhotonRoomCustom.room != this)
            {
                Destroy(PhotonRoomCustom.room.gameObject); // dontdestroyonload !!
                PhotonRoomCustom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void Start()
    {
        if (PV == null)
        {
            PV = GetComponent<PhotonView>();
        }
        readyToCount = false;
        readyToStart = false;
        hostStart = false; //
        lessThanMaxPlayers = startingTime;
        timerAtMaxPlayer = startingTime; // ??? can i use [serializedfield] startingtime? from roomcontroller
        timeToStart = startingTime;
    }

    public void onHostStartBtnClick()
    {
        hostStart = true;
    }

    void Update()
    {

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if (numberOfPlayersInRoom == 1)
            {
                RestartTimer();
            }
            if (!isGameLoaded)
            {
                if (readyToStart)
                {
                    timerAtMaxPlayer = timerAtMaxPlayer - Time.deltaTime;
                    lessThanMaxPlayers = timerAtMaxPlayer;
                    timeToStart = timerAtMaxPlayer;
                }
                else if (readyToCount)  // once > 1person, start to countdown to start game, disable it first, only allow 3/3
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                //Debug.Log("Display time to start to the players " + timeToStart);
                if (timeToStart <= 0 || hostStart) // if time = 0 ; or host click startbtn -> start game
                {
                    Debug.Log("at line98");
                    StartGame();
                }
            }
        }
    }
    void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        timerAtMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
    }
    public void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSetting.multiplayerSetting.multiplayerScene); 
        // change multiplayerScene from multiplayersetting object [serializedfield]
        
    }

    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        

        if (PhotonNetwork.IsMasterClient)
        {
            hostStartBtn.SetActive(true);
        }

        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList; // array of players
        numberOfPlayersInRoom = photonPlayers.Length;   // number of players by calling len(
        

        // put into lobby custom PhotonNetwork.NickName = "user" + Random.Range(0, 1000); // retrieve username from db
        

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            
            //
            if (numberOfPlayersInRoom > 1)
            {
                //readyToCount = true;
            }
            if (numberOfPlayersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }

        /*else   // if not delaystart, startgame straight away
        {
 
            StartGame();
        } */

    }

    void ClearPlayerListings()
    {
        if (playersPanel != null)
        {
            for (int i = playersPanel.childCount - 1; i >= 0; i--)
            {
                Destroy(playersPanel.GetChild(i).gameObject);
            }
        }
    }

    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        Debug.Log("Join a room from OnPlayerEnteredRoom(), is this for non-host?");
        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        numberOfPlayersInRoom++;

        if (MultiplayerSetting.multiplayerSetting.delayStart)
        {
            if (numberOfPlayersInRoom > 1)
            {
                //readyToCount = true;  // disabled ready to count
            }
            if (numberOfPlayersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    return;
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    public void LeaveRoom()
    {

        if (PhotonNetwork.PlayerList.Length > 1)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[1]); // ???????? bad logic?
        }
        PhotonNetwork.LeaveRoom();
        
    }
    // when master client change , change host and activate some1s start button
    void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
    {
        if (newMasterClient.IsMasterClient)
        {
            hostStartBtn.SetActive(true);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
        // PhotonRoomCustom.room.numberOfPlayersInRoom--;
        ClearPlayerListings();
        ListPlayers();
    }


    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;    // 0 / 1
        if (currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene)
        {
            isGameLoaded = true;
            if (MultiplayerSetting.multiplayerSetting.delayStart)
            {
                Debug.Log("photonroom line199 - calls LoadedGameScene for masterclient only. ");
                //PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                Debug.Log("photonroom line206 - Create 1 player (me) only");
                
               
            }
        }
    }

 

   

    
}
