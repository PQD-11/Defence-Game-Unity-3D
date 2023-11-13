using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Data" )]

[Serializable]
public class EnemyDataSO : ScriptableObject
{
    public float moveSpeed;
    public float timeBetweenAttack;
    public float attackDamage;
    public bool isFlying;
    public float totalHealth;
    public int moneyOnDeath;
    public float flyHeight;
}
