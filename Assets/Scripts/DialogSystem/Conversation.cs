using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation"  , menuName = "Dialog/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] private DialogLine[] allLines;
    [SerializeField] string conName;
    [SerializeField] public Quest quest;

    public DialogLine GetDialogLine(int index)
    {
        return allLines[index];
    }

    public int GetLength()
    {
        return allLines.Length - 1;
    }
    public string getName()
    {
        return conName;
    }
    public string GetQuestDescription()
    {
        return quest.GetDescription;
    }
}
