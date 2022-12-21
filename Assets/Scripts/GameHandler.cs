using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _confetti;
    [SerializeField] private GameObject[] _levels;

    private MenuHandler _menuHandler;

    private int _winCharacters = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _menuHandler = FindObjectOfType<MenuHandler>();

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }

        LoadLevel();

        PlayerPrefs.SetInt("CurrentLevel", 0);
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

    private void LoadLevel()
    {
        int level = DataHandler.CurrentLevel();
        if(level < _levels.Length)
        {
            _levels[level].SetActive(true);
        }
        else
        {
            _levels[Random.RandomRange(2, _levels.Length - 1)].SetActive(true);
            Debug.Log("Random Level");
        }

        
    }
}
