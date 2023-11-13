using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LazerTower : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform launcherModel;
    [SerializeField] private LineRenderer lineLaser;

    private Transform target;

    private void Update()
    {
        if (target != null)
        {
            launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position), 2f * Time.deltaTime);
            launcherModel.rotation = Quaternion.Euler(0f, launcherModel.rotation.eulerAngles.y, 0f);

            lineLaser.SetPosition(0, firePoint.position);
            lineLaser.SetPosition(1, target.position);
        }
        else
        {
            lineLaser.SetPosition(0, firePoint.position);
            lineLaser.SetPosition(1, firePoint.position);
        }

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
    }

}
