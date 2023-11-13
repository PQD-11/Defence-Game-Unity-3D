using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemyDataSO enemyData;

    private float moveSpeed;
    public float speedMod = 1f;
    private float attackDamage;
    private float timeBetweenAttacks;
    private float timeAttackCounter;
    private int selectAttacPoint;

    private bool isFlying;
    private float flyHeight;


    private Path thePath;
    private Castle castle;
    private int currentPoint;
    private bool reachedEnd;

    private event Action<float> OnSlow;
    [SerializeField] private GameObject smokeSlow;

    private void Awake()
    {
        moveSpeed = enemyData.moveSpeed;
        attackDamage = enemyData.attackDamage;
        timeBetweenAttacks = enemyData.timeBetweenAttack;
        isFlying = enemyData.isFlying;
        flyHeight = enemyData.flyHeight;

        OnSlow += OnSlowHandle;
    }

    public void DoOnSlow(float speedMod)
    {
        OnSlow?.Invoke(speedMod);
    }

    public bool isOnSlow()
    {
        return speedMod < 1 ? true : false;
    }

    private void OnSlowHandle(float speedMod)
    {
        this.speedMod = speedMod;
        ObjectPoolManager.Instance.SpawnObject(smokeSlow, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
    
        StartCoroutine(TimeSlow());
    }

    IEnumerator TimeSlow()
    {
        yield return new WaitForSecondsRealtime(3f);
        speedMod = 1f;
    }

    private void Start()
    {
        // if (castle == null)
        // {
        //     castle = FindObjectOfType<Castle>();
        // }

        // if (thePath == null)
        // {
        //     thePath = FindAnyObjectByType<Path>();
        // }

        timeAttackCounter = timeBetweenAttacks;

        if (isFlying)
        {
            transform.position += Vector3.up * flyHeight;
            currentPoint = thePath.points.Length - 1;
        }
    }
    private void Update()
    {
        if (!LevelManager.Instance.levelActive) { return; }

        if (!reachedEnd)
        {
            if (!isFlying)
            {
                transform.LookAt(thePath.points[currentPoint].position);

                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * speedMod * Time.deltaTime);
                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.01f)
                {
                    currentPoint++;
                    if (currentPoint >= thePath.points.Length)
                    {
                        reachedEnd = true;
                        selectAttacPoint = UnityEngine.Random.Range(0, castle.attackPoints.Length);
                    }
                }
            }
            else
            {
                transform.LookAt(thePath.points[currentPoint].position + Vector3.up * flyHeight);

                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position + (Vector3.up * flyHeight), moveSpeed * speedMod * Time.deltaTime);
                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position + (Vector3.up * flyHeight)) < 0.01f)
                {
                    currentPoint++;
                    if (currentPoint >= thePath.points.Length)
                    {
                        reachedEnd = true;
                        selectAttacPoint = UnityEngine.Random.Range(0, castle.attackPoints.Length);
                    }
                }
            }

        }
        else
        {
            if (!isFlying)
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.attackPoints[selectAttacPoint].position, moveSpeed * speedMod * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.attackPoints[selectAttacPoint].position + (Vector3.up * flyHeight), moveSpeed * speedMod * Time.deltaTime);
            }
            timeAttackCounter -= Time.deltaTime;
            if (timeAttackCounter <= 0 && castle.gameObject.activeSelf == true)
            {
                timeAttackCounter = timeBetweenAttacks;
                castle.TakeDamage(attackDamage);
            }
        }
    }

    public void Setup(Castle castle, Path thePath)
    {
        this.castle = castle;
        this.thePath = thePath;
    }
}
