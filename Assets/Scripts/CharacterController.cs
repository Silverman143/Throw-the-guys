using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool _isActiveCharacter = false;
    private MovementController _movementController;
    private bool _removable = false;

    public delegate void CharacterFinished(CharacterController character);
    public static event CharacterFinished OnFinished;

    private void Awake()
    {
        _movementController = GetComponentInChildren<MovementController>();
    }

    public void SetStart(Vector3 pos, Quaternion rot)
    {
        _movementController.transform.position = pos;
        _movementController.transform.localRotation = rot;
        _isActiveCharacter = true;
    }

    public Vector3 GetPosition()
    {
        return _movementController.transform.position;
    }

    public Quaternion GetRotation()
    {
        return _movementController.transform.rotation;
    }

    public bool IsActiveCharacter() => _isActiveCharacter;

    public void Deactivate(bool remove)
    {
        _removable = remove;
        _movementController.Deactivate();
        _movementController.enabled = false;
        _isActiveCharacter = false;
        Debug.Log("Character on finished");
        OnFinished(this);
    }

    public void Remove()
    {
        if (_removable) Destroy(this.gameObject);
    }
    public Transform GetPelvisPos() => _movementController.transform;

    public void SetParrent(Transform obj)
    {
        transform.parent = obj;
    }
}
