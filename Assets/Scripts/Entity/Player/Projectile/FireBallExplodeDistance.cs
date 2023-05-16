using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplodeDistance : MonoBehaviour
{
    private float damage;
    private void Start()
    {
        damage = Player.instance.GetDamage();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase enemyBase))
        {
            //run OnExplode event and do damage
            enemyBase.DamageToThis(damage);
        }
        if (collision.transform.TryGetComponent(out Crate crate))
        {
            crate.DamageToThis(5);
        }
    }
}
