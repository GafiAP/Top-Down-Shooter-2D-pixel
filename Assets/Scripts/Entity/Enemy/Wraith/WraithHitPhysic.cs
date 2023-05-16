using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithHitPhysic : MonoBehaviour
{
    [SerializeField] private Wraith wraith;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out CharacterBase characterBase))
        {
            characterBase.DamageToThis(wraith.GetDamage());
        }
    }
}
