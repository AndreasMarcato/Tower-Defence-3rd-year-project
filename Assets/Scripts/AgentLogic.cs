using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UI.Image;

public class AgentLogic : MonoBehaviour
{
    //SATS
    private TurretData _data;
    //FOV
    private FieldOfView _sentinelLogicReference;
    
    private List<Transform> _unitsInRange = new List<Transform>();

    //Visual reference
    [SerializeField] GameObject _visual;
    [SerializeField] Transform _hinge;

    //state machine materials
    [SerializeField] private Material[] materialStates = new Material[2];
    MeshRenderer _materialReference;
    
    
    //ATTACK STUFF
    private GameObject _projectile;
    private Transform _target;
    private bool isPriorityTarget = false;
    //States

    private enum States
    {
        IDLE,
        ALERTED
    }

    private States NEXT_STATE = States.IDLE;
    private States CURRENT_STATE = States.IDLE;

   
    private void Awake()
    {
        _data = GetComponent<TurretData>();
        _materialReference = _visual.GetComponent<MeshRenderer>();

        CURRENT_STATE = States.ALERTED;
        NEXT_STATE = States.IDLE;

        //_sentinelLogicReference = GameObject.Find("Logic").GetComponent<FieldOfView>();
        
    }

    private void Start()
    {

    }

    private void Update()
    {
        switch (CURRENT_STATE)
        {
            case States.IDLE:
                SM_IDLE();
                break;
            case States.ALERTED:
                SM_ALERTED();
                break;
        }


        //UnitReset();

        Debug.Log(CURRENT_STATE);

    }

   

    private void LateUpdate()
    {
        if (CURRENT_STATE != NEXT_STATE) 
        {
            switch (NEXT_STATE)
            {
                case States.IDLE:
                    StateToIdle();
                    break;
                case States.ALERTED:
                    StateToAlerted();
                    break;
            }
        }

    }

    

    #region State Machine Transitions

    private void StateToIdle()
    {
        _materialReference.material = materialStates[0];
        CURRENT_STATE = States.IDLE;
    }
    
    private void StateToAlerted()
    {
        _materialReference.material = materialStates[1];
        CURRENT_STATE = States.ALERTED;
    }
    

    #endregion


    #region State Machines
    private void SM_IDLE()
    {
        transform.eulerAngles = Vector3.zero;
       // StartCoroutine(SM_IDLE_UPDATE());
    }
    protected IEnumerator SM_IDLE_UPDATE()
    {
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 2f));
        throw new NotImplementedException("Idle Logic not implemented Yet");
    }


    private void SM_ALERTED()
    {
        switch (isPriorityTarget)
        {

            case true:
                //YES:
                //Attack (Priority Target)  

                break;

            case false:
                //NO
                //Check which one is closest

                break;
        }

        //ATTACK LOGIC
        if (_unitsInRange.Count == 0)
        {
            _hinge.LookAt(null, Vector3.zero);
            NEXT_STATE = States.IDLE;
            return;
        }


        float closestDistance = _data.AttackRange;
        float currentDistance;


        foreach (Transform T in _unitsInRange)
        {
            currentDistance = Vector3.Distance(transform.position, T.position);

            if (closestDistance < currentDistance)
                return;
            else
            {
                closestDistance = currentDistance;
                _target = T;
                Debug.Log(T);
                Debug.Log(T.name);
            }
        }
        _hinge.LookAt(_target);

        //Attack(Closest Unit)




    }

    public void UnitSighted(Transform unit)
    {
        foreach (Transform T in _unitsInRange)
        {
            if (T != unit)
                _unitsInRange.Add(unit);
            else
                return;
        }
    }
    public void UnitReset()
    {
        _unitsInRange.Clear();
    }

    public void UnitSighted()
    {
        NEXT_STATE = States.ALERTED;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        else
        {
            NEXT_STATE = States.ALERTED;
            _unitsInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        if (_unitsInRange.Contains(other.transform))
        {
            _unitsInRange.Remove(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        

    }

    #endregion


}
