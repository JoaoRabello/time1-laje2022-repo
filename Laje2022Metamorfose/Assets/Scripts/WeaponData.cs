using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Create Weapon")]
public class WeaponData : ScriptableObject
{
    public float AttackCooldown;
    public float AttackRange;
    public float Damage;
}
