using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentLogic : MonoBehaviour, IE_Turret
{
    //SATS
    private TurretData DATA;
    
    //FOV
    //private FieldOfView _sentinelLogicReference;
    

    //Visual reference for animations
    [SerializeField] GameObject _visual;
    [SerializeField] Transform _hinge;

    //state machine materials
    [SerializeField] private Material[] materialStates = new Material[2];
    MeshRenderer _materialReference;
    
    
    //ATTACK STUFF
    private Transform _target;
    private float _time = 0;
    [SerializeField] private Transform _spawnPoint;

    
    //private bool isPriorityTarget = false;
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
        DATA = GetComponent<TurretData>();
        _materialReference = _visual.GetComponent<MeshRenderer>();

        CURRENT_STATE = States.ALERTED;
        NEXT_STATE = States.IDLE;

        //_sentinelLogicReference = GameObject.Find("Logic").GetComponent<FieldOfView>();
        
    }

    

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 1f, 0.5f);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_target == null)
        {
            NEXT_STATE = States.IDLE;
        }
        else
            NEXT_STATE = States.ALERTED;

        switch (CURRENT_STATE)
        {
            case States.IDLE:
                SM_IDLE();
                break;
            case States.ALERTED:
                SM_ALERTED();
                break;
        }

        Debug.Log(CURRENT_STATE);

    }

    void UpdateTarget() 
    {
        Debug.Log("Updating Target...");
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;


        foreach (GameObject target in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null && shortestDistance <= DATA.AttackRange)
        {
            _target = nearestTarget.transform;
            //PLayer in range
        }
        else
            _target = null;

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
        //Are there player units?
        if (_target == null)
        {
            _hinge.LookAt(null, Vector3.zero);
            NEXT_STATE = States.IDLE;
            return;
        }
        else
        {

            _hinge.LookAt(_target,Vector3.up);
            //Attack(Closest Unit)
            if (_time >= DATA.AttackSpeed)
            {
                Attack(_spawnPoint.position, _target.position, DATA.ProjectilePrefab, DATA.ProjectileSpawnParticle, DATA.ProjectileHitParticle, DATA.AttackPower);
                _time = 0;
            }

        }


    }



    public void Attack(Vector3 projectileSpawnPoint, Vector3 projectileTarget, GameObject projectilePrefab, GameObject projectileSpawnParticle, GameObject projectileHitParticle, float projectileAttackPower)
    {
        Instantiate(projectilePrefab, projectileSpawnPoint, _hinge.transform.rotation, null);
        Debug.Log("ProjectileCalled");
            
    }
    

    public void DealDamage()
    {
        Debug.Log("DamageDealt");
    }

    public void TakeDamage()
    {
        Debug.Log("DamageTaken");
    }

    #endregion


}
