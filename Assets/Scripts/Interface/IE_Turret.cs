using System.Collections;
using UnityEngine;
public interface IE_Turret
{
    abstract void Attack(GameObject projectileSpawnPoint, Vector3 projectileTarget, GameObject projectilePrefab, GameObject projectileSpawnParticle, GameObject projectileHitParticle, float projectileAttackPower);

}
