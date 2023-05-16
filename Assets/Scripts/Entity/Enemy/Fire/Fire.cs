using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fire : EnemyBase
{
    public event EventHandler OnEnemyAttack;
    public event EventHandler OnEnemyDefeat;

    [SerializeField] private float coolDown;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private FireAttackDistance fireAttackDistance;


    private float timer;
    private bool isDefeat;
    private enum state
    {
        Idle,
        Attack,
        Defeat,
        Victory
    }
    private state FireState;

    private void Start()
    {
        
        //set first enemy state, reset timer, set cooldown,damage, and particle effect
        isDefeat = false;
        FireState = state.Idle;
        timer = 0f;
        SetHealth(enemySO.health);
        SetCoolDown(enemySO.maxCooldown);
        SetDamage(enemySO.damage);
        SetTarget(GameObject.FindFirstObjectByType<Player>().transform);
        SetProjectileParticlePool(FindFirstObjectByType<SpawnProjectileParticlePool>());
        SetEnemySpawnPool(FindFirstObjectByType<EnemySpawnPool>());
    }
    private void Update()
    {
        //check enemy health
        CheckHealth();
        //check switch case
        switch (FireState)
        {
            //case state idle
            case state.Idle:
                var enemyInDistance =  fireAttackDistance.inDistance;
                //check is target null
                if (GetTarget() != null && enemyInDistance)
                {
                    //target not null and add timer by seconds
                    timer += Time.deltaTime;
                    if (timer >= GetCoolDown())
                    {
                        //if timer more than or equal to max cooldown set enemystate to attack
                        FireState = state.Attack;
                    }
                }
                break;
           //case state attack
           case state.Attack:
                //run OnEnemyAttack event, set timer to 0 and set enemy state to idle
                OnEnemyAttack?.Invoke(this, EventArgs.Empty);
                timer = 0f;
                FireState = state.Idle;
                break;
           //case state defeat
           case state.Defeat:
                OnEnemyDefeat?.Invoke(this, EventArgs.Empty);
                break;
            case state.Victory:
                break;
        }
    }
    private void CheckHealth()
    {
        //if enemy health less or equal 0 set enemy state to defeat
        if (GetHealth() <= 0 && !isDefeat)
        {
            isDefeat = true;
            Player.instance.addScore(enemySO.score);
            FireState = state.Defeat;
        }
        if(Player.instance.GetHealth() <= 0)
        {
            FireState = state.Victory;
        }
    }
    //spawn fireball
    public void SpawnFireBall()
    {
        var fireBall = GetProjectileParticlePool()._fireballPool.Get();
        fireBall.setFireVariable(this);
        fireBall.transform.SetParent(GetProjectileParticlePool().transform);
        fireBall.transform.position = this.transform.position;
        fireBall.targetLastPosition = this.GetTarget().position;
        fireBall.explode = false;
    }
    //release fireball
    public void ReleaseFireBall(FireBall fireBall)
    {
        GetProjectileParticlePool()._fireballPool.Release(fireBall);
        
    }
    //release this game object
    public void ReleaseGameObject()
    {
        GetEnemySpawnPool()._firePool.Release(this);
    }
}
