using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _currentWeapon;
    [SerializeField] private LayerMask _hitBoxLayerMask;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private List<GameObject> _aimArrowList = new List<GameObject>();

    private Vector2 _aimDirection;
    private float _attackTimer;
    private Transform _currentArrowTransform;

    private void Update()
    {
        if (Animate.IsTransforming)
        {
            return;
        }
        PerformAttackTimer();
    }

    public void SetWeapon(WeaponData weapon)
    {
        _currentWeapon = weapon;
    }

    /// <summary>
    /// Faz a contagem do tempo e, se atingir o tempo para atacar, ataca e reseta o timer
    /// </summary>
    private void PerformAttackTimer()
    {
        
        if (_attackTimer >= _currentWeapon.AttackCooldown)
        {
            Attack();
            _attackTimer = 0;
        }
        else
        {
            _attackTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Performa o ataque da arma
    /// </summary>
    private void Attack()
    {
        var vfx = Instantiate(_currentWeapon.VisualEffect, _currentArrowTransform.position, Quaternion.identity);
        vfx.transform.rotation = Quaternion.LookRotation(_currentArrowTransform.position - transform.position);
        vfx.transform.rotation = Quaternion.Euler(new Vector3(0, 0, vfx.transform.eulerAngles.z));
        
        StartCoroutine(VisualEffect(vfx));
        
        _sfxSource.PlayOneShot(_currentWeapon.GetRandomAttackVFX());
        
        var hits = Physics2D.OverlapCircleAll(_currentArrowTransform.position, _currentWeapon.AttackRange, _hitBoxLayerMask);

        foreach (var hit in hits)
        {
            var enemy = hit.GetComponentInParent<Enemy>();
            if (!enemy) continue;
            
            Debug.Log($"Deu {_currentWeapon.Damage} de dano em {_currentWeapon.AttackRange} personagens de distância em {enemy.name}");

            enemy.TakeHit(_currentWeapon.Damage);
        }
    }

    private IEnumerator VisualEffect(GameObject visualEffectObject)
    {
        yield return new WaitForSeconds(_currentWeapon.VisualEffectTime);
        Destroy(visualEffectObject);
    }

    /// <summary>
    /// Define a direção de mira se for diferente de 0 e liga a seta correspondente
    /// </summary>
    /// <param name="direction">Direção desejada</param>
    public void SetAimDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > 0.1f || Mathf.Abs(direction.y) > 0.1f)
        {
            _aimDirection = direction;
        }
        
        HighlightArrowByIndex(GetArrowIndexByDirection(_aimDirection));
    }

    /// <summary>
    /// Pega o índice da seta na lista, ordenada em sentido horário, a partir de uma direção.
    /// </summary>
    /// <param name="direction">Direção a ser verificada</param>
    /// <returns>Índice da seta na lista</returns>
    private int GetArrowIndexByDirection(Vector2 direction)
    {
        return direction.x switch
        {
            0 when direction.y > 0 => 0,
            0 when direction.y < 0 => 4,
            > 0 when direction.y > 0 => 1,
            > 0 when direction.y == 0 => 2,
            > 0 when direction.y < 0 => 3,
            < 0 when direction.y < 0 => 5,
            < 0 when direction.y == 0 => 6,
            < 0 when direction.y > 0 => 7,
            _ => 0
        };
    }

    /// <summary>
    /// Desliga todas as setas e liga somente a seta cujo índice é o determinado.
    /// </summary>
    /// <param name="index">Índice da seta a ser ligada</param>
    private void HighlightArrowByIndex(int index)
    {
        for (var i = 0; i < _aimArrowList.Count; i++)
        {
            _aimArrowList[i].SetActive(i == index);
        }

        _currentArrowTransform = _aimArrowList[index].transform;
    }
}
