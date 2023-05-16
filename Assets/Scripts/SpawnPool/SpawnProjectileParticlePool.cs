using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class SpawnProjectileParticlePool : MonoBehaviour
{
    [SerializeField] private FireBall fireballPrefab;
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private FireBallSpell fireballSpellPrefab;
    [SerializeField] private GameObject bloodPrefab;

    public ObjectPool<FireBall> _fireballPool;
    public ObjectPool<Arrow> _arrowPool;
    public ObjectPool<FireBallSpell> _fireballSpellPool;
    public ObjectPool<GameObject> _bloodPool;

    private void Awake()
    {
        FireballPool();
        ArrowPool();
        FireBallSpellPool();
        BloodPool();
    }
    //fireball pool method
    private void FireballPool()
    {
        _fireballPool = new ObjectPool<FireBall>(() =>
        {
            return Instantiate(fireballPrefab);
        }, fireBall =>
        {
            fireBall.gameObject.SetActive(true);
        }, fireBall =>
        {
            fireBall.gameObject.SetActive(false);
        }, fireBall =>
        {
            Destroy(fireBall.gameObject);
        }, false, 5, 10);
    }
    //arrow pool method
    private void ArrowPool()
    {
        _arrowPool = new ObjectPool<Arrow>(() =>
        {
            return Instantiate(arrowPrefab);
        }, arrow => {
            arrow.gameObject.SetActive(true);
        }, arrow =>
        {
            arrow.gameObject.SetActive(false);
        },arrow =>
        {
            Destroy(arrow.gameObject);
        }, false, 5, 10);
    }
    //fireballspell pool method
    private void FireBallSpellPool()
    {
        _fireballSpellPool = new ObjectPool<FireBallSpell>(() =>
        {
            return Instantiate(fireballSpellPrefab);
        }, fireballSpell =>
        {
            fireballSpell.gameObject.SetActive(true);
        }, fireballSpell =>
        {
            fireballSpell.gameObject.SetActive(false);
        }, fireballSpell =>
        {
            Destroy(fireballSpell.gameObject);
        }, false, 5, 10);
    }

    //blood pool method
    private void BloodPool()
    {
        _bloodPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(bloodPrefab);
        }, blood =>
        {
            blood.gameObject.SetActive(true);
        }, blood =>
        {
            blood.gameObject.SetActive(false);
        }, blood =>
        {
            Destroy(bloodPrefab);
        }, false, 10, 20);
    }
}
