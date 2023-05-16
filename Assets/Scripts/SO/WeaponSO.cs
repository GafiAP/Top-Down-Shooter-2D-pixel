using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public float weaponDamage;
    public float cooldown;
}
