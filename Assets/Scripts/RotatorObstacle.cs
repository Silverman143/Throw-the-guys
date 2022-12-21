using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotatorObstacle : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 5f;
    [SerializeField] private bool _auto = false;

    private Quaternion _targetRotation;

    public UnityEvent OnActivate;

    private void Start()
    {
        _targetRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (_auto)
        {
            transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
        }
        else
        {
            Rotor();
        }
        
    }

    private void Rotor()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotSpeed*Time.deltaTime);
    }

    public void Interact()
    {
        if (!_auto)
        {
            OnActivate.Invoke();
            _targetRotation *= Quaternion.Euler(0, 90, 0);
            Debug.Log($"Intract with rot = {_targetRotation}");
        }
    }

}
