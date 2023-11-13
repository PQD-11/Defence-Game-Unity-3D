using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Rigidbody rb;
    private float destroyTime = 2f;
    public float moveSpeed;
    private float damageAttack = 5f;

    private void OnEnable() {
        StartCoroutine(ReturnToPoolAfterTime());
        rb.velocity = transform.forward * moveSpeed;
        AudioManager.Instance.PlaySFX(2);
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (gameObject.activeSelf == true)
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageAttack);
            Debug.Log(damageAttack);
        }
        // Destroy(gameObject);
        // Instantiate(impactEffect, transform.position, Quaternion.identity);

        ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
        ObjectPoolManager.Instance.SpawnObject(impactEffect, transform.position, transform.rotation, ObjectPoolManager.PoolType.ParticleSystem);

        AudioManager.Instance.PlaySFX(6);
    }

    public void SetUp(float damage)
    {
        damageAttack = damage;
    }
}
