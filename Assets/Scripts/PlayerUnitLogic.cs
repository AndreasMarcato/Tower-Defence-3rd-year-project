using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerUnitLogic : MonoBehaviour
{
    //Visuals References
    [SerializeField] private Transform _hinge;

    //SATS
    private TurretData DATA;

    //ATTACK STUFF
    private Transform _target;
    private float _time = 0;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private LayerMask enemyLayer;

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

        CURRENT_STATE = States.ALERTED;
        NEXT_STATE = States.IDLE;
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 1f, 0.5f);

    }


    private void OnEnable()
    {
    }


    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
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
        
        CURRENT_STATE = States.IDLE;
    }

    private void StateToAlerted()
    {
        CURRENT_STATE = States.ALERTED;
    }


    #endregion


    #region State Machines
    private void SM_IDLE()
    {
        _hinge.eulerAngles = Vector3.zero;
        // StartCoroutine(SM_IDLE_UPDATE());
    }
    protected IEnumerator SM_IDLE_UPDATE()
    {

        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 2f));
        throw new NotImplementedException("Idle Logic not implemented Yet");
    }


    private void SM_ALERTED()
    {
        //Are there enemy units?
        if (_target == null)
        {
            _hinge.LookAt(null, Vector3.zero);
            NEXT_STATE = States.IDLE;
            return;
        }
        else
        {

            _hinge.LookAt(_target, Vector3.up);
            //Attack(Closest Unit)
            if (_time >= DATA.AttackSpeed)
            {
                Attack(_spawnPoint.position, _target.position, DATA.ProjectilePrefab, DATA.ProjectileSpawnParticle, DATA.ProjectileHitParticle, DATA.AttackPower);
                _time = 0;
            }

        }


    }
    #endregion

    public void Attack(Vector3 projectileSpawnPoint, Vector3 projectileTarget, GameObject projectilePrefab, GameObject projectileSpawnParticle, GameObject projectileHitParticle, float projectileAttackPower)
    {
        Instantiate(projectilePrefab, projectileSpawnPoint, _hinge.transform.rotation, null);
        Debug.Log("ProjectileCalled");

    }

    void UpdateTarget()
    {
        Debug.Log("Updating Target...");
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;


        foreach (GameObject target in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            //Check which one is the closest
            if (distanceToEnemy < shortestDistance)
            {
                RaycastHit hitData;
                Ray ray = new Ray(_hinge.transform.position, target.transform.position);
                Debug.DrawRay(ray.origin, ray.direction * DATA.AttackRange);

                //Check if the vision is obstructed
                if (Physics.Raycast(ray, out hitData, DATA.AttackRange, enemyLayer))
                    if (hitData.transform.tag != "Enemy")
                    {
                        //Vision Obstructed
                        _target = null;
                        return;
                    }
                   

                shortestDistance = distanceToEnemy;
                nearestTarget = target;
            }
        }

        if (nearestTarget != null && shortestDistance <= DATA.AttackRange)
        {
            _target = nearestTarget.transform;
            //enemy in range
        }
        else
            _target = null;

    }

    
}