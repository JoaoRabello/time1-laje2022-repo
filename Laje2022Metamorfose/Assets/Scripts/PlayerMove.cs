using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private BaseWeapon _baseWeapon;
    [SerializeField] private CharacterData _currentCharacterData;
    [SerializeField] private SpriteRenderer _visual;
    
    [Header("UI")]
    [SerializeField] private Slider _hpBar;
    
    Rigidbody2D rb;
    Vector3 movementVector;

    Animate animate;
    bool facingRight = true;
    private float _currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();

        _currentHealth = _currentCharacterData.Health;
        
        _baseWeapon.SetWeapon(_currentCharacterData.Weapon);
        animate.SetAnimatorController(_currentCharacterData.Animator);
    }

    void Update()
    {
        PerformMovement();
    }

    private void TakeHit(float damage)
    {
        _currentHealth -= damage;

        _hpBar.value = _currentHealth / _currentCharacterData.Health;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyDamageArea"))
        {
            TakeHit(col.GetComponentInParent<Enemy>().Damage);
        }
    }

    public void Transform(CharacterData characterData)
    {
        _currentCharacterData = characterData;
        
        _currentHealth = _currentCharacterData.Health;
        _hpBar.value = 1;
        
        _baseWeapon.SetWeapon(_currentCharacterData.Weapon);
        animate.SetAnimatorController(_currentCharacterData.Animator);
    }

    /// <summary>
    /// Chama todos os métodos necessários para o movimento
    /// </summary>
    private void PerformMovement()
    {
        SetMovementDirection();
        RenderMovementAnimation();
        SetLookingDirection();
    }

    /// <summary>
    /// Verifica se a direção de movimento é a mesma que está olhando. Se não, vira o personagem
    /// </summary>
    private void SetLookingDirection()
    {
        if (movementVector.x > 0 && !facingRight || movementVector.x < 0 && facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Passa para o Animate se o player está andando ou não
    /// </summary>
    private void RenderMovementAnimation()
    {
        animate.inMovement = movementVector.x != 0 || movementVector.y != 0;
    }

    /// <summary>
    /// Define a direção do jogador, passa a mira para a arma, configura a velocidade e então aplica o movimento
    /// </summary>
    private void SetMovementDirection()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");      
        movementVector.y = Input.GetAxisRaw("Vertical");
        
        movementVector.Normalize();
        
        _baseWeapon.SetAimDirection(movementVector);
        
        movementVector *= _currentCharacterData.Speed;
        
        rb.velocity = movementVector;
    }

    /// <summary>
    /// Define se está olhando para a direita e vira o personagem
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;

        _visual.flipX = !facingRight;
    }
}
