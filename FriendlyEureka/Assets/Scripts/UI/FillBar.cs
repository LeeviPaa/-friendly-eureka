using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _fillBar;

        public void SetValue(float normalizedValue)
        {
            if (Mathf.Approximately(_fillBar.normalizedValue, normalizedValue)) return;
            _fillBar.normalizedValue = normalizedValue;
        }
    }
}
