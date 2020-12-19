using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SantaCameraController : MonoBehaviour
{
    private InputAction _actionMove;
    [SerializeField]
    private string _actionMoveName = "Move";

    private float _horizRotate;
    private float _vertRotate;

    public float _horizSensitivity = 60f;
    public float _vertSensitivity = 50f;

    [SerializeField]
    private Transform _cameraRoot;

    public void Start()
    {
        InputActionAsset inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        _actionMove = inputActions.FindActionMap("Player").FindAction(_actionMoveName, true);
    }


    public void Update()
    {
        Vector2 input = _actionMove.ReadValue<Vector2>();
        Vector2 move = Vector2.zero;

        _horizRotate += input.x * _horizSensitivity * Time.deltaTime;

        _vertRotate += input.y * _vertSensitivity * Time.deltaTime;
        _vertRotate = Mathf.Clamp(_vertRotate, -90, 90);

        _cameraRoot.rotation = Quaternion.Euler(_vertRotate, _horizRotate, 0);
        Debug.LogWarning($"VRot = {_vertRotate}, HRot = {_horizRotate}");
    }
}
