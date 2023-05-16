using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedDistance : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> enemyList;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.TryGetComponent(out EnemyBase enemyBase))
        {
            //if enemylist = 0 add enemybase to target
            if (enemyList.Count == 0)
            {
                enemyList.Add(enemyBase);
                Player.instance.SetTarget(CheckEnemyDistance().transform);

            }
            else
            {
                if (enemyList.Contains(enemyBase))
                {
                    //same enemy
                }
                else
                {
                    //add enemybase to enemylist and set target by checking the distance
                    enemyList.Add(enemyBase);
                    Player.instance.SetTarget(CheckEnemyDistance().transform);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemyBase))
        {
            //if enemybase exit from player distance, remove enemybase from enemylist
            for(int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == enemyBase) { 
                    enemyList.RemoveAt(i);
                    if (enemyList.Count == 0)
                    {
                        //set target to null if enemylist = 0
                        Player.instance.SetTarget(null);
                    }
                    else
                    {
                        //if enemylist != 0 set target by checking enemy distance
                        Player.instance.SetTarget(CheckEnemyDistance().transform);
                    }
                }
            }
        }
    }
    //check enemy distance in enemylist
    private EnemyBase CheckEnemyDistance()
    {
        EnemyBase result = null;
        float d = float.PositiveInfinity;
            foreach (EnemyBase enemy in enemyList)
            {
                float distance = Vector3.Distance(enemy.transform.position, Player.instance.transform.position);
                if (distance < d)
                {
                    d = distance;
                    result = enemy;

                }

            }
        return result;
            
    }


}
