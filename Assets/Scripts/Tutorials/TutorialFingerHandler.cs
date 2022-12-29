using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFingerHandler : MonoBehaviour
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private Animator _animator;

    private bool _firstComplete = false;
    private int _currentLevel = 0;


    private void Awake()
    {
        FindObjectOfType<InputHandler>().OnInputStart.AddListener(Deactivate);
    }

    void Start()
    {
        _currentLevel = DataHandler.CurrentLevel();
        Activate();
    }


    private void Activate()
    {
        if (_currentLevel == 0)
        {
            _finger.SetActive(true);
            _animator.enabled = true;
        }
        else
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        Debug.Log("_______________________________");
        _animator.enabled = false;
        _finger.SetActive(false);
        gameObject.SetActive(false);
    }

}
