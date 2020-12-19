using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HouseTarget : MonoBehaviour
{
    public SantaCannon cannon;

    private Collider col;

    private void Awake() {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            col.enabled = false;
            LevelManager.instance.HouseTargetHit(this);
            Santa santa = other.GetComponentInParent<Santa>();
            santa.TheOppositeOfLaunch(true);
            cannon.SetNextProjectile(santa);
            cannon.SetActive(true);
            //cinemachineCamera.SetActive(true);
            //StartCoroutine(PlayerHitRoutine()); // if needed
        }
    }

    private IEnumerator PlayerHitRoutine() {
        yield return null;
        // move player model to new chimney
        // all the effects and animations
        // start chimney aiming system
    } 
}
