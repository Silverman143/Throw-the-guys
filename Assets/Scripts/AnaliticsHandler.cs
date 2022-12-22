using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LionStudios.Suite.Analytics;

public static class AnaliticsHandler 
{
    public static void GameStart()
    {
        LionAnalytics.GameStart();
    }

    public static void LevelStart(int level)
    {
        int attempt = DataHandler.GetCurrentLevelAttempt();

        LionAnalytics.LevelStart(levelNum: level, attemptNum: attempt);


        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "attemptNum", attempt)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart,
                                   AchievementParameters);
    }

    public static void LevelComplete(int level, int stars)
    {
        LionAnalytics.LevelComplete(level: level, attemptNum: DataHandler.GetCurrentLevelAttempt(), score: stars);

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "stars", stars)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventTutorialComplete,
                                   AchievementParameters);
    }

    public static void LevelFailed(int level)
    {
        LionAnalytics.LevelFail(level: level, attemptNum: DataHandler.GetCurrentLevelAttempt());

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Level_failed",
                                   AchievementParameters);
    }

    public static void LevelRestart(int level)
    {
        int attempt = DataHandler.GetCurrentLevelAttempt();

        LionAnalytics.LevelRestart(level: level, attemptNum: attempt);

        //FireBase

        Firebase.Analytics.Parameter[] AchievementParameters = {
              new Firebase.Analytics.Parameter(
                Firebase.Analytics.FirebaseAnalytics.ParameterLevel, level),
              new Firebase.Analytics.Parameter(
                "attemptNum", attempt)
            };
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Restart",
                                   AchievementParameters);
    }
}
