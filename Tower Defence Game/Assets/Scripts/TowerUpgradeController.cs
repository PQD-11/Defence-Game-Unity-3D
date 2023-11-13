using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    [SerializeField] private Tower tower;
    public int currentUpgrade;
    public bool hasUpgrade = true;

    public void Upgrade()
    {
        tower.attackRange = tower.towerDataSO.upgradeStages[currentUpgrade].attackRange;
        tower.rangeModel.transform.localScale = new Vector3(tower.attackRange, 1f, tower.attackRange);
        // Debug.Log("Range:" + tower.attackRange);
        tower.fireRate = tower.towerDataSO.upgradeStages[currentUpgrade].fireRate;
        // Debug.Log("fireRate:" + tower.fireRate);
        
        currentUpgrade++;
        if (currentUpgrade >= tower.towerDataSO.upgradeStages.Length)
        {
            hasUpgrade = false;
        }
    }
}

// [System.Serializable]
// public class UpgradeStage
// {
//     public float range;
//     public float fireRate;
//     public int cost;
// }