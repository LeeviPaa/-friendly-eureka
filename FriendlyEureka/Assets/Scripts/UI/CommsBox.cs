using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsBox : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    public void SetText(string value)
    {
        _text.text = value;
    }
}
