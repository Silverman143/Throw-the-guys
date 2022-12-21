using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CharacterStatus
{
    Stay, Move, Attraction
}

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody _spine0;
    [SerializeField] private Rigidbody _spine1;
    [Range(-5, 15)]
    [SerializeField] private float _force;
    [SerializeField] private Vector3 _reductionTarget;
    [SerializeField] private ParticleSystem _dust;
  
    private bool _isMoving = false;

    private Vector3 _movingDirection;

    private Transform _target;
    private float _portalAcceleration = 150f;
    private CharacterStatus _status;
    private CharacterController _characterController;

    public delegate void PortalReached();
    public static event PortalReached OnPortalReached;





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
        _characterController = transform.parent.parent.GetComponent<CharacterController>();
        _movingDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Move();
        }
        Rotate();

        switch (_status)
        {
            case CharacterStatus.Stay:
                break;
            case CharacterStatus.Move:
                Move();
                break;
            case CharacterStatus.Attraction:
                PortalAttraction();
                break;
        }
            
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

    private void PortalAttraction()
    {
        Vector3 moveVector = _target.position - transform.position;

        _rigidbody.velocity = moveVector * _portalAcceleration * Time.deltaTime;
        if (Reduction())
        {
            Deactivate();
            _characterController.Deactivate(true);
            
        }
    }

    private bool Reduction()
    {
        if (transform.localScale != _reductionTarget)
        {
            transform.localScale = Vector3.Slerp(transform.localScale, _reductionTarget, 0.25f);
            return false;
        }
        else return true;
        
    }

    private void Activate(Vector3 dircetion)
    {
        if (!_characterController.IsActiveCharacter()) return;
        _movingDirection = dircetion;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        _isMoving = true;
        _status = CharacterStatus.Move;
        _rigidbody.isKinematic = false;
        Debug.Log("Doll activate");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(_movingDirection, collision.contacts[0].normal);
        _movingDirection = newDirection.normalized;
        _dust.transform.position = collision.contacts[0].point;
        _dust.Play();
        Vibration.Hit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PortalTrigger>(out PortalTrigger portal) && _status != CharacterStatus.Attraction)
        {
            _target = portal.transform;
            _status = CharacterStatus.Attraction;
            OnPortalReached();
        }
        if (other.TryGetComponent<SpikesTrigger>(out SpikesTrigger spike) && _status == CharacterStatus.Move)
        {
            _rigidbody.isKinematic = true;

            _characterController.Deactivate(false);
            _characterController.SetParrent(spike.transform);
            Debug.Log("Spike hit");
        }
    }

    public void Deactivate()
    {
        _status = CharacterStatus.Stay;
        _isMoving = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.None;
        //_spine0.constraints = RigidbodyConstraints.None;
        //_spine1.constraints = RigidbodyConstraints.None;
    }

}
