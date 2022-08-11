using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _visual;
    [SerializeField] private Transform _target;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private float _totalHealth;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _damage = 2f;

    private float _currentHealth;
    private Rigidbody2D rb;
    private Vector2 movement;
    private AudioSource _SFXSource;

    public float Damage => _damage;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        _currentHealth = _totalHealth;
    }

    /// <summary>
    /// Muda o alvo para o determinado.
    /// </summary>
    /// <param name="target">Novo alvo</param>
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetAudioSource(AudioSource source)
    {
        _SFXSource = source;
    }
    
    void Update()
    {
        Vector3 direction = _target.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

        _visual.flipX = movement.x < 0;
    }
    
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }
    
    void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * _moveSpeed;
    }

    /// <summary>
    /// Recebe o dano e verifica se morreu
    /// </summary>
    /// <param name="hitDamage">Dano recebido</param>
    public void TakeHit(float hitDamage)
    {
        _currentHealth -= hitDamage;
        
        StopAllCoroutines();
        StartCoroutine(HitFeedback());

        if (_currentHealth <= 0)
        {
            StopAllCoroutines();
            _SFXSource.PlayOneShot(_deathClip);
            Destroy(gameObject);
        }
    }

    private IEnumerator HitFeedback()
    {
        _visual.color = Color.red;
        
        while (_visual.color.g < 0.95)
        {
            _visual.color = new Color(_visual.color.r, _visual.color.g + Time.deltaTime * 2, _visual.color.b + Time.deltaTime * 2);
            yield return null;
        }
        
        _visual.color = Color.white;
    }
}
