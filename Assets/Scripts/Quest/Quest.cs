using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/New Quest")]
public class Quest : ScriptableObject
{
    [TextArea]
    public string questDescrption;
    public bool isQuestComplete;
    [SerializeField] public Quest questCondition;
    [SerializeField] public int questID;
    [SerializeField] public CharacterBase activeInStory;
    [SerializeField] public Supply reward;
    [SerializeField] public int gainReward;
    public string GetDescription
    {
        get { return questDescrption; }
        set { questDescrption = value; }
    }
    public int getQuestID 
    {
        get { return questID; }
        set { questID = value; }
    }
}
