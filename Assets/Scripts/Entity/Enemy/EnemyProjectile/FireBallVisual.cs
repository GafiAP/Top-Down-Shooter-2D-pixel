using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallVisual : MonoBehaviour
{
    private const string ONEXPLODE = "Explode";

    [SerializeField] private FireBall fireball;
    [SerializeField] private Animator animator;

    private SFX sfx;
    void Start()
    {
        //subscribe to OnExplode event
        fireball.OnExplode += Fireball_OnExplode;
        sfx = FindFirstObjectByType<SFX>();
    }

    private void Fireball_OnExplode(object sender, System.EventArgs e)
    {
        //trigger Explode animator parameter
        animator.SetTrigger(ONEXPLODE);
    }
    //destroy fireball parent
    public void DestroyFireballParent()
    {
        fireball.releaseFireball();
    }
    public void ExplodeSFX()
    {
        if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
        {
            //Player dead or victory
            sfx.StopPlay();
        }
        {
            sfx.ExplodeSFX();
        }
    }
}
