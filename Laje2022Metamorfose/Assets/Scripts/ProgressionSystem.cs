using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;
    [SerializeField] private List<TransformationTime> _transformations = new List<TransformationTime>();
    [SerializeField] private List<EnemySpawnTime> _enemySpawnTimes = new List<EnemySpawnTime>();
    
    [Header("UI")]
    [SerializeField] private TMP_Text _timerLabel;
    
    private float _currentTimer;
    
    private float _transformationTimer;
    private int _transformationIndex;
    
    private float _enemySpawnTimer;
    private int _enemySpawnTimesIndex;

    private void Start()
    {
        ChangeEnemySpawnTime(_enemySpawnTimes[_enemySpawnTimesIndex].SpawnRateInSeconds);
    }

    void Update()
    {
        //TODO: Mudar os timers para seguirem o _currentTimer ao inves de terem timers unicos
        TransformationTimer();
        EnemySpawnTimer();

        _currentTimer += Time.deltaTime;
        
        var timeInMinutes = Mathf.FloorToInt(_currentTimer / 60f);
        var timeInSeconds = Mathf.FloorToInt(_currentTimer % 60f);
        _timerLabel.SetText($"{timeInMinutes:00}:{timeInSeconds:00}");
    }

    private void TransformationTimer()
    {
        if (_transformationIndex >= _transformations.Count) return;
        
        if (_transformationTimer >= _transformations[_transformationIndex].Time)
        {
            TransformPlayer(_transformations[_transformationIndex].Character);
            
            _transformationTimer = 0;
            _transformationIndex++;
        }
        else
        {
            _transformationTimer += Time.deltaTime;
        }
    }
    
    private void EnemySpawnTimer()
    {
        if (_enemySpawnTimesIndex >= _enemySpawnTimes.Count) return;

        if (Mathf.Abs(_enemySpawnTimes[_enemySpawnTimesIndex].TimeInSecondsToEnd - _currentTimer) < 0.1f)
        {
            _enemySpawnTimesIndex++;
            
            if (_enemySpawnTimesIndex >= _enemySpawnTimes.Count) return;
            
            ChangeEnemySpawnTime(_enemySpawnTimes[_enemySpawnTimesIndex].SpawnRateInSeconds);
        }
        
        // if (_enemySpawnTimer >= _enemySpawnTimes[_enemySpawnTimesIndex].Time)
        // {
        //     _enemySpawnTimesIndex++;
        //     
        //     if (_enemySpawnTimesIndex >= _enemySpawnTimes.Count) return;
        //     ChangeEnemySpawnTime(_enemySpawnTimes[_enemySpawnTimesIndex].TimeToSpawn);
        //     
        //     _enemySpawnTimer = 0;
        // }
        // else
        // {
        //     _enemySpawnTimer += Time.deltaTime;
        // }
    }

    private void ChangeEnemySpawnTime(float value)
    {
        _enemySpawnerManager.SetTimeToSpawn(value);
    }

    private void TransformPlayer(CharacterData character)
    {
        _player.Transform(character);
    }
}

[Serializable]
public class TransformationTime
{
    public CharacterData Character;
    public float Time;
}

[Serializable]
public class EnemySpawnTime
{
    public float SpawnRateInSeconds;
    public float TimeInSecondsToEnd;
}