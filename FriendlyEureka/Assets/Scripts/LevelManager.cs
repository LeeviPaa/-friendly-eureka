using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private SantaCannon _currentActiveCannon;

    private Mission _currentMission;
    private int _currentMissionIndex = -1;

    [SerializeField]
    private List<Mission> _missions = new List<Mission>();

    [System.NonSerialized]
    public LevelState state;
    public UnityEvent<LevelState, LevelManager> OnLevelStateChanged = new UnityEvent<LevelState, LevelManager>();
    public UnityEvent<Mission> OnMissionChanged = new UnityEvent<Mission>();

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start()
    {
        AudioManager.Instance.Play("AimingMusic");
        StartNewMission();
    }

    public void StartNewMission()
    {
        if (_currentMission != null || _missions.Count <= 0) return;
        //Loop missions if run out of missons.
        SetLevelState(LevelState.MissionActive);
        AudioManager.Instance.Pause("VictoryTheme");
        AudioManager.Instance.Play("AimingMusic");
        _currentMissionIndex = _currentMissionIndex + 1 < _missions.Count ? _currentMissionIndex + 1 : 0;
        _currentMission = _missions[_currentMissionIndex];
        OnMissionChanged.Invoke(_currentMission);
        _currentMission.BeginMission();
        SetStartCannonState();
    }

    private void SetStartCannonState() {
        _currentMission?.StartCannon.SetActive(true);
        // camera, cinemachine? Here or there?
    }

    public void SetActiveCannon(SantaCannon cannon)
    {
        _currentActiveCannon?.SetActive(false);
        _currentActiveCannon = cannon;
    }

    public void SantaLaunched(Santa santa) {
        // set cinemachine from cannon/house to player
        // other effects?

    }

    public void HouseTargetHit(HouseTarget target) {
        // set cinemachine from player to house
        // other effects?
    }

    public void MissionClear(Mission mission)
    {
        if (_currentMission != mission) return;
        _currentMission.CleanupMission();
        _currentMission = null;
        SetActiveCannon(null);
        AudioManager.Instance.Pause("AimingMusic");
        AudioManager.Instance.PlayFromBeginning("VictoryTheme");
        SetLevelState(LevelState.MissionVictory);
    }

    public void MissionFailed(Mission mission)
    {
        if (_currentMission != mission) return;
        // FindObjectOfType<AudioManager>().Play("LoseTheme");
    }

    public void SetLevelState(LevelState value)
    {
        if (state == value) return;
        
        state = value;
        OnLevelStateChanged.Invoke(value, this);
    }
}
