using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SantaCannon : MonoBehaviour
{
    public GameObject launchObjectPrefab;
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
    public GameObject camRoot;

    private float horizRotate;
    private float vertRotate;
    private Santa projectile;
    private bool isActive = false;
    private InputAction actionMove;
    private InputAction actionFire;
    
    // Cannon charge variables
    public AnimationCurve chargeCurve = new AnimationCurve();
    private float _currentPower;
    
    public float minPower = 25;
    public float maxPower = 75;

    
    
    private void Start() {
        horizRotate = Mathf.Clamp(horizRotationRoot.transform.localRotation.eulerAngles.y, horizRotateLimits.x, horizRotateLimits.y);
        vertRotate = Mathf.Clamp(vertRotationRoot.transform.localRotation.eulerAngles.x, vertRotateLimits.x, vertRotateLimits.y);
        InputActionAsset inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        actionMove = inputActions.FindActionMap("Player").FindAction(actionNameMove, true);
        actionFire = inputActions.FindActionMap("Player").FindAction(actionNameFire, true);
        actionFire.started += FireInput;
    }

    private void OnDestroy() {
        if (actionFire != null) {
            actionFire.started -= FireInput;
        }
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        UpdateCurrentPower();
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

        if (horizRotationRoot == vertRotationRoot || !vertRotationRoot || !horizRotationRoot) {
            horizRotationRoot.localRotation = Quaternion.Euler(vertRotate, horizRotate, 0f);
        }
        else {
            horizRotationRoot.localRotation = Quaternion.Euler(0f, horizRotate, 0f);
            vertRotationRoot.localRotation = Quaternion.Euler(vertRotate, 0f, 0f);
        }
    }

    private void InstantiateNextProjectile() {
        projectile = Instantiate(launchObjectPrefab, spawnTransform).GetComponent<Santa>();
        projectile.transform.localRotation = Quaternion.identity;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void SetNextProjectile(Santa santa) {
        projectile = santa;
        santa.transform.SetParent(spawnTransform);
        projectile.transform.localRotation = Quaternion.identity;
        projectile.transform.localPosition = Vector3.zero;
        projectile.rigidbody.isKinematic = true;
    }

    private void FireInput(InputAction.CallbackContext input)
    {
        if (!isActive) {
            return;
        }
        if (input.ReadValueAsButton() && input.phase == InputActionPhase.Started) {
            if (!projectile) {
                InstantiateNextProjectile();
            }
            projectile.rigidbody.isKinematic = false;
            projectile.transform.SetParent(null);
            launchForce = _currentPower;
            projectile.Launch(launchForce);
            
            // Start next gameplay segment (flying to target)
            SetActive(false);
            LevelManager.instance.SantaLaunched(projectile);
            projectile = null;
        }
    }

    public void SetActive(bool state) {
        if (!isActive && state) {
            isActive = true;
            if (directionIndicator) {
                directionIndicator.SetActive(true);
            }
            if (instantiateProjectileReady && !projectile) {
                InstantiateNextProjectile();
            }
            camRoot.SetActive(true);
        }
        else if (isActive && !state) {
            isActive = false;
            if (directionIndicator) {
                directionIndicator.SetActive(false);
            }
            camRoot.SetActive(false);
        }
    }

    public void UpdateCurrentPower()
    {
        _currentPower = Mathf.Lerp(minPower, maxPower, chargeCurve.Evaluate(Time.time));
    }
}