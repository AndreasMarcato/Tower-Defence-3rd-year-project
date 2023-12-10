using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{

    private PlayerInput _playerInput;
    private PlayerAudio _playerAudio;
    private NavMeshAgent _playerAgent;
    public Transform GetPlayerAgent() { return _playerAgent.transform; }

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    private InputAction _touchMoveAction;
    private InputAction _touchPause;
    bool isPaused = false;

    private GameObject _selectedObject = null;
    private Vector3 _selectedHitPosition;

    [SerializeField] float touchRaycastDistance = 30f;
    private Transform _cameraManager;
    private Camera _camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask walkableLayerMask;
    private Transform _destinationParticleSystem;

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
            DontDestroyOnLoad(this);
        }
        _camera = Camera.main;


        _playerInput = GetComponent<PlayerInput>();
        
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];
        _touchMoveAction = _playerInput.actions["TouchMove"];
        
        
        _touchPause = _playerInput.actions["Pause"];

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        _playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAudio>();
        _playerAgent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        _cameraManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<Transform>();
        _destinationParticleSystem = GameObject.FindGameObjectWithTag("DestinationParticleSystem").transform;

        _playerInput.actions.Enable();
        _touchPressAction.performed += TouchPressed;
        _touchPause.performed += PauseGame;

    }


    public void PauseGame(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            _touchPressAction.performed -= TouchPressed;
            UIManager.Instance.HandlePause(isPaused);
        }
        else
        {
            _touchPressAction.performed += TouchPressed;
            UIManager.Instance.HandlePause(isPaused);

        }

    }
    public void UIPauseCallback()
    {
        _touchPressAction.performed += TouchPressed;
    }

    

    private void OnDisable()
    {
        _playerInput.actions.Disable();
        _touchPressAction.performed -= TouchPressed;
        _touchPause.performed -= PauseGame;


    }


    private IEnumerator WaitPass() 
    {
        yield return new WaitForEndOfFrame();
    }
    
    private void TouchPressed(InputAction.CallbackContext context)
    {
        if (isPaused)
            return;
        foreach (var touch in Input.touches)
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
        //Create the Ray to cast to the Tap position
        Ray ray = _camera.ScreenPointToRay(_touchPositionAction.ReadValue<Vector2>());
        RaycastHit hit;

        //Check if it hit something
        if (Physics.Raycast(ray, out hit, touchRaycastDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(_camera.transform.position, hit.point, Color.blue, 1f);

            if (hit.collider.TryGetComponent<Target>(out Target interactable))
            {
                _selectedObject = hit.transform.gameObject;

                interactable.TargetInteract();

                //If the tap found the Player, do this:
                if (interactable.currentTarget.ToString() == "PLAYER")
                {
                    //StartCoroutine(ChangeSubscription());
                }
            }
            else if (hit.collider.TryGetComponent<PickUpLogic>(out PickUpLogic pickUp))
            {
                _selectedObject = hit.transform.gameObject;
                if (pickUp.CURRENT_TYPE == PickUpLogic.PICKUP_TYPE.HEAL)
                    pickUp.HealPlayer();
                else if(pickUp.CURRENT_TYPE == PickUpLogic.PICKUP_TYPE.CURRENCY)
                    pickUp.AddCurrency();
                return;
            }
            else
            {
                _playerAudio.PlayerMoveAudio();
                _destinationParticleSystem.position = hit.point;
                _destinationParticleSystem.GetComponent<ParticleSystem>().Play();
                _playerAgent.SetDestination(hit.point);
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
            //_touchMoveAction.performed -= TouchMovePlayer;
            //_touchPressAction.performed += TouchPressed;
        }
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
