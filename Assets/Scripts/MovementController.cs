using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody _spineRigidBody;
    [Range(-5, 15)]
    [SerializeField] private float _force;

    private bool _isMoving = false;
    private Vector3 _movingDirection;

    private void OnEnable()
    {
        InputHandler.OnInputGetted += Activate;
    }

    private void OnDisable()
    {
        InputHandler.OnInputGetted -= Activate;
    }

    private void Start()
    {
        _movingDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }
        Rotate();
    }

    private void Rotate()
    {
        if (!_isMoving)
        {
            transform.LookAt(InputHandler.Instance.direction);
        }
    }

    private void Move()
    {
        _rigidbody.velocity = _movingDirection * _force;
    }

    private void Activate(Vector3 dircetion)
    {
        _movingDirection = dircetion;
        _spineRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
        _isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(_movingDirection, collision.contacts[0].normal);
        _movingDirection = newDirection.normalized;
    }
}
