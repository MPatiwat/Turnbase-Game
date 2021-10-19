using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleType
{
    [SerializeField] public int rewardEXP;
    [SerializeField] public int rewardGold;
    [SerializeField] public int rewardCrystal;
    [SerializeField] public string[] enemies;
}
