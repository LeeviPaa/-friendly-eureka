using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{

    public class CounterElement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _activeVisual;
        [SerializeField]
        private GameObject _inActiveVisual;
        public UnityEvent IsSetActive = new UnityEvent();
        public UnityEvent IsSetInActive = new UnityEvent();

        public void SetState(bool IsActive)
        {
            _activeVisual?.GameObjectSetActive(IsActive);
            _inActiveVisual?.GameObjectSetActive(!IsActive);
        }
    }
}
