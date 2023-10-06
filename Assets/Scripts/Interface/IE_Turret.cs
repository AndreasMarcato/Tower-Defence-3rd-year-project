using UnityEngine;
public interface IE_Turret
{
    abstract void Attack(Vector3 projectileSpawnPoint, Vector3 projectileTarget, GameObject projectilePrefab, GameObject projectileSpawnParticle, GameObject projectileHitParticle, float projectileAttackPower);
    abstract void DealDamage();
    abstract void TakeDamage();
}
