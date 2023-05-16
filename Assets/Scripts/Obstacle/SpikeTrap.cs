using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private const string SPIKEACTIVE = "SpikeActive";
    [SerializeField] private Animator animator;

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if collide with player, trigger animation
        if (collision.TryGetComponent(out CharacterBase characterBase))
        {
            animator.SetTrigger(SPIKEACTIVE);
        }
    }

}
