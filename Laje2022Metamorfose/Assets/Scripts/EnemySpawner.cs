using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public void SpawnEnemy(Transform target)
    {
        var enemy = Instantiate(_enemy, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.SetTarget(target);
    }
}
