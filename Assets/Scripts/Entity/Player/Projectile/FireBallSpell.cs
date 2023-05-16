using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : MonoBehaviour
{
    public event EventHandler OnExplode;
    public Transform targetLastPosition;
    public bool explode;

    [SerializeField] private float speed;


    private CharacterBase characterBase;
    private float timer;
    public SFX sfx;



    void Start()
    {

        //get target position from Fire script
        explode = false;
        sfx = FindFirstObjectByType<SFX>();

    }
    void Update()
    {
        if (!explode)
        {

            if (targetLastPosition != null)
            {
                //move fireball to target last position
                transform.position = Vector3.MoveTowards(transform.position, targetLastPosition.position, speed * Time.deltaTime);
                if (this.transform.position == targetLastPosition.transform.position)
                {
                    OnExplode?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                releaseFireball();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check is collision object has CharacterBase script
        if (collision.TryGetComponent(out EnemyBase enemyBase))
        {
            //run OnExplode event and do damage
            OnExplode?.Invoke(this, EventArgs.Empty);
            explode = true;

        }
        if (collision.transform.TryGetComponent(out Crate crate))
        {
            crate.DamageToThis(characterBase.GetDamage());
        }
    }
    public void setCharacterBaseVariable(CharacterBase characterBase) {
        this.characterBase = characterBase;
    }
    public void releaseFireball()
    {
        Player.instance.ReleaseFireBallSpell(this);
    }
}
