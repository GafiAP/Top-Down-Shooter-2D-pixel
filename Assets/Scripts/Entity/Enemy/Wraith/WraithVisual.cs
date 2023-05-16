using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithVisual : MonoBehaviour
{
    private const string ONWRAITHATTACK = "Attack";
    private const string ONWRAITHMOVE = "Horizontal";
    private const string ONWRAITHDEATH = "Dead";
    private const string ONWRAITHATTACKDIRECTION = "AttackDir";

    [SerializeField] private Wraith wraith;
    [SerializeField] private Animator animator;

    private SFX sfx;
    // Start is called before the first frame update
    void Start()
    {
        //subsribe wraith event and find sfx object
        wraith.OnWraithMove += Wraith_OnWraithMove;
        wraith.OnWraithAttack += Wraith_OnWraithAttack;
        wraith.OnWraithDead += Wraith_OnWraithDead;
        sfx = FindFirstObjectByType<SFX>();
    }

    private void Wraith_OnWraithDead(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ONWRAITHDEATH);
    }

    private void Wraith_OnWraithAttack(object sender, System.EventArgs e)
    {
        animator.SetFloat(ONWRAITHATTACKDIRECTION, wraith.CheckPlayerToEnemyDirection());
        animator.SetTrigger(ONWRAITHATTACK);
        
    }

    private void Wraith_OnWraithMove(object sender, System.EventArgs e)
    {
        animator.SetFloat(ONWRAITHMOVE, wraith.CheckPlayerToEnemyDirection());
    }
    public void BackToMove()
    {
        wraith.BackToMoveAfterAttack();
    }
    public void ReleaseGameObject()
    {
        wraith.ReleaseGameObject();
    }
    public void WraithAttackSFX()
    {
        if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
        {
            //Player dead or victory
            sfx.StopPlay();
        }
        else {
            sfx.PunchSFX(); 
        }
            
        
    }
}
