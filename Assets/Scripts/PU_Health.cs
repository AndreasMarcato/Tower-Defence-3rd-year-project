using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Health : MonoBehaviour
{
    [SerializeField] float healthValue = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.GetComponent<Health>().TakeDamage(-healthValue);
    }
    
}
