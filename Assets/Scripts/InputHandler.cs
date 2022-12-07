using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    public Vector3 direction;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _activeCharacter;

    private bool _isActive = true;

    public delegate void InputGet(Vector3 direction);
    public static event InputGet OnInputGetted;

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
            Vector3[] points = new Vector3[2];
            _lineRenderer.SetPositions (points);
            Touch touch = Input.touches[0];

            Vector2 touchPos = Input.GetTouch(0).position;

            // The ray to the touched object in the world
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            // Your raycast handling
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if(hit.transform.TryGetComponent<InputHandler>(out InputHandler handler))
                {
                    
                    points[0] = hit.point;
                    points[1] = _activeCharacter.position;
                    direction = points[1] - points[0];
                    _lineRenderer.SetPositions(points);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _lineRenderer.enabled = false;
                _isActive = false;
                OnInputGetted(direction.normalized);
                direction = Vector3.zero;
            }
        }
    }
}
