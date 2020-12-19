using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    public GameObject camRoot;
    public GameObject modelRoot;

    [NonSerialized]
    new public Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(float forwardForce)
    {
        if (!modelRoot.activeSelf) {
            modelRoot.SetActive(true);
        }
        rigidbody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        camRoot.SetActive(true);
        //Destroy(gameObject, 2f);
    }

    public void TheOppositeOfLaunch(bool hideModel) {
        camRoot.SetActive(false);
        modelRoot.SetActive(false);
    }
}
