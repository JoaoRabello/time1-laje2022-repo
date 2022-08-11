using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public bool inMovement;

    public void SetAnimatorController(AnimatorOverrideController controller)
    {
        _animator.runtimeAnimatorController = controller;
    }
    
    private void Update()
    {
        _animator.SetBool("inMovement", inMovement);
    }
}
