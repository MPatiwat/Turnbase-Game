using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillHUD : MonoBehaviour
{
#pragma warning disable 0414
    //[SerializeField] CharacterBase character;
    [SerializeField] public SkillData skillslot;
    [SerializeField] public int skillSlotID;

    [SerializeField] Image skillImage;
    [SerializeField] BattleSystem _activeBattlers;
    [SerializeField] GameObject battleCamera;
    [SerializeField] List<CharacterBase> activeBattlers;
    [SerializeField] bool isAttack;
    [SerializeField] GameObject group;
    [SerializeField] BattleSystem _resetColor;
    [SerializeField] bool resetColor;
    [SerializeField] TMP_Text coolDownText;
    [SerializeField] GameObject coolDown;
    [SerializeField] GameObject skillButton;
    /*[SerializeField] BattleSystem _activeEnemy;
    [SerializeField] List<CharacterBase> activeEnemy;*/
    [SerializeField] SkillData blank;
    public void Start()
    {
        _activeBattlers = FindObjectOfType<BattleSystem>();
        activeBattlers = _activeBattlers.activeBattlers;
        /*_activeEnemy = FindObjectOfType<BattleSystem>();
        activeEnemy = _activeEnemy.activeEnemy;*/
    }

    public void Update()
    {
        _resetColor = FindObjectOfType<BattleSystem>();
        resetColor = _resetColor.resetColor;

        bool battleActive = _activeBattlers.battleActive;
        int currentTurn = _activeBattlers.currentTurn;
        if (battleActive)
        {
            if (activeBattlers[currentTurn].IsPlayer)
            {
                if(activeBattlers[currentTurn].SelectedSkills.Count > skillSlotID)
                {
                    skillslot = activeBattlers[currentTurn].SelectedSkills[skillSlotID];
                    skillButton.GetComponent<Button>().enabled = true;
                }
                else
                {
                    skillslot = blank;
                    skillButton.GetComponent<Button>().enabled = false;
                }
                skillImage.sprite = skillslot.getSkillSprite;
                if (skillslot.IsActivated == true)
                {
                    if (skillslot.CurrentCoolDown > 0)
                    {
                        coolDown.SetActive(true);
                        coolDownText.text = skillslot.CurrentCoolDown.ToString();
                        skillButton.GetComponent<Button>().enabled = false;
                        skillButton.GetComponent<Image>().color = Color.gray;
                    }
                    else if (skillslot.CurrentCoolDown <= 0)
                    {
                        coolDown.SetActive(false);
                        skillButton.GetComponent<Button>().enabled = true;
                        skillButton.GetComponent<Image>().color = Color.white;
                        skillslot.IsActivated = false;
                        skillslot.CurrentCoolDown = skillslot.CoolDown;
                    }
                }

            }
        }
        if (resetColor==true)
        {
            for(int i = 0; i < group.transform.childCount; i++)
            {
                group.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }
    }
    
    public void PressSkill()
    {
        for(int i = 0; i < group.transform.childCount; i++)
        {
            if (i == skillSlotID)
            {
                group.transform.GetChild(i).GetComponent<SkillHUD>().isAttack = true;
                group.transform.GetChild(i).GetComponent<Image>().color = Color.red;
            }
            else
            {
                group.transform.GetChild(i).GetComponent<SkillHUD>().isAttack = false;
                group.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }  
        }
    }
}
