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

    private InputAction actionFire;
    [SerializeField]
    private string actionNameFire = "Fire";
    [SerializeField]
    private int _currentLaunches = 3;
    [SerializeField]
    private int _maxLaunches = 3;
    public UnityEvent<int> BoostChangedEvent = new UnityEvent<int>();

    [NonSerialized]
    new public Rigidbody rigidbody;
    public bool IsLaunched = false;

    void Awake()
    {
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
            _currentLaunches -= 1;
            var currentForce = rigidbody.velocity.magnitude;
            rigidbody.velocity = Vector3.zero;
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            var newDirection = ray.direction;
            rigidbody.AddForce(newDirection * currentForce, ForceMode.Impulse);
            UpdateUI();
        }
    }

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
        camRoot.SetActive(true);
        //Destroy(gameObject, 2f);
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
}
