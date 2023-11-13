using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Tower : MonoBehaviour
{
    [SerializeField] public TowerUpgradeController upgrade;
    [SerializeField] private float checkTime = 0.2f;
    public GameObject rangeModel;
    public TowerDataSO towerDataSO;
    public LayerMask layerMaskEnemy;
    private Collider[] collidersInRange;
    private float checkCounter;
    public List<EnemyController> enemiesInRange = new List<EnemyController>();
    private bool isUpdateEnemies;
    public bool IsUpdateEnemies { get => isUpdateEnemies; set => isUpdateEnemies = value; }
    public int price;
    public float attackRange;
    public float fireRate;
    public float attackDamage;

    private void Awake()
    {
        checkCounter = checkTime;
        attackRange = towerDataSO.attackRange;
        price = towerDataSO.Price;
        fireRate = towerDataSO.fireRate;
        attackDamage = towerDataSO.attackDamage;
        rangeModel.transform.localScale = new Vector3(attackRange, 1f, attackRange);
    }

    private void Update()
    {
        IsUpdateEnemies = false;
        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = checkTime;

            collidersInRange = Physics.OverlapSphere(transform.position, attackRange, layerMaskEnemy);
            enemiesInRange.Clear();
            foreach (Collider col in collidersInRange)
            {
                if (col != null)
                {
                    enemiesInRange.Add(col.GetComponent<EnemyController>());
                }
            }

            IsUpdateEnemies = true;
        }

    }

    private void OnMouseDown()
    {
        if (LevelManager.Instance.levelActive)
        {
            if (TowerManager.Instance.selectTower != this)
            {
                if (TowerManager.Instance.selectTower != null)
                {
                    TowerManager.Instance.selectTower.rangeModel.SetActive(false);
                }
                TowerManager.Instance.selectTower = this;
                UIController.Instance.TowerUpgradePanelOn();
            }
            else
            {
                UIController.Instance.TowerUpgradePanelOff();
            }
        }
    }
}
