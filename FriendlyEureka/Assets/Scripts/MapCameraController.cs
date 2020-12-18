using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCameraController : MonoBehaviour
{
    public static MapCameraController instance;

    public Transform areaOrigin;
    public Vector2 areaSize;

    private void Awake() {
        instance = this;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    private void HandlePlaneMovement() {

    }

    private void HandleDepthMovement() {

    }

    public void ToggleMapCameraMode(bool state) {
        // do map camera activation logic
        
    }
}
