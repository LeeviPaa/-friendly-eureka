using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public static class UIHelpers
    {
        public static void GameObjectSetActive(this GameObject obj, bool state)
        {
            if (obj.activeSelf == state) return;
            obj.SetActive(state);
        }
    }
}