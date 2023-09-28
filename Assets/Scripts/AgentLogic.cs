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

    //state machine materials
    [SerializeField] private Material[] materialStates = new Material[2];
    MeshRenderer _materialReference;

    
    //States

    private enum States
    {
        IDLE,
        ALERTED
    }

    private States NEXT_STATE = States.IDLE;
    private States CURRENT_STATE = States.IDLE;

    //ATTACK STUFF
    private GameObject _projectile;

    private void Awake()
    {
        _data = GetComponent<TurretData>();
        _materialReference = _visual.GetComponent<MeshRenderer>();

        CURRENT_STATE = States.IDLE;
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


        UnitReset();

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
        _materialReference.material = this.materialStates[0];
        CURRENT_STATE = States.IDLE;
    }
    
    private void StateToAlerted()
    {
        _materialReference.material = this.materialStates[1];
        CURRENT_STATE = States.ALERTED;
    }
    

    #endregion


    #region State Machines
    private void SM_IDLE()
    {
        Debug.Log("Idling...");

        StartCoroutine(SM_IDLE_UPDATE());
    }
    protected IEnumerator SM_IDLE_UPDATE()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 2f));
        throw new NotImplementedException("Idle Logic not implemented Yet");
    }


    private void SM_ALERTED()
    {
        
        //Is there priority target?
            //YES:
                //Attack (Priority Target)  
            //NO
                //Check which one is closest

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
            StateToAlerted();
            _unitsInRange.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        else if(_unitsInRange.Count != 0)
            _unitsInRange.Remove(other.transform);
        else
            StateToIdle();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        Transform target;
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
                target = T;
                Debug.Log(T);
                Debug.Log(closestDistance);
            }
        }

    }

    #endregion


}
