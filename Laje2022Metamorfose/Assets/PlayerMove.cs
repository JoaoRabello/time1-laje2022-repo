using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour{
    Rigidbody2D rb;
    Vector3 movementVector;
    float speed = 3f;

    Animate animate;
    bool facingRight = true;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    void Update(){
        movementVector.x = Input.GetAxisRaw("Horizontal");      //Recebe os controles de movimento e desloca o personagem
        movementVector.y = Input.GetAxisRaw("Vertical");
        movementVector *= speed;
        rb.velocity = movementVector;

        if((movementVector.x != 0) || (movementVector.y != 0)) { //Checa se o personagem está em movimento para mudar a animação de Idle e Walk
            animate.inMovement = true;
        } else {
            animate.inMovement = false;
        }

        if(movementVector.x > 0 && !facingRight) {    //Checa pra qual lado o personagem está virado e o vira dependendo da direção
            Flip();
        }
        if(movementVector.x < 0 && facingRight) {
            Flip();
        }

    }

    void Flip(){    //Vira o Sprite do personagem na direção oposta
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
