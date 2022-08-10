using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _visual;
    [SerializeField] private Transform _target;
    [SerializeField] float _moveSpeed = 2f;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Muda o alvo para o determinado.
    /// </summary>
    /// <param name="target">Novo alvo</param>
    public void SetTarget(Transform target)
    {
        _target = target;
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
}
