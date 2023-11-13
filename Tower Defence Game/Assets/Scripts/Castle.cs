using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }
    
    public Transform[] attackPoints;

    private void Awake()
    {
        currentHealth = totalHealth;
        healthSlider.maxValue = totalHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);

            AudioManager.Instance.PlaySFX(3);
        }
        else
        {
            AudioManager.Instance.PlaySFX(4);
        }
        healthSlider.value = currentHealth;
    }
}
