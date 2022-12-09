using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject _characterPrefab;

    private void CreateCharacter()
    {
        GameObject newCharacter = Instantiate(_characterPrefab, new Vector3(0f, 0.8f, -6.5f), Quaternion.identity, null);
    }
}
