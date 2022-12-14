using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Create Weapon")]
public class WeaponData : ScriptableObject
{
    public float AttackCooldown;
    public float AttackRange;
    public float Damage;

    public GameObject VisualEffect;
    public float VisualEffectTime;

    public List<AudioClip> SFX = new List<AudioClip>();

    public AudioClip GetRandomAttackVFX()
    {
        var randomIndex = Random.Range(0, SFX.Count);
        return SFX[randomIndex];
    }
}
