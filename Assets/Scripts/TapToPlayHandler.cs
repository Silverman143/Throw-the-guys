using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;


    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Ended)
            {
                _inputHandler.Activate();
                gameObject.SetActive(false);
            }
        }
    }
}
