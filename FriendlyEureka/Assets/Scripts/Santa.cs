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
        LevelManager.instance.activeSanta = this;
    }

    private void OnDestroy() {
        if (LevelManager.instance.activeSanta == this) {
            LevelManager.instance.activeSanta = null;
        }
    }

    public void Launch(float forwardForce)
    {
        rigidbody.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }
}
