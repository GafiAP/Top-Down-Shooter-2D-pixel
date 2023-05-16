using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleUI : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private GameObject punchUI,selectedPunchUI,bowUI,selectedBowUI,spellUI,selectedSpellUI,spellCooldownUI;
    [SerializeField] private TextMeshProUGUI arrowCountUI;
    void Start()
    {
        //subscribe to player event
        Player.instance.OnPlayerGotDamaged += Instance_OnPlayerGotDamaged;
        Player.instance.OnWeaponSelected += Instance_OnWeaponSelected;
        Player.instance.OnSpellCooldown += Instance_OnSpellCooldown;
        Player.instance.OnBowAttackArrowCount += Instance_OnBowAttackArrowCount;
    }

    private void Instance_OnBowAttackArrowCount(object sender, Player.OnBowAttackArrowCountEventArgs e)
    {
        arrowCountUI.text = e.arrow.ToString();
    }

    private void Instance_OnSpellCooldown(object sender, Player.OnSpellCooldownEventArgs e)
    {
        spellCooldownUI.SetActive(true);
        float cooldown = Mathf.Clamp(e.cooldown / Player.instance.weaponSO[2].cooldown, 0f, 1f);
        spellCooldownUI.GetComponent<Image>().fillAmount = cooldown;
        if(cooldown == 1f)
        {
            spellCooldownUI.SetActive(false);
        }
    }

    private void Instance_OnWeaponSelected(object sender, Player.OnWeaponSelectedEventArgs e)
    {
        if(e.weaponName == "Punch")
        {
            selectedPunchUI.SetActive(true);
            punchUI.SetActive(false);
            bowUI.SetActive(true);
            spellUI.SetActive(true);
            selectedSpellUI.SetActive(false);
            selectedBowUI.SetActive(false);
        }
        else if(e.weaponName == "Bow")
        {
            selectedBowUI.SetActive(true);
            bowUI.SetActive(false);
            punchUI.SetActive(true);
            spellUI.SetActive(true);
            selectedPunchUI.SetActive(false);
            selectedSpellUI.SetActive(false);
        }
        else if(e.weaponName == "FireBallSpell")
        {
            selectedSpellUI.SetActive(true);
            spellUI.SetActive(false);
            punchUI.SetActive(true);
            bowUI.SetActive(true);
            selectedPunchUI.SetActive(false);
            selectedBowUI.SetActive(false);
        }
        else
        {
            Debug.Log("Weapon not Found");
        }
    }

    private void Instance_OnPlayerGotDamaged(object sender, System.EventArgs e)
    {
        AddHealthUI();
    }
    public void AddHealthUI()
    {
        float totalhealth = Mathf.Clamp(Player.instance.GetHealth() / Player.instance.GetPlayerSO().health, 0, 1);
        playerHealthBar.fillAmount = totalhealth;
    }

    
}
