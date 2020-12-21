using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI;

public class DestructionObjective : ObjectiveBase
{
    [Header("Objective Type Specific:")]
    [SerializeField]
    protected List<DestructionTarget> _targets;
    [SerializeField, Tooltip("Will be displayed in the messages")]
    protected string _targetName;

    protected int _targetsLeft;

    public UnityEvent<int, int> OnProgressUpdated = new UnityEvent<int, int>();

    public override void BeginObjective()
    {
        foreach (var target in _targets)
        {
            target.OnStateChanged.AddListener(OnProgressChanged);
        }
        UpdateProgress();
    }

    public override void CleanupObjective()
    {
        IsComplete = false;
        foreach (var target in _targets)
        {
            target.OnStateChanged.RemoveListener(OnProgressChanged);
            target.SetActive(false);
        }
        Reset();
    }

    public void UpdateProgress()
    {
        var _currentProgress = 0;
        foreach (var target in _targets)
        {
            if (!target.IsActive) _currentProgress++;
        }
        if (this._targetsLeft > _currentProgress && _currentProgress > 0)
        {
            HUDController.Instance.CommsBoxMessageAction.Invoke($"{_targetName.FirstCharToUppercase()} destroye<i>D</i>");
        }
        _targetsLeft = _currentProgress;
        OnProgressUpdated.Invoke(_currentProgress, _targets.Count);
    }

    public void OnProgressChanged(bool value, DestructionTarget target)
    {
        if (value) return;
        target.OnStateChanged.RemoveListener(OnProgressChanged);
        UpdateProgress();
        if (_targetsLeft <= 0)
        {
            IsComplete = true;
        }
        OnObjectiveUpdate.Invoke(this);
    }

    public override string GetObjectiveMessage()
    {
        var maxCount = _targets.Count;
        if (IsOptional) return $"<color=#D4B343><b>[{maxCount - _targetsLeft}/{maxCount}]</b> <i>Optional</i>: Destroy {_targetName}</color>";
        return $"[{maxCount - _targetsLeft}/{maxCount}] Destroy {_targetName}";
    }

    public void Reset()
    {
        OnProgressUpdated.RemoveAllListeners();
    }
}
