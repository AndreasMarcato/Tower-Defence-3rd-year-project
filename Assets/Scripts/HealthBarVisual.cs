using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarVisual : MonoBehaviour
{
    [SerializeField] private GameObject Canvas_health;
    [SerializeField] private Slider fillValue;
    [SerializeField] private Health healthGO;
    private float currentProgress;
    private void Update()
    {
       
        float valueHP = Mathf.InverseLerp(healthGO.health, healthGO.maxHealth, currentProgress);

        fillValue.value = valueHP;
    

    }

}
