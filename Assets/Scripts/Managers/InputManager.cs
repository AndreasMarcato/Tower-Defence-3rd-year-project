using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    

    //public delegate void TouchPosition(Vector3 position, GameObject raycastHitGameObject);
    //public event TouchPosition OnTouchPosition;

    
    [SerializeField] float touchRaycastDistance = 30f;
    Camera _camera;
    [SerializeField] LayerMask layerMask;


    //Target Selection

    public static InputManager Instance { get; private set; }
  
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

    }

    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Vector3 touchPosition = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, touchRaycastDistance));
        //        OnTouchPosition?.Invoke(touchPosition, null);
        //    }
        //}
        
        
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 touch = Input.mousePosition;
            Vector3 touchPosition = _camera.ScreenToWorldPoint(new Vector3(touch.x, touch.y, touchRaycastDistance));
            RaycastHit hit;
            
            Debug.DrawLine(_camera.transform.position, touchPosition, Color.blue, 4f);
            if (!Physics.Raycast(_camera.transform.position, touchPosition, out hit, Mathf.Infinity, ~layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Did not Hit");
                return;
            }
            else
            {
              hit.transform.gameObject.GetComponent<Target>().TargetInteract();
                //OnTouchPosition?.Invoke(touchPosition, hit.transform.gameObject);

            }
        }
    }

    private void Start()
    {
       // InputManager.Instance.OnTouchPosition += InputManager_OnTouchPosition;
    }

    
    private void InputManager_OnTouchPosition(Vector3 screenPosition, GameObject raycastHitGameObject)
    {
        Debug.Log(screenPosition + "InputManager" + raycastHitGameObject.name);

    }

}
