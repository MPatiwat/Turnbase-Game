using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharacterBase Base { get; set; }
    public Transform transform;
    public int Level { get; set; }

    public int HP{ get; set; }
    public Character(CharacterBase statBase , int currentLevel)
    {
        Base = statBase;
        Level = currentLevel;
        HP = MaxHp;

    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level)/ 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }
}
