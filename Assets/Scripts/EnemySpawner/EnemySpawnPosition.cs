using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    [SerializeField] private EnemyPositionSO[] enemyPositionSO;
    [SerializeField] private EnemySpawnPool pool;
    // Start is called before the first frame update

    private void Start()
    {
        SpawnEnemyToSOPosition();
    }
    //spawn enemy to SO Position
    private void SpawnEnemyToSOPosition()
    {
        for (int i = 0; i < enemyPositionSO.Length; i++)
        {
            //check game manager difficulty
            if (enemyPositionSO[i].difficulty == GameManager.difficulty)
            {
                //check if enemymeleeposition > 0
                if (enemyPositionSO[i].enemyMeleePosition.Length > 0)
                {
                    //spawn wraith enemy to melee position
                    for (int j = 0; j < enemyPositionSO[i].enemyMeleePosition.Length; j++)
                    {
                        var wraith = pool._wraithPool.Get();
                        wraith.transform.position = enemyPositionSO[i].enemyMeleePosition[j].transform.position;
                        wraith.transform.SetParent(pool.transform);
                    }
                }
                //check if enemyrangedposition > 0
                if (enemyPositionSO[i].enemyRangedPosition.Length > 0)
                {
                    //spawn fire enemy to ranged position
                    for (int k = 0; k < enemyPositionSO[i].enemyRangedPosition.Length; k++)
                    {
                        var fire = pool._firePool.Get();
                        fire.transform.position = enemyPositionSO[i].enemyRangedPosition[k].transform.position;
                        fire.transform.SetParent(pool.transform);
                    }
                }
            }
        }
    }
}
