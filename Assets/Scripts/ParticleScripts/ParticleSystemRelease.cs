using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemRelease : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticleSystem;
    private SpawnProjectileParticlePool spawnProjectileParticlePool;

    private void Start()
    {
        
        spawnProjectileParticlePool = FindFirstObjectByType<SpawnProjectileParticlePool>();
    }
    void Update()
    {
        //if particle done, release the particle
        if (!bloodParticleSystem.IsAlive()){
            spawnProjectileParticlePool._bloodPool.Release(this.gameObject);
        }
    }
}
