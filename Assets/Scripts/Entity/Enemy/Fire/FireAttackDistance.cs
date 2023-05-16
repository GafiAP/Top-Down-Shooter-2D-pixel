using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackDistance : MonoBehaviour
{
    public bool inDistance;
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        //player in attack distance
        if (collision.TryGetComponent(out CharacterBase characterBase))
        {
            IsEnemyOnDistance(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //player out from attack distance
        if (collision.TryGetComponent(out CharacterBase characterBase))
        {
            IsEnemyOnDistance(false);
        }
    }
    //check is enemy in attack distance
    private bool IsEnemyOnDistance(bool inDistance)
    {
        this.inDistance = inDistance;
        return this.inDistance;
    }
}
