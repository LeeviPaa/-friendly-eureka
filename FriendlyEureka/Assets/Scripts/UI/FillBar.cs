using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : FillComponent
    {
        [SerializeField]
        private Slider _fillBar;

        public override void SetValue(float normalizedValue)
        {
            if (Mathf.Approximately(_fillBar.normalizedValue, normalizedValue)) return;
            _fillBar.normalizedValue = normalizedValue;
        }
    }
}
