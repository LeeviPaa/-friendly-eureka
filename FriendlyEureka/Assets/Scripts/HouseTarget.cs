using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HouseTarget : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCam;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            LevelManager.instance.HouseTargetHit(this);
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
