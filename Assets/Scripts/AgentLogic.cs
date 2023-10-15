using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentLogic : MonoBehaviour, IE_Turret
{
    //SATS
    private TurretData DATA;
    
    //FOV
   // private FieldOfView _sentinelLogicReference;
    
    //List of Player Units in Range
    private List<Transform> _unitsInRange = new List<Transform>();

    //Visual reference for animations
    [SerializeField] GameObject _visual;
    [SerializeField] Transform _hinge;

    //state machine materials
    [SerializeField] private Material[] materialStates = new Material[2];
    MeshRenderer _materialReference;
    
    
    //ATTACK STUFF
    private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<GameObject> _instantiatedBullets;
    private enum IsActiveBullet
    {
        YES,
        NO
    }
    private IsActiveBullet _isActiveBullet;
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
        _instantiatedBullets = new List<GameObject>();
        DATA = GetComponent<TurretData>();
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
        //Are there player nits?
        if (_unitsInRange.Count == 0)
        {
            _hinge.LookAt(null, Vector3.zero);
            NEXT_STATE = States.IDLE;
            return;
        }

        //yes, there are player units, calculate which is the closest and if there is a priority one to attack
        float closestDistance = DATA.AttackRange;
        float currentDistance;


        foreach (Transform T in _unitsInRange)
        {
            //if (T.GetComponent<somecomponent>().IsPriorityTarget())
            //_target = T;
            //break;

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
        Attack(_hinge.position, _target.position, DATA.ProjectilePrefab, DATA.ProjectileSpawnParticle, DATA.ProjectileHitParticle, DATA.AttackPower);



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

    public void Attack(Vector3 projectileSpawnPoint, Vector3 projectileTarget, GameObject projectilePrefab, GameObject projectileSpawnParticle, GameObject projectileHitParticle, float projectileAttackPower)
    {
        GameObject tempProjectile;
        tempProjectile = projectilePrefab;
        float speed = 2f;
        foreach (GameObject obj in _instantiatedBullets)
        {
            if (!obj.activeSelf)
            {
                tempProjectile = obj;
                _isActiveBullet = IsActiveBullet.NO;
                break;
            }
            else
            {
                //Make a new bullet and add it to the list
                //set the bullet as temp
                _isActiveBullet = IsActiveBullet.YES;
                break;
            }
        }
        
        Rigidbody rb = tempProjectile.GetComponent<Rigidbody>();
        Vector3 force;
        force = _target.position;

       // Instantiate(tempProjectile, projectileSpawnPoint, transform.rotation, _spawnPoint);
        rb.AddForce(force * speed, ForceMode.Impulse);
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
