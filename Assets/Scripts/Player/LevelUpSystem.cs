using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    [SerializeField] int currentLevel, maxLevel;

    [SerializeField] int currentExp;
    [SerializeField] public int[] nextLevelExp;

    [SerializeField] CharacterBase player;

    [SerializeField] int currentHp ;
    [SerializeField] int maxHp;
    [SerializeField] int atk;
    [SerializeField] int def;



    private void Start()
    {
        nextLevelExp = new int[maxLevel];
        nextLevelExp[1] = 1000;
        for(int i = 2; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.1f);
        }
    }

    private void Update()
    {
        currentLevel = player.Level;
        currentHp = player.CurrentHp;
        maxHp = player.MaxHp;
        atk = player.Attack;
        def = player.Defense;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddExp();
        }
    }

    public void AddExp()
    {
        currentExp += 200;
        if(currentExp >= nextLevelExp[currentLevel])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        //Up Level
        currentExp -= nextLevelExp[currentLevel];
        player.Level++;
        //Up Hp
        player.CurrentHp += (player.MaxHp - player.CurrentHp);
        player.MaxHp = Mathf.RoundToInt(player.MaxHp * 1.2f);
        //player.MaxHp = currentHp;
        //Up Attack
        player.Attack = Mathf.RoundToInt(player.Attack * 1.2f);
        //player.Attack = atk;
        //Up Defense
        player.Defense = Mathf.RoundToInt(player.Defense * 1.2f);
        //player.Defense = def;
    }

}
