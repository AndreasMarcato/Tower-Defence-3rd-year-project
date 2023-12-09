using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log(other.gameObject.name);
            StartCoroutine(UIManager.Instance.StartWinHandle());
        }
    }
}
