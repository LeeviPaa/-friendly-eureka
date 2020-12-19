using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapCameraController : MonoBehaviour {
    public static MapCameraController instance;

    public float moveSpeed;
    public float scrollTargetSpeed;
    public float scrollSpeed;
    public Transform areaOrigin;
    public Vector2 areaSize;
    public float panAreaSize;
    public Transform cameraScrollTransform;
    public Vector2 scrollLimits;
    public AnimationCurve scrollSmoothCurve;

    new private Camera camera;
    new private Transform transform;

    private Vector2 moveInput;
    private Vector2 scrollInput;
    private float scrollTarget;

    private void Awake() {
        instance = this;
        camera = Camera.main;
        transform = GetComponent<Transform>();
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start() {
        scrollTarget = cameraScrollTransform.position.y;
    }

    private void Update() {
        HandlePlaneMovement(moveInput);
        HandleDepthMovement(scrollInput);
    }

    private void HandlePlaneMovement(Vector2 input) {
        Vector2 move = Vector2.zero;
        if (input.x > camera.pixelWidth - panAreaSize) {
            move.x = Mathf.InverseLerp(camera.pixelWidth - panAreaSize, camera.pixelWidth, input.x);
        }
        else if (input.x < panAreaSize) {
            move.x = -Mathf.InverseLerp(panAreaSize, 0f, input.x);
        }
        if (input.y > camera.pixelHeight - panAreaSize) {
            move.y = Mathf.InverseLerp(camera.pixelHeight - panAreaSize, camera.pixelHeight, input.y);
        }
        else if (input.y < panAreaSize) {
            move.y = -Mathf.InverseLerp(panAreaSize, 0f, input.y);
        }
        Vector3 pos = transform.position;
        pos.x += move.x * moveSpeed * Time.deltaTime;
        pos.z += move.y * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, areaOrigin.position.x - areaSize.x, areaOrigin.position.x + areaSize.x);
        pos.z = Mathf.Clamp(pos.z, areaOrigin.position.z - areaSize.y, areaOrigin.position.z + areaSize.y);
        transform.position = pos;
    }

    private void HandleDepthMovement(Vector2 input) {
        Vector3 pos = cameraScrollTransform.position;
        scrollTarget -= input.y * scrollTargetSpeed * Time.deltaTime;
        scrollTarget = Mathf.Clamp(scrollTarget, scrollLimits.x, scrollLimits.y);
        print(scrollTarget);
        pos.y += Mathf.Sign(scrollTarget - pos.y) * scrollSmoothCurve.Evaluate(Mathf.Abs(scrollTarget - pos.y)) * scrollSpeed * Time.deltaTime;
        cameraScrollTransform.position = pos;
    }

    public void ToggleMapCameraMode(bool state) {
        // do map camera activation logic, Cinemachine mby?
    }

    public void SetMoveInput(InputAction.CallbackContext input) {
        moveInput = input.ReadValue<Vector2>();
    }

    public void SetScrollInput(InputAction.CallbackContext input) {
        scrollInput = input.ReadValue<Vector2>();
    }
}
