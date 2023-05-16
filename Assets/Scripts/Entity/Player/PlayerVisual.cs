using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    private const string BOWATTACK = "BowAttack";
    private const string PUNCHATTACK = "PunchAttack";
    private const string SPELLATTACK = "SpellAttack";

    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    private SFX sfx;

    void Start()
    {
        //subscribe to event and find sfx object
        player.OnPlayerMove += Instance_OnPlayerMove;
        player.OnPlayerAttack += Instance_OnPlayerAttack;
        sfx = FindFirstObjectByType<SFX>();
    }

    private void Instance_OnPlayerAttack(object sender, Player.OnPlayerAttackEventArgs e)
    {
        //bow attack
        if (e.state == Player.WeaponState.Bow)
        {
            animator.SetTrigger(BOWATTACK);
            animator.SetFloat("BowDir", player.CheckPlayerToEnemyDirection());
            Player.instance.arrow--;
        }
        //punch attack
        if(e.state == Player.WeaponState.Punch)
        {
            animator.SetTrigger(PUNCHATTACK);
            if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
            {
                //Player dead or victory
                sfx.StopPlay();
            }
            else
            {
                sfx.PunchSFX();
            }
        }
        //spell attack
        if(e.state == Player.WeaponState.Spell)
        {
            animator.SetTrigger(SPELLATTACK);
            animator.SetFloat("SpellDir", player.CheckPlayerToEnemyDirection());
            Player.instance.resetCooldownSpell();
        }
        
    }

    private void Instance_OnPlayerMove(object sender, System.EventArgs e)
    {
        animator.SetFloat("Horizontal", player.inputVector.x);
        animator.SetFloat("Vertical", player.inputVector.y);
        animator.SetFloat("Speed", player.inputVector.sqrMagnitude);
    }
    //this method called from animation
    private void SetAttackModeToFalse()
    {
        player.AttackMode = false;
        player.playerState = Player.PlayerState.Move;
    }
    //this method called from animation
    private void spawnArrow()
    {
        player.SpawnArrow();
    }
    //this method called from animation
    private void spawnFireballSpell()
    {
        player.SpawnFireBallSpell();
    }
}
