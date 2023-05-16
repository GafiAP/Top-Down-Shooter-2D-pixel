using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    public float health;
    public float damage;
    public EnemyBase enemyPrefab;
    public float maxCooldown;
    public int score;
}
