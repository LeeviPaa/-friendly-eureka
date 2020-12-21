using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    [SerializeField]
    private SantaCannon _startCannon;
    public SantaCannon StartCannon => _startCannon;

    [SerializeField]
    private List<ObjectiveBase> _objectives = new List<ObjectiveBase>();
    public List<ObjectiveBase> Objectives => _objectives;

    public void BeginMission()
    {
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
        LevelManager.instance.MissionClear(this);
    }

    public void OnMissionFail()
    {
        LevelManager.instance.MissionFailed(this);
    }

}
