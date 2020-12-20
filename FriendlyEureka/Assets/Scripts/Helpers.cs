using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static void DestroyAllChildren(this Transform t)
    {
        var count = t.childCount;
        for (var i = count-1; i >= 0; i--)
        {
            Object.Destroy(t.GetChild(i).gameObject);
        }
    }
}
