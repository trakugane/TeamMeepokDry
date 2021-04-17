using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject textTemplate;
    private GameObject mpStatEmailText;

    [SerializeField]
    private int[] intArray;
    /*private List<Assets.Models.User> mpStatusList = new List<Assets.Models.User>();*/
    private List<GameObject> mpStatusEmailTexts;
    private List<GameObject> mpStatusScoreTexts;

    public void setTextTemplate(GameObject a)
    {
        textTemplate = a;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GenerateLeaderboard", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateLeaderboard()
    {
        mpStatusEmailTexts = new List<GameObject>();
        mpStatusScoreTexts = new List<GameObject>();
        List<Assets.Models.User> mpStatusList = new List<Assets.Models.User>();
        mpStatusList = Assets.DatabaseInit.dbInit.getLeaderboard();
        mpStatusList.Sort((x, y) => -x.mpStatus.AccumulatedPoints.CompareTo(y.mpStatus.AccumulatedPoints));
        
        foreach (Assets.Models.User mpStat in mpStatusList)
        {
            string[] emailString = mpStat.email.Split('@');

            GameObject mpStatEmailText = Instantiate(textTemplate);
            mpStatEmailText.SetActive(true);
            mpStatEmailText.GetComponent<LeaderboardListText>().SetEmailText(emailString[0]);
            mpStatEmailText.transform.SetParent(textTemplate.transform.parent, false);
            mpStatusEmailTexts.Add(mpStatEmailText.gameObject);

            GameObject mpStatScoreText = Instantiate(textTemplate);
            mpStatScoreText.SetActive(true);
            mpStatScoreText.GetComponent<LeaderboardListText>().SetScoreText(mpStat.mpStatus.AccumulatedPoints);
            mpStatScoreText.transform.SetParent(textTemplate.transform.parent, false);
            mpStatusScoreTexts.Add(mpStatScoreText.gameObject);
        }
    }
}