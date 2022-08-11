using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProgressionSystem : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;
    [SerializeField] private List<CharacterData> _characterDatas = new List<CharacterData>();
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
        TransformationTimer();
        EnemySpawnTimer();

        _currentTimer += Time.deltaTime;
        
        var timeInMinutes = Mathf.FloorToInt(_currentTimer / 60f);
        var timeInSeconds = Mathf.FloorToInt(_currentTimer % 60f);
        _timerLabel.SetText($"{timeInMinutes:00}:{timeInSeconds:00}");
    }

    private void TransformationTimer()
    {
        if (_transformationTimer >= _transformations[_transformationIndex].Time)
        {
            if (_transformations[_transformationIndex].IsRandomCharater)
            {
                var characterIndex = Random.Range(0, _characterDatas.Count);
                TransformPlayer(_characterDatas[characterIndex]);
            }
            else
            {
                TransformPlayer(_transformations[_transformationIndex].Character);
            }
            
            _transformationTimer = 0;

            if (_transformationIndex < _transformations.Count - 1)
            {
                _transformationIndex++;
            }
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
    public bool IsRandomCharater;
    public CharacterData Character;
    public float Time;
}

[Serializable]
public class EnemySpawnTime
{
    public float SpawnRateInSeconds;
    public float TimeInSecondsToEnd;
}