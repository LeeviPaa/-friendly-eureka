using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfBombScript : MonoBehaviour
{
    public Collider bombHitBox;
    public Collider explosionHitBox;
    
    public float explosionTimer;
    
    private float _explosionTimer;

    public void Awake()
    {
        explosionHitBox.enabled = false;

        _explosionTimer = explosionTimer;
    }
    public void Update()
    {
        _explosionTimer -= Time.deltaTime;
        if (_explosionTimer >= 0)
        {
            Detonate();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        { 
            Debug.Log("Boom!");
        }
    }

    public void Detonate()
    {
        explosionHitBox.enabled = true;
    }
}
