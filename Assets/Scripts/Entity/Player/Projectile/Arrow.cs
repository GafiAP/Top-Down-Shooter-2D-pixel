using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    public Transform targetArrow;
    private float damage;
    private SFX sfx;
    private void Start()
    {
        //set arrow damage from player damage and find sfx object
        damage = Player.instance.GetDamage();
        sfx = FindFirstObjectByType<SFX>();
    }
    void Update()
    {
        //check if target is null, if target null release this arrow
        if (targetArrow != null)
        {
            //transform this arrow position to target position
            transform.position = Vector3.MoveTowards(transform.position, targetArrow.position, speed * Time.deltaTime);
            ArrowLookAtTarget();
            //release this arrow if this arrow position == target position
            if (this.transform.position == targetArrow.transform.position)
            {
                Player.instance.ReleaseArrow(this);
            }
        }
        else
        {
            Player.instance.ReleaseArrow(this);
        }
        
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check collide enemy
        if(collision.transform.TryGetComponent(out EnemyBase enemyBase))
        {
            //if collide damage to enemy, release this arrow, play sfx
            enemyBase.DamageToThis(damage);
            Player.instance.ReleaseArrow(this);
            playArrowSound();
        }
        if (collision.transform.TryGetComponent(out Crate crate))
        {
            crate.DamageToThis(Player.instance.GetDamage());
        }
    }
    //rotation arrow to look at target
    private void ArrowLookAtTarget()
    {
        if (targetArrow != null)
        {
            Vector3 targ = targetArrow.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    //arrow hit sfx
    public void playArrowSound()
    {
        if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
        {
            //Player dead or victory
            sfx.StopPlay();
        }
        else
        {
            sfx.ArrowHitSFX();
        }
    }
}
