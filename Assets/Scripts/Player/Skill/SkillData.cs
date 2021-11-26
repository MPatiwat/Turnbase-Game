using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill" , menuName = "Skill/Create a skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] int skillID;
    [SerializeField] Sprite skillSprite;

    [SerializeField] string skillName;
    //[SerializeField] int skillLevel;
    [SerializeField] float skillDamage;
    [SerializeField] ElementType elementSkill;

    [TextArea]
    [SerializeField] public string skillDescription;

    public bool isUnlocked;
    public bool isSelected;
    public SkillData[] preSkill;
    [SerializeField] int cooldown;
    [SerializeField] int currentCooldown;
    [SerializeField] bool isActivated;
    [SerializeField] string animationName;
    [SerializeField] int crystalRequire;
    [SerializeField] int levelRequire;
    [SerializeField] TargetType type;
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

    public int LevelRequire
    {
        get { return levelRequire; }
        set { levelRequire = value; }
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
    public int CurrentCoolDown
    {
        get { return currentCooldown; }
        set { currentCooldown = value; }
    }
    public string AnimationName
    {
        get { return animationName; }
        set { animationName = value; }
    }
    public int CrystalRequire
    {
        get { return crystalRequire; }
        set { crystalRequire = value; }
    }
    public TargetType getType 
    {
        get { return type; }
        set { type = value; }
    }
    /*public float SkillFormula(CharacterBase character)
    {
        skillDamage = character.Attack;
        return skillDamage;
    }*/
    public enum TargetType 
    { 
        Allies,
        Enemy
    }
}
