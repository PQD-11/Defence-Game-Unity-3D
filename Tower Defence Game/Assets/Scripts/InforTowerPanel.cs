using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InforTowerPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI price, type, range, attackDamage, fireRate;
    [SerializeField] private TowerDataSO towerDatas;

    public void TowerInforButton()
    {
        price.text = $"Price: {towerDatas.Price} / {towerDatas.upgradeStages[0].Price} / {towerDatas.upgradeStages[1].Price} / {towerDatas.upgradeStages[2].Price}";
        type.text = $"Type: {towerDatas.type}";
        range.text = $"Attack Range: {towerDatas.attackRange} / {towerDatas.upgradeStages[0].attackRange} / {towerDatas.upgradeStages[1].attackRange} / {towerDatas.upgradeStages[2].attackRange}";
        attackDamage.text = $"Attack Damage: {towerDatas.attackDamage}";
        fireRate.text = $"Fire Rate: {towerDatas.fireRate} / {towerDatas.upgradeStages[0].fireRate} / {towerDatas.upgradeStages[1].fireRate} / {towerDatas.upgradeStages[2].fireRate}";
    }
}
