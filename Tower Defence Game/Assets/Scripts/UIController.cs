using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    public TMP_Text moneyText;
    public GameObject notEnoughMoneyWarning;
    public GameObject levelComplete, levelFail;
    public GameObject towerButton;
    public GameObject pausePanel;
    public TowerUpgradePanel towerUpgradePanel;
    public GameObject inforTowerPanel;

    public void ButtonPause()
    {
        towerButton.SetActive(!towerButton.activeSelf);
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void InforTower()
    {
        inforTowerPanel.SetActive(!inforTowerPanel.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel(Button button)
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            button.interactable = false;
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TowerUpgradePanelOn()
    {
        if (LevelManager.Instance.levelActive)
        {
            towerUpgradePanel.gameObject.SetActive(true);
            towerUpgradePanel.SetupPanel();
            TowerManager.Instance.selectTower.rangeModel.SetActive(true);
        }
    }

    public void TowerUpgradePanelOff()
    {
        towerUpgradePanel.gameObject.SetActive(false);
        TowerManager.Instance.selectTower.rangeModel.SetActive(false);
        TowerManager.Instance.selectTower = null;
    }

}
