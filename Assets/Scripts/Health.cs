using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private TurretData DATA;
    public float health;
    public float maxHealth;
    public float armor;

    private ParticleSystem _hitUIFeedback;

    [SerializeField] bool isPlayer = false;

    private void Awake()
    {
        if (isPlayer)
            _hitUIFeedback = GameObject.FindGameObjectWithTag("_hitUIFeedback").GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        DATA = GetComponent<TurretData>();
        health = DATA.Health;
    }

    public void TakeDamage(float damage)
    {
        if (damage < armor)
            health -= 1;
        else
            health -= damage - armor;

        if (isPlayer)
            _hitUIFeedback.Play();

        Debug.Log(gameObject.name + health);

        if (health <= 0)
            Destroy(gameObject);
    }

}
