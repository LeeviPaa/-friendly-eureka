using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        private FillComponent _powerMeter;
        public UnityAction<float> PowerChangedAction => _powerMeter.SetValue;

        [SerializeField]
        private CounterWithNumber _boostCounter;
        public UnityAction<int> BoostCountChangedAction => _boostCounter.SetValue;

        [SerializeField]
        private CounterWithNumber _ammoCounter;
        public UnityAction<int> AmmoCountChangedAction => _ammoCounter.SetValue;
    }
}
