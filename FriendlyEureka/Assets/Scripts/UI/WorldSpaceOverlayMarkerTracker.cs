using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceOverlayMarkerTracker : MonoBehaviour
{
    public WorldSpaceOverlayMarker markerPrefab;

    [System.NonSerialized]
    public WorldSpaceOverlayMarker marker;
    [System.NonSerialized]
    new public Transform transform;

    private void Awake() {
        transform = GetComponent<Transform>();
    }

    private void Start() {
        if (OverlayCanvas.instance)
            OverlayCanvas.instance.AddMarker(this);
    }

    private void OnDestroy() {
        if (OverlayCanvas.instance) {
            OverlayCanvas.instance.RemoveMarker(this);
        }
    }
}
