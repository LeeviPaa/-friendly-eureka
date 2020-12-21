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

        public static string FirstCharToUppercase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            else if (s.Length <= 1) return char.ToUpper(s[0]).ToString();
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}