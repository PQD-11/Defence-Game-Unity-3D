using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] private EnemyController enemyToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Castle castle;
    [SerializeField] private Path thePath;
    [SerializeField] private EnemyController[] enemiesToSpawn;
    public int amountToSpawn = 15;

    public float timeBetweenSpawns;
    private float spwanCounter;

    private void Awake()
    {
        spwanCounter = timeBetweenSpawns;
    }

    private void Update()
    {
        if (!LevelManager.Instance.levelActive) { return; }

        if (amountToSpawn > 0)
        {
            spwanCounter -= Time.deltaTime;
            if (spwanCounter <= 0)
            {
                spwanCounter = timeBetweenSpawns;

                Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPoint.position, spawnPoint.rotation).Setup(castle, thePath);

                amountToSpawn--;
            }
        }
    }
}
