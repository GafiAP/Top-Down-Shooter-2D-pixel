using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public event EventHandler OnExplode;
    public Vector3 targetLastPosition;
    public bool explode;

    [SerializeField] private float speed;
    


    private Fire fire;
    private float damage;
    
    void Start()
    {
        
        //get target position from Fire script
        explode = false;
        damage = fire.GetDamage();
        
    }


    void Update()
    {
        if (!explode)
        {
            //move fireball to target last position
            transform.position = Vector3.MoveTowards(transform.position, targetLastPosition, speed * Time.deltaTime);
            if (transform.position == targetLastPosition)
            {
                //if fireball position equal to target last position run OnExplode event
                OnExplode?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check is collision object has CharacterBase script
        if(collision.TryGetComponent(out CharacterBase characterBase))
        {
            //run OnExplode event and do damage
            OnExplode?.Invoke(this,EventArgs.Empty);
            explode = true;
            characterBase.DamageToThis(damage);
        }
    }
    public void setFireVariable(Fire fire){
        this.fire = fire;
    }
    public void releaseFireball()
    {
        fire.ReleaseFireBall(this);
    }

}
