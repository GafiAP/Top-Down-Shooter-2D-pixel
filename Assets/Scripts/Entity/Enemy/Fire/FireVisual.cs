using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVisual : MonoBehaviour
{
    private const string FIREATTACK = "FireAttack";
    private const string FIREDEFEAT = "FireDefeat";
    
    [SerializeField] private Fire fire;
    [SerializeField] private Animator animator;
    
    void Start()
    {
        //subscribe to OnEnemyAttack event
        fire.OnEnemyAttack += Fire_OnEnemyAttack;
        fire.OnEnemyDefeat += Fire_OnEnemyDefeat;
    }

    private void Fire_OnEnemyDefeat(object sender, System.EventArgs e)
    {
        animator.SetTrigger(FIREDEFEAT);
    }

    private void Fire_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(FIREATTACK);
    }
    //spawn fireball
    public void FireBallAttack()
    {
        fire.SpawnFireBall();
    }
    public void ReleaseGameObject()
    {
        fire.ReleaseGameObject();
    }
}
