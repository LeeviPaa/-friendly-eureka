using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class HouseTarget : MonoBehaviour
{
    public SantaCannon cannon;
    public bool IsNaughty = false;
    public UnityEvent<bool, HouseTarget> OnNaughtyChanged = new UnityEvent<bool, HouseTarget>();

    private Collider col;

    private void Awake() {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            FindObjectOfType<AudioManager>().Crossfade("FlyingMusic","AimingMusic");
            col.enabled = false;
            LevelManager.instance.HouseTargetHit(this);
            Santa santa = other.GetComponentInParent<Santa>();
            santa.TheOppositeOfLaunch(true);
            cannon.SetNextProjectile(santa);
            cannon.SetActive(true);
            SetIsNaughty(false);
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

    public void SetIsNaughty(bool value)
    {
        IsNaughty = value;
        OnNaughtyChanged.Invoke(value, this);
    }

    public void Reset()
    {
        col.enabled = true;
    }

    public void DelayReset()
    {
        LevelManager.instance.StartCoroutine(DelayResetRoutine(Time.time));
    }

    public IEnumerator DelayResetRoutine(float time)
    {
        while (Time.time < time + 1f) yield return null;
        Reset();
    }
}
