using UnityEngine.UI;

public interface IGameplayManager
{
    void addBtnResultScreenListener();
    void checkWorld();
    void fetchQn();
    void onButtonClicked(Button btn);
    void removeBtnNextStageListener();
    void resetStage();
    void setBtnAns();
    void setNextStage();
    void setStageIndicator();
    void setText();
    void Shuffle(string strans, string strrandomA, string strrandomB, string strrandomC);
}