public interface IStageSelectManager
{
    void addBtnStageListener();
    void incrementCounter();
    bool loadStage(int currentProgress, int stageValue);
    void setSelectedStageValue();
}