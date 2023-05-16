using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IDamageable,IAttack
{
    private float health;
    private Transform target;
    private float maxCoolDown;
    private float damage;
    private SpawnProjectileParticlePool spawnProjectileParticlePool;
    private EnemySpawnPool enemySpawnPool;

    public virtual EnemySpawnPool GetEnemySpawnPool()
    {
        return this.enemySpawnPool;
    }
    public virtual void SetEnemySpawnPool(EnemySpawnPool enemySpawnPool)
    {
        this.enemySpawnPool = enemySpawnPool;
    }
    //Damage to entity has this script
    public void DamageToThis(float damage)
    {
        var blood = GetProjectileParticlePool()._bloodPool.Get();
        blood.transform.SetParent(GetProjectileParticlePool().transform);
        blood.transform.position = this.transform.position;
        this.health -= damage;
    }
    //getset damage
    public float GetDamage()
    {
        return this.damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    //getset Health
    public float GetHealth()
    {
        return this.health;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }

    //getset Cooldown
    public float GetCoolDown()
    {
        return this.maxCoolDown;
    }
    public void SetCoolDown(float value)
    {
        this.maxCoolDown = value;
    }
    //getset Target
    public Transform GetTarget()
    {
        return this.target;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public SpawnProjectileParticlePool GetProjectileParticlePool()
    {
        return this.spawnProjectileParticlePool;
    }

    public void SetProjectileParticlePool(SpawnProjectileParticlePool spawnProjectileParticlePool)
    {
        this.spawnProjectileParticlePool = spawnProjectileParticlePool;
    }
}
