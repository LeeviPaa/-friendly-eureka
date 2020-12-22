using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Santa : MonoBehaviour
{
    public GameObject camRoot;
    public GameObject modelRoot;
    public GameObject ElfPrefab;
    public SantaCameraController CameraController;
    public static Santa TheSanta;

    private InputAction actionFire;
    [SerializeField]
    private string actionNameFire = "Fire";
    [SerializeField]
    private int _currentLaunches = 3;
    [SerializeField]
    private int _maxLaunches = 3;
    public UnityEvent<int> BoostChangedEvent = new UnityEvent<int>();
    private float _rotationSpeedToForwardMin = 2f;
    private float _rotationSpeedToForwardMax = 100f;

    private float _boostTime = 0;

    [NonSerialized]
    new public Rigidbody rigidbody;
    public bool IsLaunched = false;
    public bool IsGrounded = false;

    void Awake()
    {
        if (TheSanta != null)
        {
            Destroy(TheSanta.gameObject);
        }
        TheSanta = this;
        rigidbody = GetComponent<Rigidbody>();
        InputActionAsset inputActions = PlayerInput.GetPlayerByIndex(0).actions;
        actionFire = inputActions.FindActionMap("Player").FindAction(actionNameFire, true);
        actionFire.started += FireInput;
    }
    void OnDestroy()
    {
        actionFire.started -= FireInput;
    }

    void FireInput(InputAction.CallbackContext input)
    {
        if (!IsLaunched || _currentLaunches <= 0)
        {
            return;
        }
        if (input.ReadValueAsButton() && input.phase == InputActionPhase.Started)
        {
            _boostTime = Time.realtimeSinceStartup;
            _currentLaunches -= 1;
            var currentForce = rigidbody.velocity.magnitude;
            rigidbody.velocity = Vector3.zero;
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            var newDirection = ray.direction;
            rigidbody.AddForce(newDirection * (currentForce + (IsGrounded ? 10f : 0f)), ForceMode.Impulse);
            UpdateUI();
        }
    }
    
    // Elf disposing is in a need of a button!

    public void Launch(float forwardForce)
    {
        IsLaunched = true;
        _currentLaunches = _maxLaunches;
        BoostChangedEvent.AddListener(HUDController.Instance.BoostCountChangedAction);
        UpdateUI();
        if (!modelRoot.activeSelf) {
            modelRoot.SetActive(true);
        }
        rigidbody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        RotateCameraToForwardDirection();
        camRoot.SetActive(true);
        _boostTime = Time.realtimeSinceStartup;
        //Destroy(gameObject, 2f);
    }

    public void RotateCameraToForwardDirection()
    {
        CameraController.RotateToForwardDirection(rigidbody.velocity.normalized);
    }

    public void UpdateUI()
    {
        BoostChangedEvent.Invoke(_currentLaunches);
    }

    public void TheOppositeOfLaunch(bool hideModel) {
        camRoot.SetActive(false);
        modelRoot.SetActive(false);
        IsLaunched = false;
        BoostChangedEvent.RemoveListener(HUDController.Instance.BoostCountChangedAction);
    }

    public void Update()
    {
        if (IsLaunched)
        {
            Time.timeScale = Mathf.Lerp(0.5f, 1f, Mathf.Clamp01(Time.realtimeSinceStartup - _boostTime));
        }
        var velocity = rigidbody.velocity;
        if (IsGrounded || velocity.magnitude < 15f) return;
        var lerpAmount = Mathf.Lerp(_rotationSpeedToForwardMin, _rotationSpeedToForwardMax, Mathf.Clamp01(velocity.magnitude / 40));
        transform.forward = Vector3.Lerp(transform.forward, velocity.normalized, Time.deltaTime * lerpAmount);
    }

    public void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }
}
