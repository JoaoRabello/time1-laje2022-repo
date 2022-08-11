using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Create Character")]
public class CharacterData : ScriptableObject
{
    public float Speed;
    public float Health;

    public WeaponData Weapon;
    public Sprite Visual;
    public AnimatorOverrideController Animator;
}
