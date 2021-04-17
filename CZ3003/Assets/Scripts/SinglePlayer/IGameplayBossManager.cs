using UnityEngine.UI;

public interface IGameplayBossManager
{
    void addBtnResultScreenListener();
    void checkWorld(int selectedStage);
    void fetchQn();
    void generateQuestion();
    void onButtonClicked(Button btn);
    void resetStage();
    void setBtnAns();
    void setText();
    void setTimer(float value);
    void Shuffle(string strans, string strrandomA, string strrandomB, string strrandomC);
}