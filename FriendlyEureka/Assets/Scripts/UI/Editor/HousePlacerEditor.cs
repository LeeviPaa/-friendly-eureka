using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(HousePlacer))]
public class HousePlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HousePlacer targetScript = (HousePlacer)target;
        if (GUILayout.Button("Places houses"))
        {
            targetScript.PlaceHouses();
        }

        base.OnInspectorGUI();
    }
}