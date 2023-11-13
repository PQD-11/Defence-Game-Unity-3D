using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDoubleShot : TowerGun
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

            ObjectPoolManager.Instance.SpawnObject(projectile, firePoint.position + new Vector3(0.1f, 0, 0), firePoint.rotation * Quaternion.Euler(0, 5f, 0), ObjectPoolManager.PoolType.GameObject);
            ObjectPoolManager.Instance.SpawnObject(shotEffect, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.ParticleSystem);

            ObjectPoolManager.Instance.SpawnObject(projectile, firePoint.position + new Vector3(-0.1f, 0, 0), firePoint.rotation * Quaternion.Euler(0, -5f, 0), ObjectPoolManager.PoolType.GameObject);
            ObjectPoolManager.Instance.SpawnObject(shotEffect, firePoint.position, firePoint.rotation, ObjectPoolManager.PoolType.ParticleSystem);
        }
    }
}
