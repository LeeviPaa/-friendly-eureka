using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    public GameObject launchObject;
    public float launchForce;
    public GameObject directionIndicator;
    public string actionNameMove;
    public string actionNameFire;
    public Transform spawnTransform;
    public Transform horizRotationRoot;
    public Transform vertRotationRoot;
    public Vector2 horizRotateLimits;
    public Vector2 vertRotateLimits;
    public float horizSensitivity = 60f;
    public float vertSensitivity = 50f;
    public bool instantiateProjectileReady;

    private float horizRotate;
    private float vertRotate;
    private ProjectileScript nextProjectile;
    private bool isActive = false;
    private InputAction actionMove;
    private InputAction actionFire;

    private void Start() {
        horizRotate = Mathf.Clamp(horizRotationRoot.transform.localRotation.eulerAngles.y, horizRotateLimits.x, horizRotateLimits.y);
        vertRotate = Mathf.Clamp(vertRotationRoot.transform.localRotation.eulerAngles.x, vertRotateLimits.x, vertRotateLimits.y);
        InputActionAsset inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        actionMove = inputActions.FindActionMap("Player").FindAction(actionNameMove, true);
        actionFire = inputActions.FindActionMap("Player").FindAction(actionNameFire, true);
        actionFire.started += SetFireInput;
    }

    private void OnDestroy() {
        if (actionFire != null) {
            actionFire.started -= SetFireInput;
        }
    }

    void Update()
    {
        if (!isActive) {
            return;
        }
        CannonControl();
    }

    private void CannonControl()
    {
        Vector2 input = actionMove.ReadValue<Vector2>();
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

    public void SetFireInput(InputAction.CallbackContext input)
    {
        if (!isActive) {
            return;
        }
        if (input.ReadValueAsButton() && input.phase == InputActionPhase.Started) {
            if (!nextProjectile) {
                InstantiateNextProjectile();
            }
            nextProjectile.rigidbody.isKinematic = false;
            nextProjectile.transform.SetParent(null);
            nextProjectile.Launch(launchForce);
            // Start next gameplay segment (flying to target)
        }
    }

    public void SetActive(bool state) {
        if (!isActive && state) {
            isActive = true;
            if (directionIndicator) {
                directionIndicator.SetActive(true);
            }
            if (instantiateProjectileReady && !nextProjectile) {
                InstantiateNextProjectile();
            }
        }
        else if (isActive && !state) {
            isActive = false;
            if (directionIndicator) {
                directionIndicator.SetActive(false);
            }
        }
    }
}