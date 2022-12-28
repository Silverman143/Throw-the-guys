using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _obj;
    [SerializeField] private Transform[] _points;
    [SerializeField] private int _targetPointNumver = 0;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private bool _moveX = false;
    [SerializeField] private bool _moveY = false;
    [SerializeField] private bool _moveZ = false;


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = _obj.position;

        if (_moveX)
        {
            targetPos.x = _points[_targetPointNumver].position.x;
        }

        if (_moveZ)
        {
            targetPos.z = _points[_targetPointNumver].position.z;
        }

        _obj.position = Vector3.MoveTowards(_obj.position, targetPos, _speed);

        if (Vector3.Distance(_obj.position, targetPos)<_speed)
        {
            _targetPointNumver++;
            if (_points.Length-1 < _targetPointNumver)
            {
                _targetPointNumver = 0;
            }
        }
    }
}
