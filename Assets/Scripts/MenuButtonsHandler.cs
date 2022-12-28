using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsHandler : MonoBehaviour
{
    private GameHandler _gameHandler;
    void Start()
    {
        _gameHandler = FindObjectOfType<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButton()
    {
        int currentLevel = DataHandler.CurrentLevel();
        if (_gameHandler.LevelComplete)
        {
            AnaliticsHandler.LevelRestart(currentLevel-1);
            _gameHandler.SetRestart();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            AnaliticsHandler.LevelRestart(DataHandler.CurrentLevel());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
