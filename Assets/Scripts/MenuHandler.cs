using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _levelFailedPanel;
    [SerializeField] private GameObject _inGameCharactersCounter;
    [SerializeField] private GameObject _inGameLevelCounter;

    public void ShowLevelComplete()
    {
        _inGameLevelCounter.SetActive(false);
        _inGameLevelCounter.SetActive(false);
        _levelCompletePanel.SetActive(true);

    }

    public void ShowLevelFailed()
    {
        _inGameLevelCounter.SetActive(false);
        _inGameLevelCounter.SetActive(false);
        _levelFailedPanel.SetActive(true);
    }
}
