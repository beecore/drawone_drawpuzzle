using UnityEngine;

public class Admanager : MonoBehaviour
{
    public static Admanager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        Advertisements.Instance.SetUserConsent(true);
        if (Advertisements.Instance.UserConsentWasSet())
        {
            Advertisements.Instance.Initialize();
        }
    }

    private void Start()
    {
        ShowBanner();
    }

    public void ShowBanner()
    {
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    public void ShowInterstitialAd()
    {
        Advertisements.Instance.ShowInterstitial();
    }

    public void Show_Get_Hint_AdReward(bool isShop)
    {
        if (Advertisements.Instance.IsRewardVideoAvailable())
        {
            if (isShop)
            {
                Advertisements.Instance.ShowRewardedVideo(CompleteIsShopHint);
            }
            else
            {
                Advertisements.Instance.ShowRewardedVideo(CompleteGetHint);
            }
        }
    }

    public void Show_Get_Hint_level_AdReward()
    {
        if (Advertisements.Instance.IsRewardVideoAvailable())
        {
            Advertisements.Instance.ShowRewardedVideo(CompleteIsShopLevelHint);
        }
    }
    private void CompleteGetHint(bool completed)
    {
        TextManger.instance.UpdateHint();
    }

    private void CompleteIsShopHint(bool completed)
    {
        TextManger.instance.UpdateDialog_Hint();
    }
    private void CompleteIsShopLevelHint(bool completed)
    {
        int NumberOfHints = PlayerData.instance.GetHint();
        NumberOfHints += 2;
        PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
        GameObject.FindObjectOfType<UIController>().CloseShop();
    }
}