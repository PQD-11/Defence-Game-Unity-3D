using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{
    [SerializeField] private Tower tower;

    private void Update()
    {
        foreach (EnemyController enemy in tower.enemiesInRange)
        {
            if (enemy == null || enemy.isOnSlow()) { continue; }
            enemy.DoOnSlow(tower.fireRate);
            //draw line effect 
        }
        // effectRing.localScale = new Vector3(tower.rangeDetect, 1f, tower.rangeDetect);
    }
}
