using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructionTarget : MonoBehaviour
{
    public UnityEvent<bool, DestructionTarget> OnStateChanged = new UnityEvent<bool, DestructionTarget>();

    [SerializeField]
    private List<string> _validDestructionTags = new List<string>() { "Bomb" }; 

    public bool IsActive { get; private set; }
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void SetActive(bool value)
    {
        if (IsActive == value) return;
        IsActive = value;
        OnStateChanged.Invoke(value, this);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(var tag in _validDestructionTags)
        if (other.tag == tag)
        {
            SetActive(false);
            return;
        }
    }


}
