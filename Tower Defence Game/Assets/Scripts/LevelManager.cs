using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool levelActive = false;
    public bool levelVictory;
    [SerializeField] private Castle[] theCastles;
    // [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private EnemyWaveSpawner[] waveSpawners;

    public List<EnemyHealthController> activeEnemis = new List<EnemyHealthController>();

    private void Start()
    {
        levelActive = true;

        AudioManager.Instance.PlayBGM();
    }

    private void Update()
    {
        if (levelActive)
        {
            float totalCastleHealth = 0;
            foreach(Castle castle in theCastles)
            {
                totalCastleHealth += castle.CurrentHealth;
            }
            if (totalCastleHealth <= 0)
            {
                levelVictory = false;
                levelActive = false;
                UIController.Instance.levelFail.SetActive(true);
                UIController.Instance.towerButton.SetActive(false);
            }

            bool wavesComplete = false;
            foreach(EnemyWaveSpawner waveSpawner in waveSpawners)
            {
                if (waveSpawner.wavesToSpawn.Count == 0)
                {
                    wavesComplete = true;
                    break;
                }
            }

            if (activeEnemis.Count == 0 && wavesComplete)
            {
                levelActive = false;
                levelVictory = true;
                UIController.Instance.levelComplete.SetActive(true);
                UIController.Instance.towerButton.SetActive(false);
            }
            if (!levelActive)
            {
                if (TowerManager.Instance.selectTower != null)
                {
                    UIController.Instance.TowerUpgradePanelOff();
                }
            }
        }
    }

}
