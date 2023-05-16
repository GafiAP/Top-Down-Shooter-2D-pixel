using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPhysics : MonoBehaviour
{
    [SerializeField] private CharacterBase characterBase;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collide with enemy, do damage to enemy
        if (collision.transform.TryGetComponent(out EnemyBase enemyBase))
        {
            enemyBase.DamageToThis(characterBase.GetDamage());

        }
        if(collision.transform.TryGetComponent(out Crate crate))
        {
            crate.DamageToThis(characterBase.GetDamage());
        }
    }
}
