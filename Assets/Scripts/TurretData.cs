using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    [SerializeField] private SO_TurretData data;
    private float _attackRange;
    private float _attackPower;
    private float _attackSpeed;
    private float _health;
    private float _maxHealth;
    private GameObject _projectile;
    private GameObject _spawnParticle;
    private GameObject _hitParticle;
    private GameObject _dieParticle;
    private GameObject _damagedParticle;

    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    public float AttackPower { get => _attackPower; set => _attackPower = value; }
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public GameObject ProjectilePrefab { get => _projectile; set => _projectile = value; }
    public GameObject ProjectileSpawnParticle { get => _spawnParticle; set => _spawnParticle = value; }
    public GameObject ProjectileHitParticle { get => _hitParticle; set => _hitParticle = value; }
    public GameObject DamagedParticle { get => _damagedParticle; set => _damagedParticle = value; }
    public GameObject DieParticle { get => _dieParticle; set => _dieParticle = value; }

    private void Awake()
    {
        AttackRange = data.attackRange;
        AttackPower = data.attackPower;
        AttackSpeed = data.attackSpeed;
        Health = data.health;
        MaxHealth = data.maxHealth;
        ProjectilePrefab = data.projectile;
        ProjectileSpawnParticle = data.projectileSpawnParticle;
        ProjectileHitParticle = data.ProjectileHitParticle;
        DamagedParticle = data.damagedParticle;
        DieParticle = data.dieParticle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
        
    }

}
