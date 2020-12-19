using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    [NonSerialized]
    new public Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(float forwardForce)
    {
        rigidbody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }
}
