using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private AudioSource _enemySFXSource;

    public void SpawnEnemy(Transform target)
    {
        var enemy = Instantiate(_enemy, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.SetTarget(target);
        enemy.SetAudioSource(_enemySFXSource);
    }
}
