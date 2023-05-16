using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAmmo : Collectable
{
    [SerializeField] private int ammo;
    [SerializeField] private GameObject arrowAmmoVisual;
    private float speed = 100f;
    private SFX sfx;
    private void Start()
    {
        sfx = FindFirstObjectByType<SFX>();
    }
    //add arrow ammo to player
    public override void AddToPlayer(CharacterBase characterBase)
    {
        Player.instance.arrow += ammo;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check is collision object has CharacterBase script
        if (collision.TryGetComponent(out CharacterBase characterBase))
        {
            AddToPlayer(characterBase);
            CollectThis(this.gameObject);
            if (Player.instance.GetHealth() <= 0)
            {
                //Player dead
                sfx.StopPlay();
            }
            else
            {
                sfx.CollectiblesSFx();
            }
        }
    }
    private void Update()
    {
        //rotate arrow visual
       arrowAmmoVisual.transform.Rotate(1 * speed * Time.deltaTime, 0, 0);
    }
}
