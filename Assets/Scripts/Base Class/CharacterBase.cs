using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour, IDamageable
{
    public event EventHandler OnPlayerGotDamaged;
    private PlayerSO playerSO;
    private float health;
    private Transform target;
    private float damage;
    private SpawnProjectileParticlePool spawnProjectileParticlePool;
    public void DamageToThis(float damage)
    {
        var blood =  GetProjectileParticlePool()._bloodPool.Get();
        blood.transform.SetParent(GetProjectileParticlePool().transform);
        blood.transform.position = this.transform.position;
        this.health -= damage;
        OnPlayerGotDamaged?.Invoke(this, EventArgs.Empty);
    }
    public float GetDamage()
    {
        return this.damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetHealth()
    {
        return this.health;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public Transform GetTarget()
    {
        return this.target;
    }
    public SpawnProjectileParticlePool GetProjectileParticlePool()
    {
        return this.spawnProjectileParticlePool;
    }

    public void SetProjectileParticlePool(SpawnProjectileParticlePool spawnProjectileParticlePool)
    {
        this.spawnProjectileParticlePool = spawnProjectileParticlePool;
    }

    public void SetPlayerSO(PlayerSO playerSO)
    {
        this.playerSO = playerSO;
    }

    public PlayerSO GetPlayerSO()
    {
        return this.playerSO;
    }
}
