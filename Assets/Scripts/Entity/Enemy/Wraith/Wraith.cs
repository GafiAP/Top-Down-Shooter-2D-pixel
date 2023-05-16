using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wraith : EnemyBase
{
    public event EventHandler OnWraithMove;
    public event EventHandler OnWraithAttack;
    public event EventHandler OnWraithDead;
    [SerializeField] private float speed;
    [SerializeField] private EnemySO enemySO;
    private float dist;
    private bool isDefeat;
    private float timer;
    private enum state
    {
        Move,
        Attack,
        Defeat,
        Victory
    }
    private state WraithState;
    private void Start()
    {
        timer = 0f;
        isDefeat = false;
        WraithState = state.Move;
        SetHealth(enemySO.health);
        SetTarget(Player.instance.transform);
        SetDamage(enemySO.damage);
        SetCoolDown(enemySO.maxCooldown);
        SetProjectileParticlePool(FindFirstObjectByType<SpawnProjectileParticlePool>());
        SetEnemySpawnPool(FindFirstObjectByType<EnemySpawnPool>());
    }
    private void Update()
    {
        CheckHealth();
        switch (WraithState)
        {
            case state.Move:
                timer += Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,GetTarget().transform.position,speed * Time.deltaTime);
                OnWraithMove?.Invoke(this, EventArgs.Empty);
                dist = Vector3.Distance(GetTarget().transform.position, this.transform.position);
                if(dist <= 0.5f && timer >= enemySO.maxCooldown)
                {
                    WraithState = state.Attack;
                }
                break;
            case state.Attack:
                timer = 0f;
                OnWraithAttack?.Invoke(this, EventArgs.Empty);
                break;
            case state.Defeat:
                OnWraithDead?.Invoke(this, EventArgs.Empty);
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
            WraithState = state.Defeat;
        }
        if (Player.instance.GetHealth() <= 0)
        {
            WraithState = state.Victory;
        }
    }
    public int CheckPlayerToEnemyDirection()
    {
        if (GetTarget() != null)
        {
            if (transform.position.x < GetTarget().transform.position.x)
            {
                //wraith disebelah kiri player
                return 1;
            }
            else
            {
                //wraith disebelah kanan player
                return -1;
            }
        }
        else
        {
            return 0;
        }
    }
    public void BackToMoveAfterAttack() {
        WraithState = state.Move;
    }
    public void ReleaseGameObject()
    {
        GetEnemySpawnPool()._wraithPool.Release(this);
    }
}
