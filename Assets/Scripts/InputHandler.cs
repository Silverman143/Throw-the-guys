using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    public Vector3 direction;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _activeCharacter;
    [SerializeField] private LayerMask IgnoreLayers;

    [SerializeField] private bool _isActive = false;
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _onInputField = false;

    public delegate void InputGet(Vector3 direction);
    public static event InputGet OnInputGetted;

    public UnityEvent OnInputStart;

    private RotatorObstacle _activeRotor;
    private bool _blocked = false;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (!_isGameOver && _isActive && (touch.phase == TouchPhase.Began | touch.phase == TouchPhase.Moved | touch.phase == TouchPhase.Stationary))
            {
                OnInputStart.Invoke();

                Vector3[] points = new Vector3[2];
                _lineRenderer.enabled = true;
                _lineRenderer.SetPositions(points);

                Vector2 touchPos = Input.GetTouch(0).position;

                // The ray to the touched object in the world
                Ray ray = Camera.main.ScreenPointToRay(touchPos);

                // Your raycast handling
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, IgnoreLayers))
                {
                    if (hit.transform.TryGetComponent<InputHandler>(out InputHandler handler) && !_blocked)
                    {
                        _onInputField = true;
                        //_lineRenderer.enabled = true;
                        //_lineRenderer.SetPositions(points);
                        points[0] = hit.point;
                        points[1] = _activeCharacter.position;
                        direction = points[1] - points[0];
                        _lineRenderer.SetPositions(points);
                    }
                    else
                    {
                        _onInputField = false;
                        //direction = Vector3.zero;
                        //_lineRenderer.enabled = false;
                    }

                    if (hit.transform.TryGetComponent<RotatorObstacle>(out RotatorObstacle rotator))
                    {
                        _activeRotor = rotator;
                    }
                    else if (_activeRotor != null)
                    {
                        _activeRotor = null;
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (_onInputField && _isActive)
                {
                    _isActive = false;
                    OnInputGetted(direction.normalized);
                    direction = Vector3.zero;
                    _lineRenderer.enabled = false;
                }
                if (_activeRotor != null)
                {
                    _activeRotor.Interact();
                    _activeRotor = null;
                }

            }
        }
    }

    public void SetNewCharacter(Transform character)
    {
        _activeCharacter = character;
        _isActive = true;
    }

    public void Activate()
    {
        StartCoroutine(ActivateNextFrame());
    }
    public void Deactivate()
    {
        _isActive = false;
    }

    public void SetGameOver()
    {
        _isGameOver = true;
    }

    public void SetBlock(bool value)
    {
        _blocked = value;
    }


    IEnumerator ActivateNextFrame()
    {
        yield return new WaitForEndOfFrame();
        _isActive = true;
    }


}