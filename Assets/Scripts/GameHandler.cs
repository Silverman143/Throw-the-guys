using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _confetti;

    private MenuHandler _menuHandler;

    private int _winCharacters = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _menuHandler = FindObjectOfType<MenuHandler>();
    }

    private void OnEnable()
    {
        CharactersSwitcher.OnCharactersFinished+=LevelFinished;
        MovementController.OnPortalReached += AddWinCharacter;
    }

    private void OnDisable()
    {
        CharactersSwitcher.OnCharactersFinished -= LevelFinished;
        MovementController.OnPortalReached -= AddWinCharacter;
    }

    private void LevelFinished()
    {
        if (_winCharacters > 0)
        {
            _menuHandler.ShowLevelComplete();
            _confetti.SetActive(true);
        }
        else
        {
            _menuHandler.ShowLevelFailed();
        }
    }

    private void AddWinCharacter()
    {
        _winCharacters++;
    }
}
