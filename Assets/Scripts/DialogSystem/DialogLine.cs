using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine 
{
    public CharacterBase characterSpeaker;

    [TextArea]
    public string dialog;
}
