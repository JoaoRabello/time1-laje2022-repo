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
    [SerializeField] private Animate animate;
    
    [Header("UI")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private AudioSource GameplayBackgroundMusic;
    [SerializeField] private AudioSource GameOverBackgroundMusic;
    
    Rigidbody2D rb;
    Vector3 movementVector;

    bool facingRight = true;
    private float _currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();

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
            _gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            GameplayBackgroundMusic.Stop();
            GameOverBackgroundMusic.Play();
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
        animate.PlayTransformation(characterData);

        animate.SetAnimatorController(_currentCharacterData.Animator);
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
        
        movementVector *= _currentCharacterData.Speed;
        
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
