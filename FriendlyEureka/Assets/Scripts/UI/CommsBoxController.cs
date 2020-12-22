using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsBoxController : MonoBehaviour
{
    [SerializeField]
    private CommsBox _box;
    [SerializeField]
    private float _messageDuration = 2f;
    private float _startTime  = -2f;
    private bool IsPlaying => _startTime + _messageDuration > Time.unscaledTime;

    Queue<string> _queue = new Queue<string>();

    public void Update()
    {
        if (IsPlaying) return;
        if (_queue.Count <= 0)
        {
            enabled = false;
            _box.gameObject.SetActive(false);
            return;
        }
        StartShowingMessage(_queue.Peek());
        _queue.Dequeue();
        Debug.LogWarning(_queue.Count);
    }

    public void ShowMessage(string message)
    {
        if (IsPlaying)
        {
            Enqueue(message);
            return;
        }
        StartShowingMessage(message);
    }

    public void StartShowingMessage(string message)
    {
        if (enabled == false)
        {
            enabled = true;
        }
        _box.gameObject.SetActive(true);
        _box.SetText(message);
        _startTime = Time.unscaledTime;
    }

    public void Enqueue(string message)
    {
        _queue.Enqueue(message);
    }

    public void OnDisable()
    {
        if (_queue.Count >= 0)
        {
            _queue.Clear();
        }
    }
}
