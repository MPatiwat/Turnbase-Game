using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public static BattleHUD instance;
    [SerializeField] CharacterBase character;
    [SerializeField] HpBar hpBar;
    [SerializeField] int posId;
    [SerializeField] bool isPlayer;
    [SerializeField] GameObject playerAnimator;

    [Header("Active Player")]
    //[SerializeField] TMP_Text activePlayerName;
    [SerializeField] Image activePlayerSprite;
    [SerializeField] Image activePlayerElement;
    [SerializeField] int currentHp, maxHp, attack, defense;

    [Header("Get From Battle System")]
    [SerializeField] BattleSystem _activePlayer;
    [SerializeField] BattleSystem _activeEnemy;
    [SerializeField] List<CharacterBase> activePlayer;
    [SerializeField] List<CharacterBase> activeEnemy;
    public bool shouldFade;
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] GameObject posObj;
    //[SerializeField] BattleSystem _resetSprite;
    [SerializeField] bool resetSprite;
    
    public void Start()
    {
        _activePlayer = FindObjectOfType<BattleSystem>();
        _activeEnemy = FindObjectOfType<BattleSystem>();
        activePlayer = _activePlayer.activePlayer;
        activeEnemy = _activeEnemy.activeEnemy; 

    }
    public void Update()
    {
            if (isPlayer)
            {
                for (int i = 0; i < activePlayer.Count; i++)
                {
                    if (activePlayer[i].Pos == posId)
                    {
                        character = activePlayer[i];
                        //activePlayerName.text = character.Name;
                        currentHp = character.CurrentHp;
                        maxHp = character.MaxHp;
                        attack = character.Attack;
                        defense = character.Defense;

                        playerAnimator.GetComponent<Animator>().runtimeAnimatorController = character.PlayerAnimator;
                    if (currentHp <= 0)
                        {
                        resetSprite = false;
                        playerAnimator.GetComponent<Animator>().enabled = false;
                            activePlayerSprite.sprite = character.DeadImage;
                            activePlayerSprite.color = new Color(
                                 Mathf.MoveTowards(activePlayerSprite.color.r, 1f, fadeSpeed * Time.deltaTime),
                                 Mathf.MoveTowards(activePlayerSprite.color.g, 0f, fadeSpeed * Time.deltaTime),
                                 Mathf.MoveTowards(activePlayerSprite.color.b, 0f, fadeSpeed * Time.deltaTime),
                                 Mathf.MoveTowards(activePlayerSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
                            if (activePlayerSprite.color.a == 0)
                            {
                                posObj.SetActive(false);
                            }
                        }
                        else if(currentHp>0)
                        {
                        resetSprite = true;
                            activePlayerSprite.sprite = character.BattleSprite;
                            activePlayerElement.sprite = character.ElementSprite;
                            playerAnimator.GetComponent<Animator>().enabled = true;
                        }
                        
                    }
                }
            }
            else
            {
                for (int i = 0; i < activeEnemy.Count; i++)
                {
                    if (activeEnemy[i].Pos == posId)
                    {
                        character = activeEnemy[i];
                        //activePlayerName.text = character.Name;
                        currentHp = character.CurrentHp;
                        maxHp = character.MaxHp;
                        attack = character.Attack;
                        defense = character.Defense;

                        playerAnimator.GetComponent<Animator>().runtimeAnimatorController = character.PlayerAnimator;
                    if (currentHp <= 0)
                        {
                        resetSprite = false;
                            playerAnimator.GetComponent<Animator>().enabled = false;
                            activePlayerSprite.sprite = character.DeadImage;
                            activePlayerSprite.color = new Color(
                                Mathf.MoveTowards(activePlayerSprite.color.r, 1f, fadeSpeed * Time.deltaTime),
                                Mathf.MoveTowards(activePlayerSprite.color.g, 0f, fadeSpeed * Time.deltaTime),
                                Mathf.MoveTowards(activePlayerSprite.color.b, 0f, fadeSpeed * Time.deltaTime),
                                Mathf.MoveTowards(activePlayerSprite.color.a, 0f, fadeSpeed * Time.deltaTime));
                            if (activePlayerSprite.color.a == 0)
                            {
                                posObj.SetActive(false);
                            }
                        }
                        else if(currentHp>0)
                        {
                        resetSprite = true;
                            activePlayerSprite.sprite = character.BattleSprite;
                            activePlayerElement.sprite = character.ElementSprite;
                            playerAnimator.GetComponent<Animator>().enabled = true;
                        }
                       
                    }
                    
                }
            }
            if (character)
            {
                hpBar.SetHp((float)character.CurrentHp / character.MaxHp);
            }
        if (resetSprite)
        {
            activePlayerSprite.color = Color.white;
        }
    }
    /*public void EnemyFade()
    {
        activePlayerSprite.color = new Color(1f, 1f, 1f, 1f);
    }*/
}
