using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public SantaCannon startCannon;

    [System.NonSerialized]
    public LevelState state;

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

    public void SantaLaunched(Santa santa) {
        // set cinemachine from cannon/house to player
        // other effects?

    }

    public void HouseTargetHit(HouseTarget target) {
        // set cinemachine from player to house
        // other effects?
    }
}
