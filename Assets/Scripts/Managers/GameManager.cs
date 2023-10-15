using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

    //SELECTION HANDLER
    private enum SelectedObject
    {
        PLAYER_UNIT,
        ENEMY_UNIT,
        BUILDING

    }
    private SelectedObject SELECTION_CURRENT;
    private SelectedObject SELECTION_NEXT;
    private Camera _camera;

    //Input Events Reference
    private InputManager inputManager;
    private Vector3 storedVector;
    //event raycast layermask
    private LayerMask GROUND = 9;

    //private void OnEnable()
    //{
    //    inputManager.OnTouchPosition += InputManager_OnTouchPosition;

    //}
    //private void OnDisable()
    //{
    //    inputManager.OnTouchPosition -= InputManager_OnTouchPosition;

    //}

    public static GameManager Instance { get; private set; }

    
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
    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void InputManager_OnTouchPosition(Vector3 position, GameObject raycastHitGameObject)
    {
        Debug.Log(raycastHitGameObject.name);
    }

    

    private void Update()
    {

        //switch (SELECTION_CURRENT)
        //{
        //    case SelectedObject.PLAYER_UNIT:
        //        //UI UPDATE
        //        return;
        //    case SelectedObject.ENEMY_UNIT:
        //        //UI UPDATE
        //        return;
        //    case SelectedObject.BUILDING:
        //        //UI UPDATE
        //        return;
        //}

    }
    private void LateUpdate()
    {
        //if (SELECTION_CURRENT != SELECTION_NEXT)
        //{
        //    switch (SELECTION_NEXT)
        //    {
        //        case SelectedObject.PLAYER_UNIT:
        //            if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.BUILDING) return;
        //            return;
        //        case SelectedObject.ENEMY_UNIT:
        //            if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.BUILDING) return;
        //            return;
        //        case SelectedObject.BUILDING:
        //            if (SELECTION_CURRENT == SelectedObject.PLAYER_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.ENEMY_UNIT) return;
        //            else if (SELECTION_CURRENT == SelectedObject.BUILDING) return;
        //            return;
        //    }

        //}
    }



    #region State Transitions
    private void StateToPlayer_Unit()
    {

    }
    private void StatetoEnemy_Unit()
    {

    }
    private void StateToBuilding_Unit()
    {

    }
    #endregion


    #region State Machines
    private void SM_PLAYER_UNIT()
    {

    }
    private void SM_ENEMY_UNIT()
    {

    }
    private void SM_BUILDING_UNIT()
    {

    }
    #endregion


}
