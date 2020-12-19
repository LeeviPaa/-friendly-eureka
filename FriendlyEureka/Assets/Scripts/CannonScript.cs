using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    public GameObject launchObject;
    public float launchForce;
    //public GameObject cannon;

    private Vector2 _moveInput;
    
    public Transform spawnTransform;
    public Transform horizRotationRoot;
    public Transform vertRotationRoot;
    public Vector2 horizRotateLimits;
    public Vector2 vertRotateLimits;
    public float horizSensitivity = 60f;
    public float vertSensitivity = 50f;

    private float horizRotate;
    private float vertRotate;
    private ProjectileScript nextProjectile;

    private void Start() {
        horizRotate = Mathf.Clamp(horizRotationRoot.transform.localRotation.eulerAngles.y, horizRotateLimits.x, horizRotateLimits.y);
        vertRotate = Mathf.Clamp(vertRotationRoot.transform.localRotation.eulerAngles.x, vertRotateLimits.x, vertRotateLimits.y);
        InstantiateNextProjectile();
    }

    void Update()
    {
        CannonControl(_moveInput);
    }

    private void CannonControl(Vector2 input)
    {
        Vector2 move = Vector2.zero;

        horizRotate += input.x * horizSensitivity * Time.deltaTime;
        horizRotate = Mathf.Clamp(horizRotate, horizRotateLimits.x, horizRotateLimits.y);

        vertRotate += input.y * vertSensitivity * Time.deltaTime;
        vertRotate = Mathf.Clamp(vertRotate, vertRotateLimits.x, vertRotateLimits.y);

        horizRotationRoot.localRotation = Quaternion.Euler(0f, horizRotate, 0f);
        vertRotationRoot.localRotation = Quaternion.Euler(vertRotate, 0f, 0f);
    }

    private void InstantiateNextProjectile() {
        nextProjectile = Instantiate(launchObject, spawnTransform).GetComponent<ProjectileScript>();
        nextProjectile.transform.localRotation = Quaternion.identity;
        nextProjectile.rigidbody.isKinematic = true;
    }

    public void SetMoveInput(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
    }

    public void SetFireInput(InputAction.CallbackContext input)
    {
        if (input.ReadValueAsButton() && input.phase == InputActionPhase.Started && nextProjectile) {
            Debug.Log("Fire!");
            nextProjectile.rigidbody.isKinematic = false;
            nextProjectile.transform.SetParent(null);
            nextProjectile.Launch(launchForce);
            // Start next gameplay segment (flying to target)
        }
    }
}