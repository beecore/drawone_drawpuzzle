using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance = null;
    public string Hint = "hint";
    public string Level = "level";

    private void Awake()
    {
        instance = this;
        LoadData();
    }

    public int CurrentLevel
    {
        get;
        set;
    }

    public Dictionary<int, string> TotalLevelCrossed
    {
        get;
        set;
    }

    public Dictionary<int, int> LEVELUNLOCKED
    {
        get;
        set;
    }

    public int GetTotalLevelCrossed()
    {
        int totalValue = 0;

        foreach (string levelCrossed in TotalLevelCrossed.Values)
        {
            string[] levels = levelCrossed.Split(new char[] { ',' });
            totalValue += (levels.Length - 1);
        }

        return totalValue;
    }

    public int LevelCrossedForOneWorld(int worldNumber)
    {
        string levelString = TotalLevelCrossed[worldNumber];

        return levelString.Split(new char[] { ',' }).Length - 1;
    }

    public bool IsLevelCrossed(int worldNumber, int levelToWatch)
    {
        string levelString = TotalLevelCrossed[worldNumber];

        string[] levelsCrossed = levelString.Split(new char[] { ',' });

        foreach (string levelCrossed in levelsCrossed)
        {
            int value = int.Parse(levelCrossed);

            if (value == levelToWatch)
            {
                return true;
            }
        }
        return false;
    }

    public int GetLargestLevel(int worldNumber)
    {
        string levelString = TotalLevelCrossed[worldNumber];

        string[] levelsCrossed = levelString.Split(new char[] { ',' });

        int temp = 1;
        foreach (string levelCrossed in levelsCrossed)
        {
            int value = int.Parse(levelCrossed);

            if (value > temp)
            {
                temp = value;
            }
        }

        return temp;
    }

    public void SetLevelCrossed(int worldNumber, int level)
    {
        if (!IsLevelCrossed(worldNumber, level))
        {
            string levelString = TotalLevelCrossed[worldNumber];
            levelString += "," + level;
            TotalLevelCrossed[worldNumber] = levelString;
        }
    }

    public int GetHint()
    {
        return CUtils.GetInt("hint", 0);
    }

    public void LoadData()
    {
        CurrentLevel = CUtils.GetInt("level", 1);
    }

    public void UnLockedLevelForWorld(int world)
    {
        LEVELUNLOCKED[world] = 1;
    }

    public void SaveData(string code, int value)
    {
        CUtils.SetInt(code, value);
    }

    [Serializable]
    public class PlayerDataObj
    {
        public Dictionary<int, string> levelcross;
        public Dictionary<int, int> currentLevel;
        public int totalhints;
    }
}