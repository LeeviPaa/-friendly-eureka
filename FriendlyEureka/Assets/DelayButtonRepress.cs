using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayButtonRepress : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    private float _startTime;

    public void Awake()
    {
        enabled = false;
    }

    public void Delay()
    {
        enabled = true;
        _button.interactable = false;
        _startTime = Time.time;
    }

    public void Update()
    {
        if (_startTime + 0.5f > Time.unscaledTime) return;
        _button.interactable = true;
        enabled = false;
    }

    public void OnDisable()
    {
        if (!_button.interactable && _startTime > 0.1f && _startTime + 0.5f > Time.unscaledTime)
        {
            _button.interactable = true;
        }
    }
}
