using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private BaseWeapon _baseWeapon;
    [SerializeField] private SpriteRenderer _visual;
    
    Rigidbody2D rb;
    Vector3 movementVector;
    float speed = 3f;

    Animate animate;
    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    void Update()
    {
        PerformMovement();
    }

    /// <summary>
    /// Chama todos os m�todos necess�rios para o movimento
    /// </summary>
    private void PerformMovement()
    {
        SetMovementDirection();
        RenderMovementAnimation();
        SetLookingDirection();
    }

    /// <summary>
    /// Verifica se a dire��o de movimento � a mesma que est� olhando. Se n�o, vira o personagem
    /// </summary>
    private void SetLookingDirection()
    {
        if (movementVector.x > 0 && !facingRight || movementVector.x < 0 && facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Passa para o Animate se o player est� andando ou n�o
    /// </summary>
    private void RenderMovementAnimation()
    {
        animate.inMovement = movementVector.x != 0 || movementVector.y != 0;
    }

    /// <summary>
    /// Define a dire��o do jogador, passa a mira para a arma, configura a velocidade e ent�o aplica o movimento
    /// </summary>
    private void SetMovementDirection()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");      
        movementVector.y = Input.GetAxisRaw("Vertical");
        
        movementVector.Normalize();
        
        _baseWeapon.SetAimDirection(movementVector);
        
        movementVector *= speed;
        
        rb.velocity = movementVector;
    }

    /// <summary>
    /// Define se est� olhando para a direita e vira o personagem
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;

        _visual.flipX = !facingRight;
    }
}
