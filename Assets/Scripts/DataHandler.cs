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
}
