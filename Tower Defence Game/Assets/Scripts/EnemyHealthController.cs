using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private EnemyController enemyControll;
    private float totalHealth;
    private int moneyOnDeath;

    private void Awake()
    {
        totalHealth = enemyControll.enemyData.totalHealth;
        moneyOnDeath = enemyControll.enemyData.moneyOnDeath;
        healthBar.gameObject.SetActive(false);
    }

    private void Start()
    {
        healthBar.maxValue = totalHealth;
        healthBar.value = totalHealth;

        LevelManager.Instance.activeEnemis.Add(this);

        AudioManager.Instance.PlaySFX(7);
    }

    private void Update()
    {
        // healthBar.transform.rotation = Camera.main.transform.rotation;
    }
    public void TakeDamage(float damage)
    {
        healthBar.gameObject.SetActive(true);
        totalHealth -= damage;
        if (totalHealth <= 0)
        {
            totalHealth = 0;
            Destroy(gameObject);
            MoneyManager.Instance.GiveMoney(moneyOnDeath);
            LevelManager.Instance.activeEnemis.Remove(this);

            AudioManager.Instance.PlaySFX(5);
        }

        healthBar.value = totalHealth;
    }
}
