using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBodyPart : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private ConfigurableJoint _joint;

    private void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
    }

    private void FixedUpdate()
    {
        _joint.targetRotation = Quaternion.Inverse(_target.localRotation);
    }
}
