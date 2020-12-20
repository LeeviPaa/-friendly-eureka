using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Counter : MonoBehaviour
    {
        [SerializeField]
        private CounterElement _prefab;

        [SerializeField]
        private List<CounterElement> _list;

        public void SetValue(int currentValue, int maxValue)
        {
            var loopCount = Mathf.Max(maxValue, _list.Count);
            for (var i = 0; i < loopCount; ++i)
            {
                if (i + 1 >= _list.Count)
                {
                    var newItem = Instantiate(_prefab);
                    _list.Add(newItem);
                }
                _list[i].gameObject.GameObjectSetActive(i < maxValue);
                _list[i].SetState(i < currentValue);
            }
        }
    }
}
