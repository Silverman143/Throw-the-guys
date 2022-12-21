using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;


    private void Awake()
    {
        if (DataHandler.CurrentLevel() == 0 | DataHandler.CurrentLevel() == 7)
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
        gameObject.SetActive(false);
    }
}
