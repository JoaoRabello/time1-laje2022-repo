using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();
    
    private float _timeToSpawn;
    private float _timer;
    
    public void SetTimeToSpawn(float value)
    {
        _timeToSpawn = value;
    }
    
    private void Update()
    {
        if (_timer >= _timeToSpawn)
        {
            SpawnEnemyAtRandomSpawner();

            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void SpawnEnemyAtRandomSpawner()
    {
        var spawnerIndex = Random.Range(0, _spawners.Count);
        
        _spawners[spawnerIndex].SpawnEnemy(_playerTransform);
    }
}
