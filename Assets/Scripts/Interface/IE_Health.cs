using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IE_Health
{
    public float health { get; set; }
    public float maxHealth { get; set; }
    public float armor { get; set; }


    void TakeDamage(float damage);
}
