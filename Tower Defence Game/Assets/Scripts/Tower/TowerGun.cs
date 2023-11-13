using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerGun : MonoBehaviour
{
    [SerializeField] protected Tower tower;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform launcherModel;
    [SerializeField] protected GameObject shotEffect;
    protected float shotCounter;
    protected Transform target;

    protected virtual void Update()
    {
        if (tower.IsUpdateEnemies)
        {
            if (tower.enemiesInRange.Count > 0)
            {
                float minDistance = tower.attackRange + 1f;
                foreach (EnemyController enemy in tower.enemiesInRange)
                {
                    if (enemy != null)
                    {
                        float distance = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = enemy.transform;
                        }
                    }
                }
            }
            else
            {
                target = null;
            }
        }
        
        if (target != null)
        {
            launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);
            launcherModel.rotation = Quaternion.Euler(0f, launcherModel.rotation.eulerAngles.y, 0f);
        }
        shotCounter -= Time.deltaTime;

        Shot();
        
        // if (shotCounter <= 0 && target != null)
        // {
        //     shotCounter = tower.fireRate;

        //     firePoint.LookAt(target);

        //     Instantiate(projectile, firePoint.position, firePoint.rotation).SetUp(tower.attackDamage);
        //     Instantiate(shotEffect, firePoint.position, firePoint.rotation);
        // }
    }

    protected abstract void Shot();
}
