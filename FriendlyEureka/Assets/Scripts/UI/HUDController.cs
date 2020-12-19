using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private FillBar _powerMeter;
        public FillBar PowerMeter => _powerMeter;
    }
}
