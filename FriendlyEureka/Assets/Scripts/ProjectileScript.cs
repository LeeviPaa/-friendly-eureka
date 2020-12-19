using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float launchForce;

    public Rigidbody _rb;

    void Awake()
    {
        _rb.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.AddForce(transform.position * launchForce, ForceMode.Force);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
