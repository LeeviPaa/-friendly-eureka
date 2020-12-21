using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

public class NaughtyObjective : ObjectiveBase
{
    [Header("Objective Type Specific:")]
    [SerializeField]
    private List<HouseTarget> _targets;
    private int _currentCount;

    public UnityEvent<int, int> OnNaughtyCountUpdated = new UnityEvent<int, int>();

    public override void BeginObjective()
    {
        foreach (var house in _targets)
        {
            house.SetIsNaughty(true);
            house.OnNaughtyChanged.AddListener(OnNaughtyChanged);
        }
        UpdateNaughtyCount();
    }

    public override void CleanupObjective()
    {
        IsComplete = false;
        foreach (var house in _targets)
        {
            house.OnNaughtyChanged.RemoveListener(OnNaughtyChanged);
            house.SetIsNaughty(false);
        }
        Reset();
    }

    public void UpdateNaughtyCount()
    {
        var currentCount = 0;
        foreach (var house in _targets)
        {
            if (house.IsNaughty) currentCount++;
        }
        if (_currentCount > currentCount && currentCount > 0)
        {
            HUDController.Instance.CommsBoxMessageAction.Invoke("Child slai<i>N</i>");
        }
        _currentCount = currentCount;
        OnNaughtyCountUpdated.Invoke(currentCount, _targets.Count);
        //Update HUD naughty count
    }

    public void OnNaughtyChanged(bool value, HouseTarget target)
    {
        if (value) return;
        target.OnNaughtyChanged.RemoveListener(OnNaughtyChanged);
        UpdateNaughtyCount();
        if (_currentCount <= 0)
        {
            IsComplete = true;
        }
        OnObjectiveUpdate.Invoke(this);
    }

    public void Reset()
    {
        OnNaughtyCountUpdated.RemoveAllListeners();
    }
}
