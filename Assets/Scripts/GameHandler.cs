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

    private static bool _gameStarts = false;

    public bool LevelComplete = false;
    public static bool _restart = false;
    public int _activeLevel = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _menuHandler = FindObjectOfType<MenuHandler>();

        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }

        LoadLevel();

        
    }

    private void Start()
    {
        if (!_gameStarts)
        {
            AnaliticsHandler.GameStart();
            _gameStarts = true;
        }
        DataHandler.AddLevelAttempt(DataHandler.CurrentLevel());
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
        int currentLevel = DataHandler.CurrentLevel();
        if (_winCharacters >= 3)
        {
            LevelComplete = true;
            _menuHandler.ShowLevelComplete();
            if(_activeLevel == currentLevel)
            {
                DataHandler.LevelUpload();
            }
            AnaliticsHandler.LevelComplete(currentLevel, _winCharacters);
            _confetti.SetActive(true);
        }
        else
        {
            AnaliticsHandler.LevelFailed(currentLevel);
            _menuHandler.ShowLevelFailed();
        }
    }

    private void AddWinCharacter()
    {
        _winCharacters++;
    }

    public void SetRestart()
    {
        _restart = true;
    }

    private void LoadLevel()
    {
        int level = DataHandler.CurrentLevel();
        if (_restart)
        {
            level--;
            _restart = false;
        }
        _activeLevel = level;

        if(level < _levels.Length)
        {
            _levels[level].SetActive(true);
        }
        else
        {
            _levels[Random.RandomRange(4, _levels.Length - 1)].SetActive(true);
            Debug.Log("Random Level");
        }

        
    }
}
