using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill" , menuName = "Skill/Create a skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] int skillID;
    [SerializeField] Sprite skillSprite;

    [SerializeField] string skillName;
    [SerializeField] int skillLevel;
    [SerializeField] float skillDamage;
    [SerializeField] ElementType elementSkill;

    [TextArea]
    [SerializeField] public string skillDescription;

    public bool isUnlocked;
    public bool isSelected;
    public SkillData[] preSkill;
    [SerializeField] int cooldown;
    [SerializeField] bool isActivated;
    public int getSkillID
    {
        get { return skillID; }
        set { skillID = value; }
    }

    public Sprite getSkillSprite
    {
        get { return skillSprite; }
        set { skillSprite = value; }
    }

    public string getSkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }

    public int getSkillLevel
    {
        get { return skillLevel; }
        set { skillLevel = value; }
    }

    public ElementType getElementSkill
    {
        get { return elementSkill; }
        set { elementSkill = value; }
    }

    public string getSkillDescription
    {
        get { return skillDescription; }
        set { skillDescription = value; }
    }

    public float SkillDamage
    {
        get { return skillDamage; }
        set { skillDamage = value; }
    }
    public int CoolDown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public bool IsActivated
    {
        get { return isActivated; }
        set { isActivated = value; }
    }
}
