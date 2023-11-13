using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWaveSpawner : MonoBehaviour
{
    public List<EnemyWave> wavesToSpawn;
    private float spawnCounter;
    public float waitForFirstSpawn;
    public Transform spawnPoint;
    public Castle theCastle;
    public Path thePath;
    private bool shouldSpawn = true;

    private void Start()
    {
        spawnCounter = waitForFirstSpawn;
    }

    private void Update()
    {
        if (shouldSpawn)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                if (wavesToSpawn.Count > 0)
                {
                    if (wavesToSpawn[0].enemySpawnOrder.Count > 0)
                    {
                        Instantiate(wavesToSpawn[0].enemySpawnOrder[0], spawnPoint.position, spawnPoint.rotation).Setup(theCastle, thePath);

                        spawnCounter = wavesToSpawn[0].timeBetweenSpawns;

                        wavesToSpawn[0].enemySpawnOrder.RemoveAt(0);
                        if (wavesToSpawn[0].enemySpawnOrder.Count == 0)
                        {
                            spawnCounter = wavesToSpawn[0].timeBetweenWaves;

                            wavesToSpawn.RemoveAt(0);

                            if (wavesToSpawn.Count == 0)
                            {
                                shouldSpawn = false;
                            }
                        }
                    }
                }
            }
        }

    }

}

[System.Serializable]
public class EnemyWave
{
    public List<EnemyController> enemySpawnOrder = new List<EnemyController>(0);
    public float timeBetweenSpawns;
    public float timeBetweenWaves;
}
