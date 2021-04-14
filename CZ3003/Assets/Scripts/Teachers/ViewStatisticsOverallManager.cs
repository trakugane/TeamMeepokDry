using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewStatisticsOverallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject textTemplate;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GenerateSummary", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSummary()
    {
        List<Assets.Models.StageProgress> spProgressList = new List<Assets.Models.StageProgress>();
        spProgressList = Assets.DatabaseInit.dbInit.getCummulativeAttemptStages();

        foreach (Assets.Models.StageProgress spProg in spProgressList)
        {
            GameObject mpProgText = Instantiate(textTemplate);
            mpProgText.SetActive(true);
            string pushText = "";
            if (spProg.Stage >= 10 && spProg.Stage <= 15)
                pushText = "+ World" + "\n" + "Stage: " + spProg.Stage + "\n" + "Attempts: " + spProg.Attempt;
            else if (spProg.Stage >= 20 && spProg.Stage <= 25)
                pushText = "- World" + "\n" + "Stage: " + spProg.Stage + "\n" + "Attempts: " + spProg.Attempt;
            else if (spProg.Stage >= 30 && spProg.Stage <= 35)
                pushText = "x World" + "\n" + "Stage: " + spProg.Stage + "\n" + "Attempts: " + spProg.Attempt;
            else if (spProg.Stage >= 40 && spProg.Stage <= 45)
                pushText = "/ World" + "\n" + "Stage: " + spProg.Stage + "\n" + "Attempts: " + spProg.Attempt;
            Debug.Log(pushText);
            mpProgText.GetComponent<ViewStatisticsOverallListText>().SetProgressText(pushText);
            mpProgText.transform.SetParent(textTemplate.transform.parent, false);

        }
    }
}
