using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthCollectible : Collectable
{
    [SerializeField] private int health;
    [SerializeField] private GameObject healthCollectibleVisual;
    private float speed = 100f;
    private SFX sfx;
    private void Start()
    {
        sfx = FindFirstObjectByType<SFX>();
    }
    //add health player
    public override void AddToPlayer(CharacterBase characterBase)
    {
        var totalhealth =  Player.instance.GetHealth();
        totalhealth += health;
        if(totalhealth >= 100) {
            Player.instance.SetHealth(100);
        }
        else
        {
            Player.instance.SetHealth(totalhealth);
        }
        
        BattleUI battleUI = FindFirstObjectByType<BattleUI>();
        battleUI.AddHealthUI();
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
        //rotate health visual
        healthCollectibleVisual.transform.Rotate(0, 1 * speed * Time.deltaTime, 0);
    }
}
