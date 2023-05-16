using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpellVisual : MonoBehaviour
{
    private const string ONEXPLODE = "Explode";

    [SerializeField] private FireBallSpell fireballSpell;
    [SerializeField] private Animator animator;

    private SFX sfx;
    void Start()
    {
        //subscribe to OnExplode event
        fireballSpell.OnExplode += Fireball_OnExplode;
        sfx = FindFirstObjectByType<SFX>();
    }

    private void Fireball_OnExplode(object sender, System.EventArgs e)
    {
        //trigger Explode animator parameter
        animator.SetTrigger(ONEXPLODE);
    }
    //release fireball parent
    public void ReleaseFireballSpell()
    {
        fireballSpell.releaseFireball();
    }
    public void PlayExplodeSFX()
    {
        if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
        {
            //Player dead or victory
            sfx.StopPlay();
        }
        else
        {
            sfx.ExplodeSFX();
        }
    }
}
