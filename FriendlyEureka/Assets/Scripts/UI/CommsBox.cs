using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommsBox : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;
    [SerializeField]
    private Animation _anim;

    public void SetText(string value)
    {
        _text.text = value;
        _anim?.Play();
    }
}
