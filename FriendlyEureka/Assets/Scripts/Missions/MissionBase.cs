using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBase : MonoBehaviour
{
    public SantaCannon StartCannon;
    public abstract void BeginMission();

    public void OnComplete()
    {
        //LevelManager.instance.MissionClear(this);
    }

    public void OnMissionFail()
    {
        //LevelManager.instance.MissionFailed(this);
    }
}
