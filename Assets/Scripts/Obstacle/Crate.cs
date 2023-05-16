using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    private float health;
    public void DamageToThis(float damage)
    {
        this.health -= damage;
    }

    public float GetDamage()
    {
        throw new System.NotImplementedException();
    }

    public float GetHealth()
    {
        return this.health;
    }

    public SpawnProjectileParticlePool GetProjectileParticlePool()
    {
        throw new System.NotImplementedException();
    }

    public void SetDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void SetProjectileParticlePool(SpawnProjectileParticlePool spawnProjectileParticlePool)
    {
        throw new System.NotImplementedException();
    }
    private void Start()
    {
        SetHealth(5f);
    }
    private void Update()
    {
        CheckHealth();
    }
    private void CheckHealth()
    {
        if(this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
