using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private TextMeshProUGUI _completeLevelText;

    [SerializeField] private GameObject _levelFailedPanel;
    [SerializeField] private TextMeshProUGUI _failedLevelText;

    [SerializeField] private GameObject _inGameCharactersCounter;
    [SerializeField] private GameObject _inGameLevelCounter;
    [SerializeField] private TextMeshProUGUI _levelCounterInGame;

    private void Awake()
    {
        _levelCounterInGame.text = "level " + DataHandler.CurrentLevel();
    }

    public void ShowLevelComplete()
    {
        _inGameLevelCounter.SetActive(false);
        _inGameLevelCounter.SetActive(false);
        _levelCompletePanel.SetActive(true);
        _completeLevelText.text = "LEVEL " + DataHandler.CurrentLevel();
        DataHandler.LevelUpload();

    }

    public void ShowLevelFailed()
    {
        _inGameLevelCounter.SetActive(false);
        _inGameLevelCounter.SetActive(false);
        _levelFailedPanel.SetActive(true);
        _failedLevelText.text = "LEVEL " + DataHandler.CurrentLevel();
    }
}
