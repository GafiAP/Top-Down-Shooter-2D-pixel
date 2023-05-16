using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collide with player, do damage to player
        if (collision.TryGetComponent(out CharacterBase characterBase))
        {
            Player.instance.DamageToThis(25);
        }
    }
    
}
