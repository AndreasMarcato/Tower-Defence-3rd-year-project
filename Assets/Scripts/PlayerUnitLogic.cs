using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnitLogic : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    private NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void ScreenTapped(object sender, System.EventArgs e)
    {
        _agent.SetDestination(Vector3.zero);
    
    }


    private void OnEnable()
    {
        _inputManager.OnScreenTap += ScreenTapped;

    }


    private void OnDisable()
    {
        _inputManager.OnScreenTap -= ScreenTapped;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
