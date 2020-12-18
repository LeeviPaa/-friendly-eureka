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
            for (var i = 0; i < currentValue; ++i)
            {
                if (i + 1 >= _list.Count)
                {
                    var newItem = Instantiate(_prefab);
                    _list.Add(newItem);
                }
                _list[i].gameObject.GameObjectSetActive(true);
                _list[i].SetState(i < maxValue);
            }
            var listCount = _list.Count;
            for (var i = currentValue + 1; i < listCount; ++i)
            {
                _list[i].gameObject.GameObjectSetActive(false);
            }
        }
    }
}
