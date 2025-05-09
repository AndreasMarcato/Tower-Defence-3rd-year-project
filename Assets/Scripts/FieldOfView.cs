using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FieldOfView : MonoBehaviour
{
    public bool isSentinel = false;
    public float radius;
    [Range(0f, 360f)] public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private AgentLogic _agentLogic;
    

    public event EventHandler OnPlayerSpotted;


    // Start is called before the first frame update
    void Start()
    {
        if(!isSentinel)
            _agentLogic = GetComponent<AgentLogic>();
        
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }


    private IEnumerator FOVRoutine()
    {
        
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }


    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0 ) 
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    if (isSentinel)
                        OnPlayerSpotted?.Invoke(this, EventArgs.Empty);
                    else
                        return;   
                    // _agentLogic.PlayerSpotted();

                }
                else
                    canSeePlayer = false;

            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;

    }


}
