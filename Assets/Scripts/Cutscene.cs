using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    private void Start()
    {
        Invoke("KillIt", 9f);
    }

    public void KillIt()
    {
        Destroy(gameObject);
    }
}
