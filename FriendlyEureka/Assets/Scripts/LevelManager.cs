using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private SantaCannon _currentActiveCannon;

    private Mission _currentMission;
    private int _currentMissionIndex = -1;

    [SerializeField]
    private List<Mission> _missions = new List<Mission>();

    private LevelState state;
    public LevelState GetState() => state;
    public UnityEvent<LevelState, LevelManager> OnLevelStateChanged = new UnityEvent<LevelState, LevelManager>();
    public UnityEvent<Mission> OnMissionChanged = new UnityEvent<Mission>();

    private InputActionAsset _inputActions;
    private InputActionMap _inputActionMap;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start()
    {
        _inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        _inputActionMap = _inputActions.FindActionMap("Player");
        SetGameInputActive(false);
        HUDController.Instance.LevelStateChanged(LevelState.MainMenu, this);
        AudioManager.Instance.Play("MenuMusic");
    }

    public void StartNewMission()
    {
        if (_currentMission != null || _missions.Count <= 0) return;
        //Loop missions if run out of missons.
        SetLevelState(LevelState.MissionActive);
        _currentMissionIndex = _currentMissionIndex + 1 < _missions.Count ? _currentMissionIndex + 1 : 0;
        _currentMission = _missions[_currentMissionIndex];
        HUDController.Instance.SetMissionHUD(_currentMission);
        _currentMission.BeginMission();
        OnMissionChanged.Invoke(_currentMission);
        SetStartCannonState();
    }

    public void RestartMission()
    {
        _currentMissionIndex -= 1;
        StartNewMission();
    }

    public void ResetLevel()
    {
        _currentMission.CleanupMission();
        _currentMission = null;
        SetActiveCannon(null);
        SetLevelState(LevelState.MissionFailed);
        RestartMission();
        HUDController.Instance.CommsBoxMessageAction.Invoke("Level rese<i>T</i>");
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
        SetLevelState(LevelState.MissionVictory);
    }

    public void MissionFailed(Mission mission)
    {
        if (_currentMission != mission) return;
        _currentMission.CleanupMission();
        _currentMission = null;
        SetActiveCannon(null);
        SetLevelState(LevelState.MissionFailed);
    }

    public void SetLevelState(LevelState value)
    {
        if (state == value) return;
        var previousState = state;
        state = value;
        
        switch (state)
        {
            case LevelState.MainMenu:
                SetGameInputActive(false);
                AudioManager.Instance.Pause("AimingMusic");
                AudioManager.Instance.Pause("FlyingMusic");
                AudioManager.Instance.PlayFromBeginning("MenuMusic");
                _currentMissionIndex = -1;
                break;

            case LevelState.MissionVictory:
                SetGameInputActive(false);
                AudioManager.Instance.Pause("AimingMusic");
                AudioManager.Instance.Pause("MenuMusic");
                AudioManager.Instance.PlayFromBeginning("VictoryTheme");
                break;

            case LevelState.MissionFailed:
                SetGameInputActive(false);
                AudioManager.Instance.Pause("AimingMusic");
                AudioManager.Instance.Pause("FlyingMusic");
                AudioManager.Instance.Pause("MenuMusic");
                AudioManager.Instance.PlayFromBeginning("LoseTheme");
                break;

            case LevelState.MissionActive:
                SetGameInputActive(true);
                AudioManager.Instance.Pause("MenuMusic");
                AudioManager.Instance.Pause("VictoryTheme");
                AudioManager.Instance.Pause("LoseTheme");
                AudioManager.Instance.Play("AimingMusic");
                break;
            case LevelState.Credits:
                SetGameInputActive(true);
                break;
        }
        OnLevelStateChanged.Invoke(state, this);
    }

    public void SetGameInputActive(bool value)
    {
        if (value) _inputActionMap?.Enable();
        else _inputActionMap?.Disable();
    }
}
