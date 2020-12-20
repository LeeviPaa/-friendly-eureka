using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FillValue : FillComponent
{
    public UnityEvent<float> OnFillValueChanged = new UnityEvent<float>();

    public override void SetValue(float value) => OnFillValueChanged.Invoke(value);
}
