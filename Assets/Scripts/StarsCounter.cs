using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCounter : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    private int _counter = 0;

    private void OnEnable()
    {
        MovementController.OnPortalReached += AddStar;
    }

    private void OnDisable()
    {
        MovementController.OnPortalReached -= AddStar;
    }

    private void AddStar()
    {
        _images[_counter].color = Color.white;
        _counter++;
    }

    public void HideStars()
    {
        _images[0].gameObject.SetActive(false);
        _images[_images.Length - 1].gameObject.SetActive(false);
        _images = new Image[] { _images[1], _images[2] };
    }
}
