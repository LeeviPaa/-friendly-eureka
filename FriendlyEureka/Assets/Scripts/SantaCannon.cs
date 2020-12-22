using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
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
    public Transform cameraHorizRotationRoot;
    public Transform cameraVertRotationRoot;
    public Vector2 horizRotateLimits;
    public Vector2 vertRotateLimits;
    private float horizSensitivity = 100f;
    public float vertSensitivity = 100f;
    public bool instantiateProjectileReady;
    public GameObject camRoot;

    private float horizRotate;
    private float vertRotate;
    private Santa projectile;
    private bool isActive = false;
    private InputAction actionMove;
    private InputAction actionFire;

    public UnityEvent<float> PowerValueUpdated = new UnityEvent<float>();
    public UnityEvent SantaIsNotHome = new UnityEvent();
    public UnityEvent CannonFired = new UnityEvent();
    
    // Cannon charge variables
    public AnimationCurve chargeCurve = new AnimationCurve();
    private float _currentPower;
    
    public float minPower = 25;
    public float maxPower = 75;

    //Some nicer way to do this?
    private Camera GetMainCamera() => Camera.main;

    private void Start()
    {
        horizRotate = Mathf.Clamp(cameraHorizRotationRoot.transform.localRotation.eulerAngles.y, horizRotateLimits.x, horizRotateLimits.y);
        vertRotate = Mathf.Clamp(cameraVertRotationRoot.transform.localRotation.eulerAngles.x, vertRotateLimits.x, vertRotateLimits.y);
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
        //horizRotate = Mathf.Clamp(horizRotate, horizRotateLimits.x, horizRotateLimits.y);

        vertRotate += input.y * vertSensitivity * Time.deltaTime;
        vertRotate = Mathf.Clamp(vertRotate, vertRotateLimits.x, vertRotateLimits.y);

        //if (horizRotationRoot == vertRotationRoot || !vertRotationRoot || !horizRotationRoot) {
            horizRotationRoot.localRotation = Quaternion.Euler(vertRotate, horizRotate, 0f);
        //}
        //else {
        //    horizRotationRoot.localRotation = Quaternion.Euler(0f, horizRotate, 0f);
        //    vertRotationRoot.localRotation = Quaternion.Euler(vertRotate, 0f, 0f);
        //}
        //Rotate camera
        cameraHorizRotationRoot.localRotation = Quaternion.Euler(0f, horizRotate, 0f);
        cameraVertRotationRoot.localRotation = Quaternion.Euler(vertRotate, 0f, 0f);

        //Rotate cannon
        var ray = GetMainCamera()?.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height/ 2, 0));
        var lookAtPoint = ray.Value.GetPoint(10000f);
        horizRotationRoot.LookAt(new Vector3(lookAtPoint.x, horizRotationRoot.position.y, lookAtPoint.z));
        vertRotationRoot.LookAt(lookAtPoint);
    }

    private void InstantiateNextProjectile() {
        projectile = Instantiate(launchObjectPrefab).GetComponent<Santa>();
        SetNextProjectile(projectile);
    }

    public void SetNextProjectile(Santa santa) {
        projectile = santa;
        santa.transform.SetParent(spawnTransform);
        projectile.transform.localRotation = Quaternion.identity;
        projectile.transform.localPosition = Vector3.zero;
        projectile.transform.localScale = Vector3.one;
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
            CannonFired.Invoke();
            AudioManager.Instance.Play("CannonLaunch");
            AudioManager.Instance.Crossfade("AimingMusic","FlyingMusic");
            projectile.rigidbody.isKinematic = false;
            projectile.transform.SetParent(null);
            launchForce = _currentPower;
            projectile.Launch(launchForce);
            
            // Start next gameplay segment (flying to target)
            SetActive(false);
            LevelManager.instance.SantaLaunched(projectile);
            projectile = null;
            SantaIsNotHome.Invoke();
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

            //Cam gum
            //var mainCam = Camera.main.
            //horizRotate = mainCam.transform.rotation.eulerAngles.y;
            //vertRotate = 0;
            //cameraHorizRotationRoot.localRotation = Quaternion.Euler(0f, horizRotate, 0f);
            //cameraVertRotationRoot.localRotation = Quaternion.Euler(vertRotate, 0f, 0f);
            camRoot.SetActive(true);
            LevelManager.instance.SetActiveCannon(this);
            PowerValueUpdated.AddListener(HUDController.Instance.PowerChangedAction);
        }
        else if (isActive && !state) {
            isActive = false;
            if (directionIndicator) {
                directionIndicator.SetActive(false);
            }
            camRoot.SetActive(false);

            PowerValueUpdated.RemoveListener(HUDController.Instance.PowerChangedAction);
            spawnTransform.DestroyAllChildren();
            SantaIsNotHome.Invoke();
        }
    }

    public void UpdateCurrentPower()
    {
        var normalizedValue = chargeCurve.Evaluate(Mathf.Repeat(Time.time, 1f));
        _currentPower = Mathf.Lerp(minPower, maxPower, normalizedValue);
        PowerValueUpdated.Invoke(normalizedValue);
    }
}