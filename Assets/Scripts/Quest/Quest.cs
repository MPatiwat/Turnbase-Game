using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/New Quest")]
public class Quest : ScriptableObject
{
    [TextArea]
    public string questDescrption;
    public bool isQuestComplete;

    public string GetDescription
    {
        get { return questDescrption; }
        set { questDescrption = value; }
    }
}
