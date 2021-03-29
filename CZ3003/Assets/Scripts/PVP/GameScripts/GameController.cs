using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    private PhotonView PV;

    // for count down, not immediately start game when scene loaded
    [SerializeField] GameObject timer321Panel;
    float currentTime = 0f;
    float startingTime = 3f;             // countdown 321? can change ? put to serializefield
    [SerializeField] Text countdownText;

    // for game
    public GameObject gamePanel;
    private bool startGame = false;
    [SerializeField] Text gameTimer;
    float gameTime = 60f;               // set game time here now 60seconds / game
    public int pointsFromGame;         // must be public for QuestionBank.cs to access -> click correct answer = ++++

    // for local , 1) my name 2) questions for me to play
    public GameObject userObj;
    public Text usernameTEXT;
    public Text userPoints;
    public GameObject qnsPrefab;
    public GameObject questionPanel;

    // network
    public GameObject PlayerXPrefab;
    private int winCondition = 5; // just need to answer 5 qn example
    public GameObject gameEndPanel;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        currentTime = startingTime;
        pointsFromGame = 0;             // initialize everyones points to 0 !
        setMyUsername();
        DisplayPlayersInGame();
    }

    public void Update()
    {
        DisplayPVPQuestions();
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            gamePanel.SetActive(true);
            userObj.SetActive(true);
            timer321Panel.SetActive(false);
            startGame = true;
        }

        if (startGame)
        {
            gameTime -= 1 * Time.deltaTime;
            gameTimer.text = "Time Left: " + gameTime.ToString("0") + " sec";
            if (gameTime <= 0 )  // add in win condition , or player 100 points example
            {
                
                Debug.Log("you have achieved a score of " + pointsFromGame);
                

                //disconnect use this 2
                DestroyObject();
                DisconnectPlayer();
            }

            if (pointsFromGame == winCondition)     // reach wincondition (5points currently)
            {
                RPC_GameEnd();
            }
        }
    }
    private void Awake()
    {
        if (GameController.gameController == null)
        {
            GameController.gameController = this;
        }
        else
        {
            if (MultiplayerSetting.multiplayerSetting != this)
            {
                Destroy(this.gameObject);
            }
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    // for leaving room / back button in pvp
    public void LeaveGame()
    {
        DestroyObject();
        DisconnectPlayer();
    }
    public void DestroyObject()
    {
        Destroy(MultiplayerSetting.multiplayerSetting.gameObject);
        Destroy(PhotonRoom.room.gameObject);
    }
    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
        Destroy(MultiplayerSetting.multiplayerSetting);
        Destroy(PhotonRoom.room);
        SceneManager.LoadScene("MainMenu");

    }
    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.Disconnect();

        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
    }

    // client side 1 - set own username 2 - display questions over n over again
    public void setMyUsername()
    {
        //String username = PhotonRoom.room.username; // trying to get straight from photonnetwork
        String username = PhotonNetwork.NickName;
        usernameTEXT.text = username;
    }

    public void DisplayPVPQuestions()
    {

        if (PhotonNetwork.InRoom && questionPanel == null)
        {
            questionPanel = Instantiate(qnsPrefab, gamePanel.transform);

        }

    }

    // network part
    public void DisplayPlayersInGame()
    {
        if (PhotonNetwork.InRoom)
        {
            int playersFromPhotonNetwork = PhotonNetwork.PlayerList.Length;

         
            int i = 0; 
            foreach (Player player in PhotonNetwork.PlayerList) 
            {
                if (!player.IsLocal)
                {
                    if (i == 0) // if in game 4person, 1 user, 3 opponent, 0 = 1st opponent
                    {

                        string username = player.NickName;
                        GameObject newObject = (GameObject)GameController.Instantiate(PlayerXPrefab, new Vector3(0.52f, 2.6f, 0), Quaternion.identity, gamePanel.transform);
                        newObject.GetComponent<Image>().color = new Color32(255, 93, 93, 255);
                        newObject.name = username;
                        newObject.GetComponentInChildren<Text>().text = "Player " + username;
                    } 
                    else if (i == 1) // 2nd opponent
                    {
                        string username = player.NickName;
                        GameObject newObject = (GameObject)GameController.Instantiate(PlayerXPrefab, new Vector3(1.56f, 3.1f, 0), Quaternion.identity, gamePanel.transform);
                        newObject.GetComponent<Image>().color = new Color32(128, 149, 229, 255);
                        newObject.name = username;
                        newObject.GetComponentInChildren<Text>().text = "Player " + username;
                    }
                    else if (i == 2) // 3rd opponent
                    {
                        string username = player.NickName;
                        GameObject newObject = (GameObject)GameController.Instantiate(PlayerXPrefab, new Vector3(2.1f, 2.1f, 0), Quaternion.identity, gamePanel.transform);
                        newObject.GetComponent<Image>().color = new Color32(88, 236, 125, 255);
                        newObject.name = username;
                        newObject.GetComponentInChildren<Text>().text = "Player " + username;
                        
                    }
                    i++; // 
                } 
                
            } 
        }
    }


    [PunRPC]
    public void RPC_UpdateOnOtherClient()
    {
        PV.RPC("RPC_UpdatePoints", RpcTarget.Others, PhotonNetwork.NickName, pointsFromGame); // change to Player.username once db up
    }

    [PunRPC]
    public void RPC_UpdatePoints(string otherUsername, int OtherPlayerPoints)
    {
        GameObject obj = GameObject.Find(otherUsername);
        obj.GetComponentInChildren<Text>().text = otherUsername + " : " + OtherPlayerPoints;

    }

    [PunRPC]
    public void RPC_GameEnd()
    {
        PV.RPC("RPC_ShowGameEndPanel", RpcTarget.All, PhotonNetwork.NickName);
    }

    [PunRPC]
    public void RPC_ShowGameEndPanel(string username)
    {
        gamePanel.SetActive(false); //disable
        gameEndPanel.SetActive(true); // enable

        string gameEndMessage = username + " has won." +
            // add in 'your updated rank points, eg; player.rankpoints +/- 3 .toString()
            "\n return to main menu.";

        gameEndPanel.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = gameEndMessage;
    }
}
