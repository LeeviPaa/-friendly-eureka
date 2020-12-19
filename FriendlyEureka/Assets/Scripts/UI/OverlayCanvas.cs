using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayCanvas : MonoBehaviour {
    public static OverlayCanvas instance;

    public RectTransform markerContainer;

    public float borderSafeArea;

    private List<WorldSpaceOverlayMarker> markers = new List<WorldSpaceOverlayMarker>();
    private Canvas canvas;
    new private RectTransform transform;
    new private Camera camera;

    private void Awake() {
        instance = this;
        transform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
        camera = Camera.main;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start() {
    }

    private void Update() {
        MoveMarkers();
    }

    private void MoveMarkers() {
        Vector3 pos;
        for (int i = 0; i < markers.Count; i++) {
            pos = camera.WorldToScreenPoint(markers[i].tracker.transform.position);
            if (pos.x > camera.pixelWidth - borderSafeArea) {
                // right edge
                markers[i].SetEdgeStatus(SixDirections.Right);
            }
            else if (pos.x < borderSafeArea) {
                // left edge
                markers[i].SetEdgeStatus(SixDirections.Left);
            }
            else if (pos.y > camera.pixelHeight - borderSafeArea) {
                // top edge
                markers[i].SetEdgeStatus(SixDirections.Up);
            }
            else if (pos.y < borderSafeArea) {
                // bottom edge
                markers[i].SetEdgeStatus(SixDirections.Down);
            }
            else {
                // center
                markers[i].SetVisibleStatus();
            }
            pos.x = Mathf.Clamp(pos.x, borderSafeArea, camera.pixelWidth - borderSafeArea);
            pos.y = Mathf.Clamp(pos.y, borderSafeArea, camera.pixelHeight - borderSafeArea);
            markers[i].transform.position = pos;
        }
    }

    public void AddMarker(WorldSpaceOverlayMarkerTracker tracker) {
        WorldSpaceOverlayMarker marker = Instantiate(tracker.markerPrefab, markerContainer);
        tracker.marker = marker;
        marker.tracker = tracker;
        markers.Add(marker);
    }

    public void RemoveMarker(WorldSpaceOverlayMarkerTracker tracker) {
        markers.Remove(tracker.marker);
        Destroy(tracker.marker.gameObject);
    }
}
