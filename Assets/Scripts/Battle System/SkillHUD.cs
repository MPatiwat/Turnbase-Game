using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillHUD : MonoBehaviour
{
#pragma warning disable 0414
    //[SerializeField] CharacterBase character;
    [SerializeField] SkillData skillslot;
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
    [SerializeField] BattleSystem _playerTurn;
    [SerializeField] bool playerTurn;
    public void Start()
    {
        _activeBattlers = FindObjectOfType<BattleSystem>();
        activeBattlers = _activeBattlers.activeBattlers;
        

    }

    public void Update()
    {
        _resetColor = FindObjectOfType<BattleSystem>();
        resetColor = _resetColor.resetColor;
        _playerTurn = FindObjectOfType<BattleSystem>();
        playerTurn = _playerTurn.playerTurn;

        bool battleActive = _activeBattlers.battleActive;
        int currentTurn = _activeBattlers.currentTurn;
        if (battleActive)
        {
            if (activeBattlers[currentTurn].IsPlayer)
            {
                skillslot = activeBattlers[currentTurn].SelectedSkills[skillSlotID];
                skillImage.sprite = skillslot.getSkillSprite;
                if (skillslot.IsActivated==true)
                {
                    if (skillslot.CoolDown > 0)
                    {
                        coolDown.SetActive(true);
                        coolDownText.text = skillslot.CoolDown.ToString();
                        skillButton.GetComponent<Button>().enabled = false;
                        skillButton.GetComponent<Image>().color = Color.gray;
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
