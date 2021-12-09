using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character" , menuName = "Character/Create new character")]
public class CharacterBase : ScriptableObject
{
    //public static CharacterBase instance;
    public CharacterBase(CharacterBase newCharacter)
    {
        newCharacter.Name = characterName;
        newCharacter.level = level;
        newCharacter.playerID = playerID;
        newCharacter.pos = pos;
        newCharacter.description = description;
        newCharacter.image = image;
        newCharacter.battleImage = battleImage;
        newCharacter.element = element;
        newCharacter.role = role;
        newCharacter.currentHp = currentHp;
        newCharacter.maxHp = maxHp;
        newCharacter.attack = attack;
        newCharacter.defense = defense;
        newCharacter.seletedSkills = seletedSkills;
        newCharacter.isActivePlayer = isActivePlayer;
        newCharacter.isPlayer = isPlayer;
        newCharacter.playerAnimator = playerAnimator;
        newCharacter.hasDied = hasDied;
        newCharacter.deadImage = deadImage;
    }
    [SerializeField] string characterName;
    [SerializeField] int level;
    [SerializeField] int playerID;
    [SerializeField] int pos;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite image;
    [SerializeField] Sprite battleImage;
    [SerializeField] Sprite elementSrpite;

    [SerializeField] ElementType element;
    [SerializeField] Role role;

    [Header("Base Stat")]
    [SerializeField] int currentHp;
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;

    [SerializeField] List<SkillData> seletedSkills;
    [SerializeField] bool isActivePlayer;
    [SerializeField] bool isPlayer;
    [SerializeField] RuntimeAnimatorController playerAnimator;
    [SerializeField] bool hasDied;
    [SerializeField] Sprite deadImage;
    [SerializeField] bool isActiveInStory; 

    [Header("Stat For Level Up")]
    [SerializeField] int b_maxHp;
    [SerializeField] int b_attack;
    [SerializeField] int b_defense;

    [Header("Stat Multiplier")]
    [SerializeField] int m_maxHp;
    [SerializeField] int m_attack;
    [SerializeField] int m_defense;

    public string Name
    {
        get { return characterName; }
        set { characterName = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public Sprite Sprite
    {
        get { return image ; }
        set { image = value; }
    }

    public Sprite BattleSprite
    {
        get { return battleImage; }
        set { battleImage = value; }
    }
    public Sprite ElementSprite
    {
        get { return elementSrpite; }
        set { elementSrpite = value; }
    }

    public ElementType Element
    {
        get { return element; }
        set { element = value; }
    }

    public Role Role
    {
        get { return role; }
        set { role = value; }
    }
    public int CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public List<SkillData>SelectedSkills
    {
        get { return seletedSkills; }
        set { seletedSkills = value; }
    }

    public int PlayerID
    {
        get { return playerID; }
        set { playerID = value; }
    }
    
    public bool IsActivePlayer
    {
        get { return isActivePlayer; }
        set { isActivePlayer = value; }
    }

    public int Pos
    {
        get { return pos; }
        set { pos = value; }
    }
    public bool IsPlayer
    {
        get { return isPlayer; }
        set { isPlayer = value; }
    }

    public RuntimeAnimatorController PlayerAnimator
    {
        get { return playerAnimator; }
        set { playerAnimator = value; }
    }

    public Sprite DeadImage
    {
        get { return deadImage; }
        set { deadImage = value; }
    }
    public bool IsDied
    {
        get { return hasDied; }
        set { hasDied = value; }
    }
    public bool IsActiveInStory
    {
        get { return isActiveInStory; }
        set { isActiveInStory = value; }
    }
    public int BMaxHP
    {
        get { return b_maxHp; }
        set { b_maxHp = value; }
    }
    public int BAttack
    {
        get { return b_attack; }
        set { b_attack = value; }
    }
    public int BDefense
    {
        get { return b_defense; }
        set { b_defense = value; }
    }
    public int MMaxHP
    {
        get { return m_maxHp; }
        set { m_maxHp = value; }
    }
    public int MAttack
    {
        get { return m_attack; }
        set { m_attack = value; }
    }
    public int MDefense
    {
        get { return m_defense; }
        set { m_defense = value; }
    }
    /*public CharacterSaveData GetSaveData()
    {
        var savedata = new CharacterSaveData();
        savedata.level = level;
        savedata.pos = pos;
        savedata.currentHp = currentHp;
        savedata.maxHp = maxHp;
        savedata.atk = attack;
        savedata.def = defense;
        savedata.isActive = isActivePlayer;
        savedata.isDied = hasDied;
        savedata.isActiveInStory = isActiveInStory;
        return savedata;
    }*/
}

/*public class LearnableSkill
{
    [SerializeField] SkillData skill;
    [SerializeField] int level;

    public SkillData getSkill
    {
        get { return skill; }
    }

    public int getLevel
    {
        get { return level; }
    }
}*/

public enum ElementType
{
    Fire,
    Water,
    Wood,
    Thunder,
    Normal
}
public enum Role
{
    DPS,
    Sup,
    Buff,
    Tank
}
/*[System.Serializable]
public class CharacterSaveData
{
    public int level = CharacterBase.instance.Level;
    public int pos = CharacterBase.instance.Pos;
    public int currentHp = CharacterBase.instance.CurrentHp;
    public int maxHp = CharacterBase.instance.MaxHp;
    public int atk = CharacterBase.instance.Attack;
    public int def = CharacterBase.instance.Defense;
    public bool isActive = CharacterBase.instance.IsActivePlayer;
    public bool isDied = CharacterBase.instance.IsDied;
    public bool isActiveInStory = CharacterBase.instance.IsActiveInStory;
}*/

