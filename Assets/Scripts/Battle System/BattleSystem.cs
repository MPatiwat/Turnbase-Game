using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;

    [SerializeField] public bool battleActive;

    [SerializeField] GameObject battleCamera;

    [SerializeField] public GameObject[] playerPos;
    [SerializeField] public GameObject[] enemyPos;
    [SerializeField] public CharacterBase[] playerPrefabs;
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
    [SerializeField] List<SkillData> skillCooldown;
    [SerializeField] GameObject[] coolDownText;
    [SerializeField] GameObject encounterField;
    [SerializeField] int selectAttack;
    [SerializeField] GameObject groupOfStatue;
    [SerializeField] Conversation falseToRevive;
    [SerializeField] Conversation successToRevive;

    [SerializeField] public List<SkillData> saveSkill;
    /*[SerializeField] int enemyTurn;
    [SerializeField] bool playerTurn;*/


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
                            var newCharacter = ScriptableObject.CreateInstance<CharacterBase>();
                            newCharacter.Name = playerPrefabs[j].Name;
                            newCharacter.Level = playerPrefabs[j].Level;
                            newCharacter.PlayerID = playerPrefabs[j].PlayerID;
                            newCharacter.Pos = i;
                            newCharacter.Description = playerPrefabs[j].Description;
                            newCharacter.Sprite = playerPrefabs[j].Sprite;
                            newCharacter.BattleSprite = playerPrefabs[j].BattleSprite;
                            newCharacter.Element = playerPrefabs[j].Element;
                            newCharacter.Role = playerPrefabs[j].Role;
                            newCharacter.CurrentHp = playerPrefabs[j].CurrentHp;
                            newCharacter.MaxHp = playerPrefabs[j].MaxHp;
                            newCharacter.Attack = playerPrefabs[j].Attack;
                            newCharacter.Defense = playerPrefabs[j].Defense;
                            newCharacter.SelectedSkills = playerPrefabs[j].SelectedSkills;
                            newCharacter.IsActivePlayer = playerPrefabs[j].IsActivePlayer;
                            newCharacter.IsPlayer = playerPrefabs[j].IsPlayer;
                            newCharacter.PlayerAnimator = playerPrefabs[j].PlayerAnimator;
                            //newCharacter.hasDied = enemyPrefabs[j].Name;
                            newCharacter.DeadImage = playerPrefabs[j].DeadImage;
                            Debug.Log(newCharacter.Name);
                            activePlayer.Add(newCharacter);
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
                        newCharacter.CurrentHp = Random.Range(enemyPrefabs[j].CurrentHp-2, enemyPrefabs[j].CurrentHp + 2);
                        newCharacter.MaxHp = newCharacter.CurrentHp;
                        newCharacter.Attack = Random.Range(enemyPrefabs[j].Attack-2, enemyPrefabs[j].Attack + 2);
                        newCharacter.Defense = Random.Range(enemyPrefabs[j].Defense-2, enemyPrefabs[j].Defense + 2);
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
            /*if (activeBattlers[currentTurn].IsPlayer)
            {
                playerTurn = true;
            }
            else
            {
                enemyTurn = activeEnemy.Count;
            } */  
        }
          
    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
            
        }
        /*if (activeBattlers[currentTurn].IsPlayer)
        {
            playerTurn = true;
        }
        else
        {
            playerTurn = false;
        }
        if (playerTurn == true)
        {
            enemyTurn = 0;
        }
        else
        {
            enemyTurn = activeEnemy.Count;
        }*/
        turnWaiting = true;
        
        /*for(int i = 0; i< skillCooldown.Count; i++)
        {
            if (skillCooldown[i].CurrentCoolDown == 0)
            {
                skillCooldown.Remove(skillCooldown[i]);
            }
        }*/
        
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
            for (int i = 0; i < skillCooldown.Count; i++)
            {
                if (activeBattlers[currentTurn].SelectedSkills.Contains(skillCooldown[i]))
                {
                    if (skillCooldown[i].CurrentCoolDown > 0&&skillCooldown[i].IsActivated==true)
                    {
                        skillCooldown[i].CurrentCoolDown--;
                    }
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
        //enemyTurn--;
        yield return new WaitForSeconds(3f);
        NextTurn();
    }
    public void EnemyAttack()
    {
        selectAttack = Random.Range(0, activeBattlers[currentTurn].SelectedSkills.Count);
        if(activeBattlers[currentTurn].SelectedSkills[selectAttack].getType == SkillData.TargetType.Allies)
        {
            Debug.Log("buff");
            List<int> enemy = new List<int>();
            for (int i = 0; i < activeBattlers.Count; i++)
            {
                if (activeBattlers[i].IsPlayer == false && activeBattlers[i].CurrentHp > 0)
                {
                    enemy.Add(i);
                }
            }
            int selectedEnemies = enemy[Random.Range(0, enemy.Count)];
            float statGain = activeBattlers[currentTurn].SelectedSkills[selectAttack].SkillDamage;
            StartCoroutine(BuffStat(selectedEnemies,statGain));
        }
        else
        {
            Debug.Log("atk");
            List<int> player = new List<int>();
            for (int i = 0; i < activeBattlers.Count; i++)
            {
                if (activeBattlers[i].IsPlayer && activeBattlers[i].CurrentHp > 0)
                {
                    player.Add(i);
                }
            }
            int selectedTarget = player[Random.Range(0, player.Count)];
            float skillDamage = activeBattlers[currentTurn].SelectedSkills[selectAttack].SkillDamage;

            StartCoroutine(DealDamage(selectedTarget, skillDamage));
        }  
    }

    public IEnumerator DealDamage(int target, float skillDamage)
    {
        /*for(int i =0; i < 3; i++)
        {
            uiButtonClose.transform.GetChild(i).GetComponent<Button>().enabled = false;
        }*/
        float atkPower = activeBattlers[currentTurn].Attack;
        float defPower = activeBattlers[target].Defense;

        //float damageCal = (atkPower / defPower) * skillDamage * ElementCalculate(activeBattlers[currentTurn],activeBattlers[target]);
        float damageCal = ((((((2 * activeBattlers[currentTurn].Level) / 5) + 2) * skillDamage * (atkPower/defPower))/50) + 2) * ElementCalculate(activeBattlers[currentTurn], activeBattlers[target]);
        Debug.Log("Multiple :  " + ElementCalculate(activeBattlers[currentTurn], activeBattlers[target]));
        int damageToGive = Mathf.RoundToInt(damageCal);
        if (activeBattlers[currentTurn].IsPlayer)
        {
            activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName);
            Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[skillSlotID].getSkillName + " dealing " + damageCal + " (" + damageToGive + ") " + activeBattlers[target].Name);
        }
        else
        {
            activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName);
            Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[selectAttack].getSkillName + " dealing " + damageCal + " (" + damageToGive + ") " + activeBattlers[target].Name);
        }
        
        yield return new WaitForSeconds(2.0f);
        //Debug.Log(activeBattlers[currentTurn].Name +" use " + activeBattlers[currentTurn].SelectedSkills[skillSlotID].getSkillName + " is dealing " + damageCal + " (" + damageToGive + ") " + activeBattlers[target].Name);
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
        //activeBattlers[currentTurn].SelectedSkills[skillSlotID].CurrentCoolDown = activeBattlers[currentTurn].SelectedSkills[skillSlotID].CoolDown;
        if (!skillCooldown.Contains(activeBattlers[currentTurn].SelectedSkills[skillSlotID]))
        {
            skillCooldown.Add(activeBattlers[currentTurn].SelectedSkills[skillSlotID]);
        }
        StartCoroutine(DealDamage(selectTarget, skillDamage));
        
        StartCoroutine(PlayerCo());
        //NextTurn();
    }
    public void PlayerBuff(int selectTarget)
    {
        for (int i = 0; i < activeAnimator.Count; i++)
        {
            if (activeAnimator[i].name == "Player")
            {
                activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", false);
                activeAnimator[i].GetComponent<Button>().enabled = false;
            }
        }
        float statGain = activeBattlers[currentTurn].SelectedSkills[skillSlotID].SkillDamage;
        activeBattlers[currentTurn].SelectedSkills[skillSlotID].IsActivated = true;
        //activeBattlers[currentTurn].SelectedSkills[skillSlotID].CurrentCoolDown = activeBattlers[currentTurn].SelectedSkills[skillSlotID].CoolDown;
        if (!skillCooldown.Contains(activeBattlers[currentTurn].SelectedSkills[skillSlotID]))
        {
            skillCooldown.Add(activeBattlers[currentTurn].SelectedSkills[skillSlotID]);
        }
        StartCoroutine(BuffStat(selectTarget, statGain));

        StartCoroutine(PlayerCo());

    }
    public IEnumerator BuffStat(int target, float statGain)
    {
        if(activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName == "Atk Buff" || activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName == "Atk Buff")
        {
            float atkPower = activeBattlers[target].Attack;
            float atkBuff = atkPower * statGain;
            activeBattlers[target].Attack += Mathf.RoundToInt(atkBuff);

            if (activeBattlers[currentTurn].IsPlayer)
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName );
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[skillSlotID].getSkillName + " Boost " + atkBuff + " To " + activeBattlers[target].Name);
            }
            else
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName);
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[selectAttack].getSkillName + " Boost " + atkBuff + " To " + activeBattlers[target].Name);
            }  
        }else if (activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName == "Def Buff" || activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName == "Def Buff")
        {
            float defPower = activeBattlers[target].Defense;
            float defBuff = defPower * statGain;
            activeBattlers[target].Attack += Mathf.RoundToInt(defBuff);

            if (activeBattlers[currentTurn].IsPlayer)
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName);
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[skillSlotID].getSkillName + " Boost " + defBuff + " To " + activeBattlers[target].Name);
            }
            else
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName);
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[selectAttack].getSkillName + " Boost " + defBuff + " To " + activeBattlers[target].Name);
            }
        }else if (activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName == "Heal"|| activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName == "Heal")
        {
            float maxHp = activeBattlers[target].MaxHp;
            float heal = maxHp * statGain;
            activeBattlers[target].CurrentHp += Mathf.RoundToInt(heal);
            if (activeBattlers[target].CurrentHp > activeBattlers[target].MaxHp)
            {
                activeBattlers[target].CurrentHp = activeBattlers[target].MaxHp;
            }

            if (activeBattlers[currentTurn].IsPlayer)
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[skillSlotID].AnimationName);
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[skillSlotID].getSkillName + " Boost " + heal + " To " + activeBattlers[target].Name);
            }
            else
            {
                activeAnimator[currentTurn].GetComponent<Animator>().Play(activeBattlers[currentTurn].SelectedSkills[selectAttack].AnimationName);
                Debug.Log(activeBattlers[currentTurn].Name + " use " + activeBattlers[currentTurn].SelectedSkills[selectAttack].getSkillName + " Boost " + heal + " To " + activeBattlers[target].Name);
            }
        }
        yield return new WaitForSeconds(2.0f);
        
        activeAnimator[target].GetComponent<Animator>().Play("hit");
        yield return new WaitForSeconds(2.0f);
    }
    public void ChooseSkill(int num)
    {
        _skillSlotID = FindObjectsOfType<SkillHUD>()[num];
        skillSlotID = _skillSlotID.skillSlotID;
        Debug.Log(skillSlotID);
        if (_skillSlotID.skillslot.getType == SkillData.TargetType.Allies)
        {
            for (int i = 0; i < activeAnimator.Count; i++)
            {
                if (activeAnimator[i].name == "Player")
                {
                    activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", true);
                    activeAnimator[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", false);
                    activeAnimator[i].GetComponent<Button>().enabled = false;
                }
            }
        }else if (_skillSlotID.skillslot.getType == SkillData.TargetType.Enemy)
        {
            for (int i = 0; i < activeAnimator.Count; i++)
            {
                if (activeAnimator[i].name == "Monster")
                {
                    activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", true);
                    activeAnimator[i].GetComponent<Button>().enabled = true;
                }
                else
                {
                    activeAnimator[i].GetComponent<Animator>().SetBool("isChooseSkill", false);
                    activeAnimator[i].GetComponent<Button>().enabled = false;
                }
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
        for (int i = 0; i< skillCooldown.Count; i++)
        {
            skillCooldown[i].IsActivated = false;
            skillCooldown[i].CurrentCoolDown = skillCooldown[i].CoolDown;
        }
        for(int i = 0; i < coolDownText.Length; i++)
        {
            coolDownText[i].GetComponent<Button>().enabled = true;
            coolDownText[i].transform.GetChild(0).gameObject.SetActive(false);        }
        yield return new WaitForSeconds(.5f);

        for(int i = 0; i < activePlayer.Count; i++)
        {
            for(int j = 0; j < playerPrefabs.Length; j++)
            {
                if (activePlayer[i].Name == playerPrefabs[j].Name)
                {
                    playerPrefabs[j].CurrentHp = activePlayer[i].CurrentHp;
                }
            }
        }
        activePlayer.Clear();
        activeEnemy.Clear();
        activeBattlers.Clear();
        activeAnimator.Clear();
        skillCooldown.Clear();
        currentTurn = 0;
        yield return new WaitForSeconds(1f);
        BattleReward.instance.OpenBattelRewardScreen(xpGain,goldGain,crystalGain);
        //encounterField.SetActive(false);
        for(int i =0; i < encounterField.transform.childCount; i++)
        {
            encounterField.transform.GetChild(1).GetComponent<TilemapCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < encounterField.transform.childCount; i++)
        {
            encounterField.transform.GetChild(1).GetComponent<TilemapCollider2D>().enabled = true;
        }
        //encounterField.SetActive(true);
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
        groupOfStatue.SetActive(false);
        if (BattleReward.instance.gold.SupplyValue >= 10)
        {
            for (int i = 0; i < playerPrefabs.Length; i++)
            {
                playerPrefabs[i].CurrentHp = playerPrefabs[i].MaxHp;
                playerPrefabs[i].IsDied = false;
            }
            SkillManager2.instance.StartCon(successToRevive);
            Debug.Log("Revive Success");
            BattleReward.instance.gold.SupplyValue -= 10;
        }
        else
        {
            SkillManager2.instance.StartCon(falseToRevive);
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

