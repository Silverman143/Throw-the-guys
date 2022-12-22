using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHandler 
{
    public static void LevelUpload()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
    }

    public static int CurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    public static void AddLevelAttempt(int level)
    {
        string qry = "level_attempts_" + level;

        if (!PlayerPrefs.HasKey(qry))
        {
            PlayerPrefs.SetInt(qry, 1);
        }
        else
        {
            int was = PlayerPrefs.GetInt(qry);
            PlayerPrefs.SetInt(qry, was++);
        }
    }

    public static int GetCurrentLevelAttempt()
    {
        string qry = "level_attempts_" + PlayerPrefs.GetInt("CurrentLevel");

        return PlayerPrefs.GetInt(qry);
    }

    public static int GetPrevousLevelAttempt()
    {
        string qry = "level_attempts_" + (PlayerPrefs.GetInt("CurrentLevel")-1);

        return PlayerPrefs.GetInt(qry);
    }
}
