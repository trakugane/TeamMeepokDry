public interface IGameController
{
    void DestroyObject();
    void DisconnectPlayer();
    void DisplayPlayersInGame();
    void DisplayPVPQuestions();
    void LeaveGame();
    void RPC_GameEnd();
    void RPC_ShowGameEndPanel(string username);
    void RPC_UpdateOnOtherClient();
    void RPC_UpdatePoints(string otherUsername, int OtherPlayerPoints);
    void setMyUsername();
    void Update();
}