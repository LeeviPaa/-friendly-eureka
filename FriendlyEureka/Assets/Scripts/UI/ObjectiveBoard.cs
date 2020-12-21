using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ObjectiveBoard : MonoBehaviour
    {
        [SerializeField]
        private ObjectiveElement _prefab;
        [SerializeField]
        private List<ObjectiveElement> _list = new List<ObjectiveElement>();

        public void BindObjectives(Mission mission)
        {
            var objectives = mission.Objectives;
            var count = objectives.Count;
            var loopCount = Mathf.Max(count, _list.Count);
            for (var i = 0; i < loopCount; ++i)
            {
                if (i >= _list.Count)
                {
                    var newItem = Instantiate(_prefab, transform);
                    _list.Add(newItem);
                }
                var element = _list[i];
                element.gameObject.GameObjectSetActive(i < count);
                if (count > i)
                {
                    element.Bind(objectives[i]);
                }
            }
        }

        public void UnbindObjectives()
        {
            foreach (var element in _list)
            {
                UnbindObjectives();
            }
        }
    }
}
