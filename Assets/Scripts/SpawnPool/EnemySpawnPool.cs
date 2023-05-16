using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class EnemySpawnPool : MonoBehaviour
{
    public ObjectPool<Fire> _firePool;
    public ObjectPool<Wraith> _wraithPool;

    [SerializeField] private Fire firePrefab;
    [SerializeField] private Wraith wraithPrefab;
    
    void Awake()
    {
        
        FirePool();
        WraithPool();
    }
    //firepool method
    public void FirePool()
    {
        _firePool = new ObjectPool<Fire>(() =>
        {
            return Instantiate(firePrefab);
        }, fire =>
        {
            fire.gameObject.SetActive(true);
        }, fire =>
        {
            fire.gameObject.SetActive(false);
        }, fire =>
        {
            Destroy(fire.gameObject);
        }, false, 10, 20);
    }
    //wraithpool method
    public void WraithPool()
    {
        _wraithPool = new ObjectPool<Wraith>(() =>
        {
            return Instantiate(wraithPrefab);
        }, wraith =>
        {
            wraith.gameObject.SetActive(true);
        }, wraith =>
        {
            wraith.gameObject.SetActive(false);
        }, wraith =>
        {
            Destroy(wraith.gameObject);
        }, false, 10, 20);
    }
}
