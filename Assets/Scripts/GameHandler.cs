using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _confetti;
    [SerializeField] private GameObject[] _levels;
    

    private MenuHandler _menuHandler;
    private InputHandler _inputHandler;
    private CharactersSwitcher _charactersSwitcher;
    private StarsCounter _starsCounter;

    private int _winCharacters = 0;
    private int _deadCharacters = 0;

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

        DataHandler.AddLevelAttempt(DataHandler.CurrentLevel());
        _inputHandler = FindObjectOfType<InputHandler>();
        _charactersSwitcher = FindObjectOfType<CharactersSwitcher>();
        _starsCounter = FindObjectOfType<StarsCounter>();

        LoadLevel();
    }

    private void Start()
    {
        GameAnalytics.Initialize();

        if (!_gameStarts)
        {
            AnaliticsHandler.GameStart();
            _gameStarts = true;
        }
        //DataHandler.AddLevelAttempt(DataHandler.CurrentLevel());
        //_inputHandler = FindObjectOfType<InputHandler>();
        //_charactersSwitcher = FindObjectOfType<CharactersSwitcher>();
        //_starsCounter = FindObjectOfType<StarsCounter>();
        HideCharacters();
    }

    private void OnEnable()
    {
        CharactersSwitcher.OnCharactersFinished+=LevelFinished;
        MovementController.OnPortalReached += AddWinCharacter;
        MovementController.OnCharacterDead += CountDeadCharacter;
    }

    private void OnDisable()
    {
        CharactersSwitcher.OnCharactersFinished -= LevelFinished;
        MovementController.OnPortalReached -= AddWinCharacter;
        MovementController.OnCharacterDead -= CountDeadCharacter;
    }

    private void HideCharacters()
    {
        int level = DataHandler.CurrentLevel();
        if (level < 2)
        {
            _starsCounter.HideStars();
            _charactersSwitcher.HideCharacters();
        }
    }

    private void CountDeadCharacter()
    {
        _deadCharacters++;
        if (_deadCharacters > 1)
        {
            _inputHandler.Deactivate();
            LevelFinished();
        }
    }

    private void LevelFinished()
    {
        int currentLevel = DataHandler.CurrentLevel();
        if (_winCharacters >= 3 | _activeLevel<2)
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
