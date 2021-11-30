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
        //nextLevelExp[1] = 1;
        for (int i = 1; i <= maxLevel; i++)
        {
            nextLevelExp[i-1] = Mathf.RoundToInt(((i*i*i) * 4)/5);
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
        currentExp += (nextLevelExp[predictLevel] - nextLevelExp[predictLevel - 1]);
        expText.text = currentExp.ToString();
        goldText.text = (predictLevel * 100).ToString();
    }
    public void Minus()
    {
        if (currentExp > 0)
        {
            predictLevel--;
            levelText.text = predictLevel.ToString();
            currentExp -= (nextLevelExp[predictLevel+1] - nextLevelExp[predictLevel]);
            expText.text = currentExp.ToString();
            goldText.text = (predictLevel * 100).ToString();
        }    
    }
    public void Submit()
    {
        if (exp.SupplyValue >= currentExp&&gold.SupplyValue>=predictLevel*100)
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
            character.MaxHp = Mathf.RoundToInt((((((2 * character.BMaxHP) + character.MMaxHP) + Mathf.RoundToInt(Mathf.Sqrt(nextLevelExp[character.Level])/4)) * character.Level) / 100f) + character.Level + 10);
            Debug.Log("Max Hp : " + character.MaxHp);
            //player.MaxHp = currentHp;
            //Up Attack
            character.Attack = Mathf.RoundToInt((((((2 * character.BAttack) + character.MAttack) + Mathf.RoundToInt(Mathf.Sqrt(nextLevelExp[character.Level]) / 4)) * character.Level) / 100f) + 5);
            Debug.Log("Atk : " + character.Attack);
            //player.Attack = atk;
            //Up Defense
            character.Defense = Mathf.RoundToInt((((((2 * character.BDefense) + character.MDefense) + Mathf.RoundToInt(Mathf.Sqrt(nextLevelExp[character.Level]) / 4)) * character.Level) / 100f) + 5);
            Debug.Log("Def : " + character.Defense);
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
