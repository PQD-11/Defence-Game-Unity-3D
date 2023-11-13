using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePanel : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI attackRange, fireRate, upgradeMoney, sellMoney;

    public void SetupPanel()
    {
        Tower selectTower = TowerManager.Instance.selectTower;
        attackRange.text = "Attack Range: " + selectTower.attackRange.ToString();
        fireRate.text = "Fire Rate: " + selectTower.fireRate.ToString();
        if (selectTower.upgrade.hasUpgrade)
        {
            upgradeMoney.text = "Upgrade Money: " + selectTower.towerDataSO.upgradeStages[selectTower.upgrade.currentUpgrade].Price.ToString();
        }
        else 
        {
            upgradeMoney.text = "Upgrade Money: MAX";
        }
        sellMoney.text = "Sell Money: " + "50";
        if (selectTower.upgrade.hasUpgrade)
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    public void SellTower()
    {
        MoneyManager.Instance.GiveMoney(50);
        Destroy(TowerManager.Instance.selectTower.gameObject);
        UIController.Instance.TowerUpgradePanelOff();

        AudioManager.Instance.PlaySFX(9);
    }

    public void UpgradeTower()
    {
        Tower selectTower = TowerManager.Instance.selectTower;
        if (selectTower.upgrade.hasUpgrade && MoneyManager.Instance.SpendMoney(selectTower.towerDataSO.upgradeStages[selectTower.upgrade.currentUpgrade].Price))
        {
            selectTower.upgrade.Upgrade();
            SetupPanel();

            AudioManager.Instance.PlaySFX(10);
        }
        else if (!MoneyManager.Instance.SpendMoney(selectTower.towerDataSO.upgradeStages[selectTower.upgrade.currentUpgrade].Price))
        {
            StartCoroutine(ActivateAndDeactivate());
        }
    }

    IEnumerator ActivateAndDeactivate()
    {
        UIController.Instance.notEnoughMoneyWarning.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        UIController.Instance.notEnoughMoneyWarning.SetActive(false);
    }
}
