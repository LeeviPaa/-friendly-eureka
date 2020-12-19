using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fillBar;

        public void SetValue(float normalizedValue)
        {
            if (Mathf.Approximately(_fillBar.fillAmount, normalizedValue)) return;
            _fillBar.fillAmount = normalizedValue;
        }
    }
}
