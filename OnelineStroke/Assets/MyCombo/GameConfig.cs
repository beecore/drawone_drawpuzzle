using UnityEngine;

[System.Serializable]
public class GameConfig : MonoBehaviour
{

    [Header("")]
    public int adPeriod;
    public int rewardedVideoPeriod;
    public int showInterstitialAdAfterLevel;
    public string androidPackageID;

    public static GameConfig instance;
    private void Awake()
    {
        instance = this;
    }
}

