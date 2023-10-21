using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{

    private PlayerInput _playerInput;


    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    private InputAction _touchMoveAction;

    private GameObject _selectedObject = null;
    private Vector3 _selectedHitPosition;

    [SerializeField] float touchRaycastDistance = 30f;
    Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask walkableLayerMask;
    int bitLayer;


    public static TouchManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _camera = Camera.main;

        bitLayer = (1 << 7) & (1 << 8);

        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];
        _touchMoveAction = _playerInput.actions["TouchMove"];
    }
    private void OnEnable()
    {
        _playerInput.actions.Enable();
        _touchPressAction.performed += TouchPressed;
        
        //_touchPositionAction.performed += TouchPosition;
    }
    private void OnDisable()
    {
        _playerInput.actions.Disable();
        _touchPressAction.performed -= TouchPressed;
        //_touchPositionAction.performed -= TouchPosition;

    }

    private void TouchPosition(InputAction.CallbackContext context)
    {
        Debug.Log("TouchPosition");
    }

    
    private void TouchPressed(InputAction.CallbackContext context)
    {
        
        Ray ray = _camera.ScreenPointToRay(_touchPositionAction.ReadValue<Vector2>());
        RaycastHit hit;
        //layermask with bitwise somehow makes it inconsistent "~layerMask"
        if (Physics.Raycast(ray, out hit, touchRaycastDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(_camera.transform.position, hit.point, Color.blue, 1f);
            
            if (hit.collider.TryGetComponent<Target>(out Target interactable))
            {
                _selectedObject = hit.transform.gameObject;
                
                interactable.TargetInteract();
                if (interactable.currentTarget.ToString() == "PLAYER")
                {
                    StartCoroutine(ChangeSubscription());
                    
                }
            }
        }
    }

    private void TouchMovePlayer(InputAction.CallbackContext obj)
    {
        Ray ray = _camera.ScreenPointToRay(_touchPositionAction.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, touchRaycastDistance, walkableLayerMask, QueryTriggerInteraction.Ignore))
        {
            NavMeshAgent t = _selectedObject.GetComponent<NavMeshAgent>();
            t.SetDestination(hit.point);
            _touchMoveAction.performed -= TouchMovePlayer;
            _touchPressAction.performed += TouchPressed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    

    IEnumerator ChangeSubscription()
    {
        yield return new WaitForEndOfFrame();
        _touchPressAction.performed -= TouchPressed;
        _touchMoveAction.performed += TouchMovePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        _touchPressAction.WasPerformedThisFrame();
    }
}
