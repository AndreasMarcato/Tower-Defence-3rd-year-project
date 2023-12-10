using UnityEngine;

[CreateAssetMenu(fileName = "TurretData")]
public class SO_TurretData : ScriptableObject
{
    public float attackRange;
    public float attackSpeed;
    public float attackPower;
    public float health;
    public float maxHealth;
    public GameObject projectile;
    public GameObject projectileSpawnParticle;
    public GameObject ProjectileHitParticle;
    public GameObject dieParticle;
    public GameObject damagedParticle;
}
