using UnityEngine;

public class TextManger : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject shopUI;
    private int showHint = 0; //with every incr show 3 hints;
    public static TextManger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void showHints()
    {
       
        int NumberOfHints = PlayerData.instance.GetHint();
        Debug.Log("NumberOfHints " + NumberOfHints);
        if (NumberOfHints <= 0)
        {
            GameObject.FindObjectOfType<UIControllerForGame>().OpenShop();
            return;
        }
        showHint++;
        Debug.Log("nhan hint");

        int maxPoint = showHint * 3;
        int minPoint = maxPoint - 2;
        if (showHint == 3)
            showHint = 0;
        //1 mean show 1 2 3
        //2 mean show 4 , 5 ,6;
        //3 mean show 7, 8 , 9
        WaysUI[] allWays = GameObject.FindObjectsOfType<WaysUI>();
       
        int count = allWays.Length;
        Debug.Log("createOrUpdateTextM count " + count);
        bool isAnyHintShown = false;
        for (int i = 0; i < count; i++)
        {
            WaysUI wayUi = allWays[i];
            Ways way = wayUi.getWayModel();

            string forStart = "";
            string forEnd = "";
            bool kiemtra = way.solutionPosition >= minPoint && way.solutionPosition <= maxPoint;
            if (!kiemtra)
            {
                Debug.Log("createOrUpdateTextM way.solutionPosition  " + way.solutionPosition);
                Debug.Log("createOrUpdateTextM minPoint  " + minPoint);
                Debug.Log("createOrUpdateTextM maxPoint  " + maxPoint);
            }
            Debug.Log("createOrUpdateTextM kiem tra  " + kiemtra);
            Debug.Log("createOrUpdateTextM way.pathTag " + way.pathTag);
            if (way.pathTag > 1)
            {
                string sol = way.solutions;

                string[] allSol = sol.Split(new char[] { ',' });
                int len = allSol.Length;
                Debug.Log("createOrUpdateTextM  allSol.Length " + allSol.Length);
                for (int j = 0; j < len; j++)
                {
                    string[] allSolRead = allSol[j].Split(new char[] { '_' });

                    int solIndex = int.Parse(allSolRead[0]);
                    if (solIndex >= minPoint && solIndex <= maxPoint)
                    {
                        if (allSolRead[1].Contains("s"))
                        {
                            forStart = solIndex + "";
                            forEnd = (solIndex + 1) + "";
                        }
                        else
                        {
                            forStart = (solIndex + 1) + "";
                            forEnd = (solIndex) + "";
                        }
                        Debug.Log("createOrUpdateTextM 1");
                        createOrUpdateTextM(way, forStart, forEnd);
                        isAnyHintShown = true;
                    }
                }
            }
            else if (way.solutionPosition >= minPoint && way.solutionPosition <= maxPoint)
            {
                Debug.Log("createOrUpdateTextM 2");
                forStart = way.solutionPosition + "";
                forEnd = (way.solutionPosition + 1) + "";
                createOrUpdateTextM(way, forStart, forEnd);
                isAnyHintShown = true;
            }
        }

        if (isAnyHintShown)
        {
          
            NumberOfHints -= 1;
            PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
            GameObject.FindObjectOfType<UIControllerForGame>().UpdateHint();
        }

        GameObject.FindObjectOfType<AnimationHandler>().runAnimations();
    }

    private TextM CreatObj(Vector3 pos)
    {
        GameObject go = Instantiate(textPrefab) as GameObject;
        go.transform.position = pos;
        go.transform.parent = transform;

        return go.GetComponent<TextM>();
    }

    public void createOrUpdateTextM(Ways way, string forStart, string forEnd)
    {
        Debug.Log("createOrUpdateTextM 3");
        Vector3 startPos = GridManager.GetGridManger().GetPosForGrid(way.startingGridPosition);
        Vector3 endPos = GridManager.GetGridManger().GetPosForGrid(way.endGridPositon);

        startPos.z = -1;
        endPos.z = -1;
        TextM stPosText = findTextMAtPos(startPos);
        TextM endPosText = findTextMAtPos(endPos);

        if (stPosText == null)
        {
            stPosText = CreatObj(startPos);
        }

        if (endPosText == null)
        {
            endPosText = CreatObj(endPos);
        }

        GameObject.FindObjectOfType<AnimationHandler>().addAnimationToRun(stPosText.transform.position, int.Parse(forStart), stPosText);
        GameObject.FindObjectOfType<AnimationHandler>().addAnimationToRun(endPosText.transform.position, int.Parse(forEnd), endPosText);
    }

    public TextM findTextMAtPos(Vector3 pos)
    {
        TextM[] allMeshe = GameObject.FindObjectsOfType<TextM>();

        if (allMeshe.Length > 0)
        {
            int len = allMeshe.Length;

            for (int i = 0; i < len; i++)
            {
                TextM tM = allMeshe[i];

                if (tM.transform.position.Equals(pos))
                {
                    return tM;
                }
            }
        }
        return null;
    }

    public void UpdateHint()
    {
        int NumberOfHints = PlayerData.instance.GetHint();
        NumberOfHints += 2;
        PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
        Timer.Schedule(this, 0.5f, () =>
        {
            GameObject.FindObjectOfType<UIControllerForGame>().LoadNextLevel();
        });
    }

    public void UpdateDialog_Hint()
    {
        int NumberOfHints = PlayerData.instance.GetHint();
        NumberOfHints += 2;
        PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
        Timer.Schedule(this, 0.5f, () =>
        {
            GameObject.FindObjectOfType<UIControllerForGame>().CloseShop();
        });
    }
    public void UpdateDialog_Level_Hint()
    {
        int NumberOfHints = PlayerData.instance.GetHint();
        NumberOfHints += 2;
        PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
        GameObject.FindObjectOfType<UIController>().CloseShop();
    }
}