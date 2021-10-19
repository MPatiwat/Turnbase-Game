using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [Header("LevelUI")]
    [SerializeField] Supply gold;
    [SerializeField] Supply exp;
    [SerializeField] GameObject levelUI;
    [SerializeField] bool isLevelActivited;
    [SerializeField] int[] nextLevelExp;

    [Header("CharacterBox")]
    [SerializeField] CharacterBase character;
    [SerializeField] GameObject chaUI;
    [SerializeField] Image characterSprite;
    [SerializeField] TMP_Text levelText;
    [SerializeField] int predictLevel;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text expText;
    [SerializeField] GameObject selectClose;

    [Header("Character Stat")]
    [SerializeField] int currentExp;
    [SerializeField] int currentLevel, maxLevel;
    [SerializeField] int currentHp;
    [SerializeField] int maxHp;
    [SerializeField] int atk;
    [SerializeField] int def;

    public void Level(CharacterBase selectCharacter)
    {
        character = selectCharacter;
        characterSprite.sprite = character.Sprite;
        predictLevel = character.Level;
        levelText.text = predictLevel.ToString();
        currentExp = 0;
        expText.text = currentExp.ToString();
    }
    private void Start()
    {
        nextLevelExp = new int[maxLevel];
        nextLevelExp[1] = 1000;
        for (int i = 2; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.1f);
        }
    }
    private void Update()
    {
        /*predictLevel = character.Level;
        currentLevel = character.Level;
        currentHp = character.CurrentHp;
        maxHp = character.MaxHp;
        atk = character.Attack;
        def = character.Defense;*/
    }
    public void OpenLevel(GameObject level)
    {
        if (isLevelActivited == false)
        {
            isLevelActivited = true;
            levelUI.SetActive(true);
            selectClose.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                chaUI.transform.GetChild(i).GetComponent<Button>().enabled = true;
            }   
        }

    }
    public void CloseFormation(GameObject formation)
    {
        if (isLevelActivited == true)
        {
            isLevelActivited = false;
            formation.SetActive(false);
            selectClose.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                chaUI.transform.GetChild(i).GetComponent<Button>().enabled = false;
                chaUI.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }
    public void Plus()
    {
        predictLevel++;
        levelText.text = predictLevel.ToString();
        currentExp += nextLevelExp[predictLevel - 1];
        expText.text = currentExp.ToString();
    }
    public void Minus()
    {
        if (currentExp > 0)
        {
            predictLevel--;
            levelText.text = predictLevel.ToString();
            currentExp -= nextLevelExp[predictLevel];
            expText.text = currentExp.ToString();
        }    
    }
    public void Submit()
    {
        if (exp.SupplyValue >= currentExp)
        {
            LevelUp();
            currentExp = 0;
            levelText.text = predictLevel.ToString();
            expText.text = currentExp.ToString();
        }
    }
    public void LevelUp()
    {
        exp.SupplyValue -= currentExp;
        while(character.Level!=predictLevel)
        {
            //Up Level
            character.Level++;
            //Up Hp
            character.CurrentHp += (character.MaxHp - character.CurrentHp);
            character.MaxHp = Mathf.RoundToInt(character.MaxHp * 1.2f);
            //player.MaxHp = currentHp;
            //Up Attack
            character.Attack = Mathf.RoundToInt(character.Attack * 1.2f);
            //player.Attack = atk;
            //Up Defense
            character.Defense = Mathf.RoundToInt(character.Defense * 1.2f);
            //player.Defense = def;
        }

    }
    /*public void AddExp()
    {
        currentExp += ;
        if (currentExp >= nextLevelExp[currentLevel])
        {
            LevelUp();
        }
    }*/
}
