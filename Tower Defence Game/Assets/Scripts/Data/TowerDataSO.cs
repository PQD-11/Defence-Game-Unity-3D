using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Towers/Data")]

[Serializable]
public class TowerDataSO : ScriptableObject
{
    public string type;
    public float attackDamage;
    public float attackRange;
    public float fireRate;
    public int Price;
    public TowerUpgradeStage[] upgradeStages;
}

[Serializable]
public struct TowerUpgradeStage
{
    public float attackRange;
    public float fireRate;
    public int Price;
}

