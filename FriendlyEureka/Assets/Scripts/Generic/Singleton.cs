using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = (T)FindObjectOfType(typeof(T));
            return;
        }
        Destroy(gameObject);
    }
}
