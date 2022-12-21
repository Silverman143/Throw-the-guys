using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2Handler : MonoBehaviour
{
    [SerializeField] private RotatorObstacle _rotor;

    private InputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
        _inputHandler.SetBlock(true);
        _rotor.OnActivate.AddListener(Deactivate);
    }

    private void Deactivate()
    {
        _inputHandler.SetBlock(false);
        gameObject.SetActive(false);
    }
}
