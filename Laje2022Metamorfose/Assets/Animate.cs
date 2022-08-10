using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour{

    Animator animator;

    public bool inMovement;


    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        animator.SetBool("inMovement", inMovement);
    }
}
