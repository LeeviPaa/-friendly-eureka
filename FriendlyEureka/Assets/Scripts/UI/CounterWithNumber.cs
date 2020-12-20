using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CounterWithNumber : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text _numberText;
        [SerializeField]
        private Counter _counter;
        [SerializeField]
        private int _maxIcons = 4;

        public void SetValue(int value)
        {
            _numberText.text = value.ToString("D3");
            _counter.SetValue(value, _maxIcons);
        }
    }
}
