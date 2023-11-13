// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ProjectileTower : MonoBehaviour
// {
//     [SerializeField] private Tower tower;
//     [SerializeField] private Projectile projectile;
//     [SerializeField] private Transform firePoint;
//     [SerializeField] private Transform launcherModel;
//     [SerializeField] private GameObject shotEffect;
//     private float shotCounter;
//     private Transform target;

//     private void Update()
//     {
//         if (tower.IsUpdateEnemies)
//         {
//             if (tower.enemiesInRange.Count > 0)
//             {
//                 float minDistance = tower.attackRange + 1f;
//                 foreach (EnemyController enemy in tower.enemiesInRange)
//                 {
//                     if (enemy != null)
//                     {
//                         float distance = Vector3.Distance(transform.position, enemy.transform.position);
//                         if (distance < minDistance)
//                         {
//                             minDistance = distance;
//                             target = enemy.transform;
//                         }
//                     }
//                 }
//             }
//             else
//             {
//                 target = null;
//             }
//         }
        
//         if (target != null)
//         {
//             launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);
//             launcherModel.rotation = Quaternion.Euler(0f, launcherModel.rotation.eulerAngles.y, 0f);
//         }
//         shotCounter -= Time.deltaTime;
        
//         if (shotCounter <= 0 && target != null)
//         {
//             shotCounter = tower.fireRate;

//             firePoint.LookAt(target);

//             Instantiate(projectile, firePoint.position, firePoint.rotation).SetUp(tower.attackDamage);
//             Instantiate(shotEffect, firePoint.position, firePoint.rotation);
//         }
//     }
// }
