using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void DamageToThis(float damage);
    float GetDamage();
    void SetDamage(float damage);
    void SetHealth(float health);
    float GetHealth();

    SpawnProjectileParticlePool GetProjectileParticlePool();
    void SetProjectileParticlePool(SpawnProjectileParticlePool spawnProjectileParticlePool);

}
