using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NaughtyListMission : MissionBase
{
    [SerializeField]
    private List<HouseTarget> _targets;
    private int _currentCount;

    public UnityEvent<int, int> OnNaughtyCountUpdated = new UnityEvent<int, int>();

    public override void BeginMission()
    {
        foreach (var house in _targets)
        {
            house.SetIsNaughty(true);
            house.OnNaughtyChanged.AddListener(OnNaughtyChanged);
        }
        UpdateNaughtyCount();

    }

    public void UpdateNaughtyCount()
    {
        var currentCount = 0;
        foreach (var house in _targets)
        {
            if (house.IsNaughty) currentCount++;
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
            OnComplete();
        }
    }

    public void Reset()
    {
        OnNaughtyCountUpdated.RemoveAllListeners();
    }
}
