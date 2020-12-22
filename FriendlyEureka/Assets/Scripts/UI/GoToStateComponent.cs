using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStateComponent : MonoBehaviour
{
    [SerializeField]
    private LevelState _state;

    public void GoToState()
    {
        LevelManager.instance.SetLevelState(_state);
    }
}
