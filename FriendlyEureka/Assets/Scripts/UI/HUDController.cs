using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI
{
    public class HUDController : MonoBehaviour
    {
        public static HUDController Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        [SerializeField]
        private GameObject _gameplayHUD;

        [SerializeField]
        private GameObject _victoryHUD;

        [SerializeField]
        private GameObject _missionFailedHUD;

        [SerializeField]
        private FillComponent _powerMeter;
        public UnityAction<float> PowerChangedAction => _powerMeter.SetValue;

        [SerializeField]
        private CounterWithNumber _boostCounter;
        public UnityAction<int> BoostCountChangedAction => _boostCounter.SetValue;

        [SerializeField]
        private CounterWithNumber _ammoCounter;
        public UnityAction<int> AmmoCountChangedAction => _ammoCounter.SetValue;

        [SerializeField]
        private CommsBoxController _commsBoxController;
        public UnityAction<string> CommsBoxMessageAction => _commsBoxController.ShowMessage;

        [Header("NaughtyMissionUI")]
        [SerializeField]
        private Counter _naughtyCounter;

        public void SetMissionHUD(Mission mission)
        {
            _naughtyCounter.gameObject.GameObjectSetActive(false);
            foreach (var obj in mission.Objectives)
            {
                var type = obj.GetType();
                if (type == typeof(NaughtyObjective))
                {
                    var naughtyObjective = (NaughtyObjective)obj;
                    naughtyObjective.OnNaughtyCountUpdated.AddListener(_naughtyCounter.SetValue);
                    _naughtyCounter.gameObject.GameObjectSetActive(true);
                }
            }
        }

        public void LevelStateChanged(LevelState state, LevelManager levelManager)
        {
            _gameplayHUD.GameObjectSetActive(state == LevelState.MissionActive);
            _victoryHUD.GameObjectSetActive(state == LevelState.MissionVictory);
            _missionFailedHUD.GameObjectSetActive(state == LevelState.MissionFailed);
        }
    }
}
