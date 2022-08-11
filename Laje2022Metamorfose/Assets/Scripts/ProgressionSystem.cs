using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private List<TransformationTime> _transformations = new List<TransformationTime>();
    
    [Header("UI")]
    [SerializeField] private TMP_Text _timerLabel;
    
    private float _currentTimer;
    private float _transformationTimer;
    private int _transformationIndex;

    void Update()
    {
        TransformationTimer();

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