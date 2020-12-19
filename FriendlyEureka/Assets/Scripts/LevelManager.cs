using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public CannonScript startCannon;

    private void Awake() {
        instance = this;
    }

    private void OnDestroy() {
        instance = null;
    }

    private void Start() {
        SetStartCannonState();
    }

    private void SetStartCannonState() {
        startCannon.SetActive(true);
        // camera, cinemachine? Here or there?
    }
}
