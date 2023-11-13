using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : TowerGun
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void Shot()
    {
        if (shotCounter <= 0 && target != null)
        {
            shotCounter = tower.fireRate;

            firePoint.LookAt(target);

            // Instantiate(projectile, firePoint.position, firePoint.rotation).SetUp(tower.attackDamage);
            // Instantiate(shotEffect, firePoint.position, firePoint.rotation);

            ObjectPoolManager.Instance.SpawnObject(projectile, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.GameObject);
            ObjectPoolManager.Instance.SpawnObject(shotEffect, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.ParticleSystem);
        }
    }
}
