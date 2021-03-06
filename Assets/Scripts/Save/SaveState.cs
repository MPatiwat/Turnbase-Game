using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveState 
{
    public int gold;
    public int exp;
    public int crystal;

    public float playerPosX;
    public float playerPosY;

    //Camera Position
    public float minCamX;
    public float minCamY;
    public float maxCamX;
    public float maxCamY;

    //Character
    public int[] level = new int[5];
    public int[] pos = new int[5];
    public int[] currentHp = new int[5];
    public int[] maxHp = new int[5];
    public int[] atk = new int[5];
    public int[] def = new int[5];
    public bool[] isActive= new bool[5];
    public bool[] isDied = new bool[5];
    public bool[] isActiveInStory = new bool[5];

    //Skill
    public bool[] skillIsUnlock = new bool[100];
    public bool[] skillIsSelected = new bool[100];
    public string[] skillUnlock = new string[100000];
    public string[] skillSelected = new string[100000];
    public int[] skillPos = new int[100];
    //public Dictionary<int, int> skillSelected = new Dictionary<int, int>();
    //public SkillData[] selectedSkill = new SkillData[15];

    //Quest
    public int currentQuestID;
    //public bool[] npcClose = new bool[100];
    public string[] npc = new string[100000];

    //Signal
    //public bool[] signalClose = new bool[100];
    public string[] signal = new string[100000];

    //BGM
    public string bgm;

}

