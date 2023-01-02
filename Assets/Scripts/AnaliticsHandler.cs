using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using GameAnalyticsSDK;

//using LionStudios.Suite.Analytics;

public static class AnaliticsHandler 
{
    public static void GameStart()
    {
        //LionAnalytics.GameStart();

        int count = 0 ;
        string qry = "Game_starts";

        if (!PlayerPrefs.HasKey(qry))
        {

            PlayerPrefs.SetInt(qry, 1);
            count = 1;
        }
        else
        {
            int num = PlayerPrefs.GetInt(qry);
            num++;
            count = num;
        }

        int days = GetDaysSinceReg();

        string daysQ = "Day: " + days;
        string countQ = "Count: " + count;


        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game_start", countQ, daysQ);

        //Yandex
        string eventParameters = $"\"Event_type\":\"GameStart\", \"count\":\"{count}\", \"Days\":\"{GetDaysSinceReg()}\"";
        eventParameters = "{" + eventParameters + "}";

        AppMetrica.Instance.ReportEvent(eventParameters);
    }

    public static void LevelStart(int level)
    {
        int attempt = DataHandler.GetCurrentLevelAttempt();

        //LionAnalytics.LevelStart(levelNum: level, attemptNum: attempt);


        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "attemptNum", attempt)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
                                   AchievementParameters);

        //GA
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level_"+level, GetDaysSinceReg());
        //Yandex
        string eventParameters = $"\"Event_type\":\"LevelStart\", \"level\":\"{level}\", \"Days\":\"{GetDaysSinceReg()}\"";
        eventParameters = "{" + eventParameters + "}";

        AppMetrica.Instance.ReportEvent(eventParameters);
    }

    public static void LevelComplete(int level, int stars)
    {
        //LionAnalytics.LevelComplete(level: level, attemptNum: DataHandler.GetCurrentLevelAttempt(), score: stars);

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "stars", stars)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_complete",
                                   AchievementParameters);

        //GA
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level_" + level, GetDaysSinceReg());

        //Yandex
        string eventParameters = $"\"Event_type\":\"LevelComplete\", \"level\":\"{level}\", \"Days\":\"{GetDaysSinceReg()}\"";
        eventParameters = "{" + eventParameters + "}";

        AppMetrica.Instance.ReportEvent(eventParameters);
    }

    public static void LevelFailed(int level)
    {
        //LionAnalytics.LevelFail(level: level, attemptNum: DataHandler.GetCurrentLevelAttempt());

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_failed",
                                   AchievementParameters);

        //GA
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level_" + level, GetDaysSinceReg());

        //Yande
        string eventParameters = $"\"Event_type\":\"LevelFailed\", \"level\":\"{level}\", \"Days\":\"{GetDaysSinceReg()}\"";
        eventParameters = "{" + eventParameters + "}";

        AppMetrica.Instance.ReportEvent(eventParameters);
    }

    public static void LevelRestart(int level)
    {
        int attempt = DataHandler.GetCurrentLevelAttempt();

        //LionAnalytics.LevelRestart(level: level, attemptNum: attempt);

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "attemptNum", attempt)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_restart",
                                   AchievementParameters);
    }

    private static int GetDaysSinceReg()
    {
        string qry = "RegistrationDay";
        DateTime dateCurrent = DateTime.UtcNow;

        if (!PlayerPrefs.HasKey(qry))
        {

            PlayerPrefs.GetString(qry, dateCurrent.ToString());
            return 0;
        }
        else
        {
            DateTime was = DateTime.Parse(PlayerPrefs.GetString(qry));

            TimeSpan difference = dateCurrent - was;

            return difference.Days;
        }
    }
}
