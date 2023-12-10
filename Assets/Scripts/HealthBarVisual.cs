using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisual : MonoBehaviour
{
    [SerializeField] private GameObject Canvas_health;
    [SerializeField] private Slider fillValue;
    [SerializeField] private Health healthGO;

  
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
        float valueHP = Mathf.InverseLerp(0, healthGO.maxHealth, healthGO.health);
        fillValue.value = valueHP;
    

    }

}
