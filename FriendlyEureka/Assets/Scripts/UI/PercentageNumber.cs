using UnityEngine;

namespace UI
{
    public class PercentageNumber : FillComponent
    {
        [SerializeField]
        private TMPro.TMP_Text _numberText;

        public override void SetValue(float normalized)
        {
            var valueToSet = Mathf.RoundToInt(normalized * 100).ToString("D3");
            if (_numberText.text == valueToSet) return;
            _numberText.text = valueToSet;
        }
    }
}