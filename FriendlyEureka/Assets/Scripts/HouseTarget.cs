using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            LevelManager.instance.HouseTargetHit();
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
