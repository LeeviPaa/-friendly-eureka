using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    public GameObject launchObject;
    public GameObject cannon;

    private Vector2 _moveInput;
    private bool _fire;
    
    public Transform spawnTransform;
    public float horizRotate = 0.0f;
    public float sensitivityX = 60;
  
    public float vertRotate = -20.0f;
    public float sensitivityY = 50;
  
    void Update()
    {
        CannonControl(_moveInput);
    }

    private void CannonControl(Vector2 input)
    {
        Vector2 move = Vector2.zero;
        
        horizRotate += input.x * sensitivityX * Time.deltaTime;
        horizRotate = Mathf.Clamp(horizRotate, -180, 180);
        // transform.rotation = Quaternion.Euler(0, horizRotate, 0);
        
        vertRotate += input.y * sensitivityY * Time.deltaTime;
        vertRotate = Mathf.Clamp(vertRotate, -90, -5);
        transform.rotation = Quaternion.Euler(vertRotate, horizRotate, 0);
    }

    public void SetMoveInput(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
    }

    public void SetFireInput(InputAction.CallbackContext input)
    {
        _fire = input.ReadValueAsButton();
        Debug.Log("Fire!");
        Instantiate(launchObject, spawnTransform);
    }
}