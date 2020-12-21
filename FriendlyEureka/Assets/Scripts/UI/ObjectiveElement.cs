using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ObjectiveElement : MonoBehaviour
    {
        private ObjectiveBase _boundObjective;
        [SerializeField]
        private TMPro.TMP_Text _text;
        [SerializeField]
        private CounterElement _toggle;

        public void Bind(ObjectiveBase objective)
        {
            _boundObjective = objective;
            objective.OnObjectiveUpdate.AddListener(UpdateVisuals);
            UpdateVisuals(objective);
        }

        public void Unbind()
        {
            _boundObjective?.OnObjectiveUpdate.RemoveListener(UpdateVisuals);
            _boundObjective = null;
        }

        public void UpdateVisuals(ObjectiveBase objective)
        {
            _text.text = objective.GetObjectiveMessage();
            _toggle?.SetState(objective.IsComplete);
        }
    }
}
