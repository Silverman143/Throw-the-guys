using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TapToPlayHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    private int _currentLevel;

    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (_currentLevel == 0 | _currentLevel == 7)
        {
            Activate();
        }
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Ended)
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        _inputHandler.Activate();
        try
        {
            AnaliticsHandler.LevelStart(_currentLevel);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        gameObject.SetActive(false);
    }
}
