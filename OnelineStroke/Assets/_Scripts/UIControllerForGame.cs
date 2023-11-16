using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerForGame : MonoBehaviour
{
    public GameObject settingUi;
    public GameObject dotAnim;
    private bool inableArb;
    public GameObject hintBtn;
    public PathReader PathReader;
    private Vector3 positionPrefab;
    public GameObject level_Prefab;
    public TMP_Text hint_Count_Text;
    public TMP_Text levelText;
    public GameObject uiShop;

    private void Start()
    {
        UpdateHint();
        CUtils.ChangeGameMusic();
        inableArb = true;
        hintBtn.SetActive(false);
        positionPrefab = new Vector3();
        if (PlayerData.instance.CurrentLevel == 1)
        {
            hintBtn.SetActive(true);
        }
        Admanager.instance.ShowBanner();
    }

    public void PlayButtonSound()
    {
        Sound.instance.PlayButton();
    }

    public void UpdateHint()
    {
        int level = LevelData.levelSelected;
        int hints = PlayerData.instance.GetHint();
        hint_Count_Text.text = hints.ToString();
        levelText.text = "LEVEL " + level;
    }

    public void ToggleSound()
    {
        bool isEnabled = !Sound.instance.IsEnabled();
        Sound.instance.SetEnabled(isEnabled);
    }

    public void ToggleMusic()
    {
        bool isEnabled = !Music.instance.IsEnabled();
        Music.instance.SetEnabled(isEnabled, true);
    }

    public void ShowPauseScene()
    {
        settingUi.SetActive(true);
    }

    public void CloseSettingcene()
    {
        settingUi.SetActive(false);
    }

    public void OpenStageMode()
    {
        UIController.mode = UIController.UIMODE.OPENLEVELSCREEN;
        SceneManager.LoadScene(0);
    }

    public void OpenHomeMode()
    {
        UIController.mode = UIController.UIMODE.OPENPLAYSCREEN;
        SceneManager.LoadScene(0);
    }

    public void ShowWinUi()
    {
        Sound.instance.Play(Sound.Others.Win);
        level_Prefab.SetActive(true);
        Timer.Schedule(this, 0.3f, () =>
        {
            Admanager.instance.ShowInterstitialAd();
        });
    }

    public void ShowStore()
    {
        CUtils.OpenStore();
    }

    public void Get_Hint()
    {
        int NumberOfHints = PlayerData.instance.GetHint();
        NumberOfHints += 1;
        PlayerData.instance.SaveData(PlayerData.instance.Hint, NumberOfHints);
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        UpdateHint();
        Sound.instance.PlayButton();
        int levelcurrent = LevelData.levelSelected + 1;
        PlayerData.instance.CurrentLevel = levelcurrent;
        LevelData.levelSelected = levelcurrent;
        PlayerData.instance.SaveData(PlayerData.instance.Level, PlayerData.instance.CurrentLevel);
        if (levelcurrent > LevelData.totalLevelsPerWorld)
        {
            levelcurrent = Random.Range(0, LevelData.totalLevelsPerWorld);
        }
        SceneManager.LoadScene(2);
    }

    public void ShowAnimationOnAllNodes()
    {
        GameObject.FindObjectOfType<DotAnimation>().gameObject.SetActive(false);
        WaysUI[] allUis = GameObject.FindObjectsOfType<WaysUI>();
        List<Vector3> dotAnimations = new List<Vector3>();

        foreach (WaysUI wayUi in allUis)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 pos = wayUi.childPos(i);
                if (!dotAnimations.Contains(pos))
                {
                    GameObject an = Instantiate(dotAnim) as GameObject;
                    an.GetComponent<DotAnimation>().setTargetScale(2.5f);
                    an.GetComponent<DotAnimation>().setEnableAtPosition(true, pos);
                    an.GetComponent<DotAnimation>().scalingSpeed = 2.5f;
                    dotAnimations.Add(pos);
                }
            }
        }

        hintBtn.SetActive(false);
        Invoke("ShowWinUi", 2f);
    }

    public void OpenSetting(bool isActive)
    {
        settingUi.SetActive(isActive);
    }

    public void OpenPolices()
    {
        Application.OpenURL("https://powegamestudio.blogspot.com/2022/03/privacy-policy-this-privacy-policy.html");
    }

    public void ShowAdReward(bool isShop = false)
    {
        Admanager.instance.Show_Get_Hint_AdReward(isShop);
    }

    public void OpenShop()
    {
        uiShop.SetActive(true);
    }

    public void CloseShop()
    {
        UpdateHint();
        uiShop.SetActive(false);
    }

    public void ShowHints()
    {
        TextManger.instance.showHints();
    }
}