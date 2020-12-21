using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Mission : MonoBehaviour
{
    // timer variables
    public float missionTime;
    private float _time;
    private float _timeR;
    private float _compareT;
    
    private bool _countdown = true;

    [SerializeField]
    private SantaCannon _startCannon;
    public SantaCannon StartCannon => _startCannon;

    [SerializeField]
    private List<ObjectiveBase> _objectives = new List<ObjectiveBase>();
    public List<ObjectiveBase> Objectives => _objectives;

    void Update()
    {
        if (_time > 0 && _countdown)
        {
            _time -= Time.deltaTime;
            _timeR = Mathf.Round(_time);
            
            if (_compareT > _timeR)
            {
                // Take the timer for the UI from here
                Debug.LogWarning($"Current time left: {_timeR}");   
            }
            
            _compareT = _timeR;
        }
        else if(_time <= 0 && _countdown)
        {
            OnMissionFail();
        }
    }

    public void BeginMission()
    {
        _time = missionTime;
        _countdown = true;
        foreach (var objective in _objectives)
        {
            objective.BeginObjective();
            objective.OnObjectiveUpdate.AddListener(OnObjectiveStateUpdate);
        }
    }

    public void CleanupMission()
    {
        foreach (var objective in _objectives)
        {
            objective.CleanupObjective();
            objective.OnObjectiveUpdate.RemoveListener(OnObjectiveStateUpdate);
        }
    }

    public void OnObjectiveStateUpdate(ObjectiveBase objective)
    {
        var IsComplete = true;
        foreach (var o in _objectives)
        {
            if (o.IsOptional || o.IsComplete) continue;
            IsComplete = false;
            break;
        }
        if (IsComplete)
        {
            OnComplete();
        }
    }

    public void OnComplete()
    {
        _countdown = false;
        LevelManager.instance.MissionClear(this);
    }

    public void OnMissionFail()
    {
        _countdown = false;
        LevelManager.instance.MissionFailed(this);
    }
}
