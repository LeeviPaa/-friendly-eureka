using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectiveBase : MonoBehaviour
{
    public UnityEvent<ObjectiveBase> OnObjectiveUpdate = new UnityEvent<ObjectiveBase>();

    public bool IsOptional;
    public bool IsComplete { get; protected set; }
    public abstract void BeginObjective();
    public abstract void CleanupObjective();
}