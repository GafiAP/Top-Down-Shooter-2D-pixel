using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyPositionSO : ScriptableObject
{
    public Transform[] enemyRangedPosition;
    public Transform[] enemyMeleePosition;
    public string difficulty;
}
