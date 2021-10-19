using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;

    [SerializeField] public bool battleActive;

    [SerializeField] GameObject battleCamera;

    [SerializeField] public GameObject[] playerPos;
    [SerializeField] public GameObject[] enemyPos;
    [SerializeField] CharacterBase[] playerPrefabs;
    [SerializeField] CharacterBase[] enemyPrefabs;
    [SerializeField] public List<CharacterBase> activePlayer;
    [SerializeField] public List<CharacterBase> activeEnemy;
    [SerializeField] public List<CharacterBase> activeBattlers;

    [SerializeField] public int currentTurn;
    [SerializeField] bool turnWaiting;
    //[SerializeField] bool isPlayerTurn;
    [SerializeField] GameObject uiButtonClose;
    [SerializeField] List<GameObject> activeAnimator;
    [SerializeField] int skillSlotID;
    [SerializeField] SkillHUD _skillSlotID;
    [SerializeField] public bool resetColor;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject questUI;
    [SerializeField] public int xpGain, goldGain, crystalGain;
    [SerializeField] GameObject[] resetImage;
    [SerializeField] GameObject skillHudClose;

    [SerializeField] GameObject gameoverCamera;
    [SerializeField] GameObject group;
    [SerializeField] public bool playerTurn;


    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            BattleStart(new string[] {"Goblin"});
        }*/
        if (battleActive)
        {
            if (turnWaiting)
            {
                if (activeBattlers[currentTurn].IsPlayer)
                {
                    uiButtonClose.SetActive(true);
                }
                else
                {
                    uiButtonClose.SetActive(false);

                    //enemy Attack
                    StartCoroutine(EnemyMoveCo());
                }
            }
            /*if (Input.GetKeyDown(KeyCode.O))
            {
                NextTurn();
            }*/
        }
    }
    public void BattleStart(string[] enemySpawn)
    {
        settingUI.SetActive(false);
        joyUI.SetActive(false);
        questUI.SetActive(false);
        if (!battleActive)
        {
            battleActive = true;
            battleCamera.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].Pos != -1)
                    {
                        if (playerPrefabs[j].Pos == i)
                        {
                            activePlayer.Add(playerPrefabs[j]);
                            playerPos[i].gameObject.SetActive(true);

                            activeAnimator.Add(playerPos[i].transform.GetChild(0).gameObject);

                        }
                    }
                }

            }

            for (int i = 0; i < enemySpawn.Length; i++)
            {
                for(int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].Name == enemySpawn[i])
                    {
                        var newCharacter = ScriptableObject.CreateInstance<CharacterBase>();
                        newCharacter.Name = enemyPrefabs[j].Name;
                        newCharacter.Level = enemyPrefabs[j].Level;
                        newCharacter.PlayerID = enemyPrefabs[j].PlayerID;
                        newCharacter.Pos = i; 
                        newCharacter.Description = enemyPrefabs[j].Description;
                        newCharacter.Sprite = enemyPrefabs[j].Sprite;
                        newCharacter.BattleSprite = enemyPrefabs[j].BattleSprite;
                        newCharacter.Element = enemyPrefabs[j].Element;
                        newCharacter.Role = enemyPrefabs[j].Role;
                        newCharacter.CurrentHp = enemyPrefabs[j].CurrentHp;
                        newCharacter.MaxHp = enemyPrefabs[j].MaxHp;
                        newCharacter.Attack = enemyPrefabs[j].Attack;
                        newCharacter.Defense = enemyPrefabs[j].Defense;
                        newCharacter.SelectedSkills = enemyPrefabs[j].SelectedSkills;
                        newCharacter.IsActivePlayer = enemyPrefabs[j].IsActivePlayer;
                        newCharacter.IsPlayer = enemyPrefabs[j].IsPlayer;
                        newCharacter.PlayerAnimator = enemyPrefabs[j].PlayerAnimator;
                        //newCharacter.hasDied = enemyPrefabs[j].Name;
                        newCharacter.DeadImage = enemyPrefabs[j].DeadImage;
                        Debug.Log(newCharacter.Name);
                        activeEnemy.Add(newCharacter);
                        enemyPos[i].gameObject.SetActive(true);

                        activeAnimator.Add(enemyPos[i].transform.GetChild(0).gameObject);
                    }
                } 
            }

            //Merge Player List and Enemy List
            activeBattlers.AddRange(activePlayer);
            activeBattlers.AddRange(activeEnemy);


            turnWaiting = true;
            //playerTurn = true;
            currentTurn = Random.Range(0,activeBattlers.Count);
        }
          
    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;
        if (activeBattlers[currentTurn].IsPlayer)
        {
            playerTurn = true;
        }
        

        UpdateBattle();
    }

    public void UpdateBattle()
    {
        bool allEnemeiesDead = true;
        bool allPlayersDead = true;
        int playerCount = 0 ;
        int enemyCount = 0;

        for(int i=0; i < activePlayer.Count; i++)
        {
            if (activePlayer[i].CurrentHp <= 0)
            {
                activePlayer[i].CurrentHp = 0;
                activePlayer[i].IsDied = true;
                activePlayer[i].IsActivePlayer = false;
            }
                if (activePlayer[i].IsDied)
                {
                    playerCount++;
                    Debug.Log("player have "+playerCount+" Dead");
                activePlayer[i].Pos = -1;
                }       
        }
        for (int i = 0; i < activeEnemy.Count; i++)
        {
            if (activeEnemy[i].CurrentHp <= 0)
            {
                activeEnemy[i].CurrentHp = 0;
                activeEnemy[i].IsDied = true;
            }
            if (activeEnemy[i].IsDied)
            {
                enemyCount++;
                Debug.Log("enemy have "+enemyCount+" Dead");
            }

        }
        if (playerCount != activePlayer.Count)
        {
            allPlayersDead = false;
        }
        else
        {
            allPlayersDead = true;
        }
        if (enemyCount != activeEnemy.Count)
        {
            allEnemeiesDead = false;
        }
        else
        {
            allEnemeiesDead = true;
        }

            if (allEnemeiesDead || allPlayersDead)
        {
            if (allEnemeiesDead)
            {
                //end battle in victory
                StartCoroutine(EndBattle());
            }
            else
            {
                StartCoroutine(GameOver());
            }

            //battleCamera.SetActive(false);
            //battleActive = false;
        }
        else
        {
            while (activeBattlers[currentTurn].CurrentHp <= 0)
            {
                currentTurn++;
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        //playerTurn = false;
        for (int i = 0; i < group.transform.childCount; i++)
        {
            group.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(3f);
        NextTurn();
    }
    public void EnemyAttack()
    {
        List<int> player = new List<int>();
        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].IsPlayer && activeBattlers[i].CurrentHp > 0)
            {
                player.Add(i);
            }
        }
        int selectedTarget = player[Random.Range(0, player.Count)];

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].SelectedSkills.Count);
        float skillDamage = activeBattlers[currentTurn].SelectedSkills[selectAttack].SkillDamage;


        //DealDamage(selectedTarget, skillDamage);
        StartCoroutine(DealDamage(selectedTarget , skillDamage));
        
    }

    public IEnumerator DealDamage(int target, float skillDamage)
    {
        /*for(int i =0; i < 3; i++)
        {
            uiButtonClose.transform.GetChild(i).GetComponent<Button>().enabled = false;
        }*/
        float atkPower = activeBattlers[currentTurn].Attack;
        float defPower = activeBattlers[target].Defense;
        
        float damageCal = (atkPower / defPower) * skillDamage * ElementCalculate(activeBattlers[currentTurn],activeBattlers[target]);
        Debug.Log("Multiple :  " + ElementCalculate(activeBattlers[currentTurn], activeBattlers[target]));
        int damageToGive = Mathf.RoundToInt(damageCal);

        activeAnimator[currentTurn].GetComponent<Animator>().Play("attack");
        yield return new WaitForSeconds(2.0f);
        Debug.Log(activeBattlers[currentTurn].Name + "is dealing " + damageCal + " (" + damageToGive + ") " + activeBattlers[target].Name);
        activeBattlers[target].CurrentHp -= damageToGive;
        if(activeBattlers[target].CurrentHp < 0)
        {
            activeBattlers[target].CurrentHp = 0;
            
        }
        activeAnimator[target].GetComponent<Animator>().Play("hit");
        yield return new WaitForSeconds(2.0f);
    }  
    public void PlayerAttack(int selectTarget)
    {
        for (int i = 0; i < activeAnimator.Count; i++)
        {
            if (activeAnimator[i].name == "Monster")
            {
                activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", false);
                activeAnimator[i].GetComponent<Button>().enabled = false;
            }
        }

        int setTarget = activeBattlers.Count - activeEnemy.Count;
        selectTarget += setTarget; 
        float skillDamage = activeBattlers[currentTurn].SelectedSkills[skillSlotID].SkillDamage;
        activeBattlers[currentTurn].SelectedSkills[skillSlotID].IsActivated = true;
        StartCoroutine(DealDamage(selectTarget, skillDamage));
        
        StartCoroutine(PlayerCo());
        //NextTurn();
    }
    public void ChooseSkill(int num)
    {
        _skillSlotID = FindObjectsOfType<SkillHUD>()[num];
        skillSlotID = _skillSlotID.skillSlotID;
        Debug.Log(skillSlotID);
        for(int i=0; i < activeAnimator.Count; i++)
        {
            if(activeAnimator[i].name == "Monster")
            {
                activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", true);
                activeAnimator[i].GetComponent<Button>().enabled = true;
            }
        }
    }
    public IEnumerator PlayerCo()
    {
        skillHudClose.SetActive(false);
        resetColor = true;
        yield return new WaitForSeconds(2.0f);
        NextTurn();
        skillHudClose.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        resetColor = false;
    }
    public IEnumerator EndBattle()
    {
        battleActive = false;
        uiButtonClose.SetActive(false);

        yield return new WaitForSeconds(.5f);
        
        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        UIFade.instance.FadeFromBlack();
        battleCamera.SetActive(false);
        for (int i = 0; i < playerPos.Length; i++)
        {
            playerPos[i].SetActive(false);
        }
        for (int i = 0; i < enemyPos.Length; i++)
        {
            enemyPos[i].SetActive(false);
        }
        for(int i = 0; i < resetImage.Length; i++)
        {
            resetImage[i].GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < group.transform.childCount; i++)
        {
            group.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        yield return new WaitForSeconds(.5f);

        activePlayer.Clear();
        activeEnemy.Clear();
        activeBattlers.Clear();
        activeAnimator.Clear();
        currentTurn = 0;
        yield return new WaitForSeconds(1f);
        BattleReward.instance.OpenBattelRewardScreen(xpGain,goldGain,crystalGain);
    }
    public IEnumerator GameOver()
    {
        battleActive = false;
        uiButtonClose.SetActive(false);

        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        UIFade.instance.FadeFromBlack();
        battleCamera.SetActive(false);
        gameoverCamera.SetActive(true);
    }

    public void Revive()
    {
        if (BattleReward.instance.gold.SupplyValue >= 10)
        {
            for (int i = 0; i < playerPrefabs.Length; i++)
            {
                playerPrefabs[i].CurrentHp = playerPrefabs[i].MaxHp;
                playerPrefabs[i].IsDied = false;
            }
            Debug.Log("Revive Success");
            BattleReward.instance.gold.SupplyValue -= 10;
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
        
    }
    public float ElementCalculate(CharacterBase attack ,CharacterBase target )
    {
        ElementType attackSkillElement = ElementType.Normal;
        if (attack.IsPlayer == true)
        {
             attackSkillElement = attack.SelectedSkills[skillSlotID].getElementSkill;
        }
        else
        {
            attackSkillElement = attack.SelectedSkills[0].getElementSkill;
        }
        ElementType targetElement = target.Element;
        if (attackSkillElement == ElementType.Fire && targetElement == ElementType.Wood)
        {
            return 1.5f;
        }else if (attackSkillElement == ElementType.Fire && targetElement == ElementType.Water)
        {
            return 0.5f;
        }
        else if (attackSkillElement == ElementType.Water && targetElement == ElementType.Fire)
        {
            return 1.5f;
        }
        else if (attackSkillElement == ElementType.Water && targetElement == ElementType.Thunder)
        {
            return 0.5f;
        }
        else if (attackSkillElement == ElementType.Wood && targetElement == ElementType.Thunder)
        {
            return 1.5f;
        }
        else if (attackSkillElement == ElementType.Wood && targetElement == ElementType.Fire)
        {
            return 0.5f;
        }
        else if (attackSkillElement == ElementType.Thunder && targetElement == ElementType.Water)
        {
            return 1.5f;
        }
        else if (attackSkillElement == ElementType.Thunder && targetElement == ElementType.Wood)
        {
            return 0.5f;
        }
        return 1.0f;
    }
}

