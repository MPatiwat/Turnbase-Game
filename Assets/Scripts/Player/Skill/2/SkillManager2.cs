using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillManager2 : MonoBehaviour
{
    public static SkillManager2 instance;

    

    public void StartCon()
    {
        joyUI.SetActive(false);
        settingUI.SetActive(false);
        questUI.SetActive(false);
        conButton.SetActive(false);
        DialogManager.StartConversation(conversation);
    }
    public void StartCon(Conversation con)
    {
        joyUI.SetActive(false);
        settingUI.SetActive(false);
        questUI.SetActive(false);
        //conButton.SetActive(false);
        DialogManager.StartConversation(con);
    }

    [SerializeField] public CharacterBase character;
    [SerializeField] public SkillData selectedSkill;
    //[SerializeField] MenuSystem _skills;
    [SerializeField] List<SkillData> skill;

    [Header("Skill Point")]
    [SerializeField] public Supply crystal;
    public TMP_Text pointText;

    [SerializeField] private SkillButton2[] skillButtons;

    [SerializeField] GameObject skillTreeMenu;
    [SerializeField] GameObject player;

    [Header("Selected Button")]
    [SerializeField] TMP_Text selectedButton;
    [SerializeField] int selectedSkillCount;

    [Header("Conversation")]
    [SerializeField] public Conversation conversation;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject questUI;
    [SerializeField] GameObject conButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        UpdatePointUI();
    }
    /*private void Update()
    {
        if (selectedSkill.isUnlocked == true && selectedSkill.isSelected == true)
        {
            skillButtons[selectedSkill.getSkillID].GetComponent<Image>().color = Color.white;
            skillButtons[selectedSkill.getSkillID].transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
    }*/
    private void UpdatePointUI()
    {
        pointText.text = crystal.SupplyValue.ToString();
    }

    public void UpgradeButton()
    {
        if(selectedSkill == null)
        {
            return;
        }
        //Skill ID 0-1,0-2,0-3,0-4
        if(crystal.SupplyValue >= selectedSkill.CrystalRequire && selectedSkill.preSkill.Length == 0 )
        {
            UpdateSkill();
        }
        //Skill ID 0-3,...
        if(crystal.SupplyValue >= selectedSkill.CrystalRequire && character.Level >= selectedSkill.LevelRequire)
        {
            for(int i = 0; i < selectedSkill.preSkill.Length; i++)
            {
                if(selectedSkill.preSkill[i].isUnlocked == true&&selectedSkill.preSkill[i])
                {
                    UpdateSkill();
                    break;
                }
            }
        }
    }

    private void UpdateSkill()
    {
        //Change Color when Learn Skill to White and Activate Skill Level
        skillButtons[selectedSkill.getSkillID].GetComponent<Image>().color = Color.white;
        //skillButtons[selectedSkill.getSkillID].transform.GetChild(1).gameObject.SetActive(true);
        //selectedSkill.getSkillLevel++;
        //skillButtons[selectedSkill.getSkillID].transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = selectedSkill.getSkillLevel.ToString();

        //Point--
        crystal.SupplyValue-=selectedSkill.CrystalRequire;
        UpdatePointUI();

        //Unlock Skill
        selectedSkill.isUnlocked = true;
    }

    public void CloseSkillTree()
    {
        skillTreeMenu.gameObject.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }
    public void SetSkill()
    {
        if (selectedSkill.isUnlocked == true && selectedSkill.isSelected != true)
        {
            if (character.SelectedSkills.Count < 3)
            {
                //change color selected skill
                skillButtons[selectedSkill.getSkillID].transform.GetChild(0).GetComponent<Image>().color = Color.red;

                //Update Select Text
                selectedSkill.isSelected = true;
                skillButtons[selectedSkill.getSkillID].UpdateSelectText();

                character.SelectedSkills.Add(selectedSkill);

                //Update Select Count
                //character.SelectedSkills.Count++;
            }
        }else if (selectedSkill.isUnlocked == true && selectedSkill.isSelected == true)
        {
            skillButtons[selectedSkill.getSkillID].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            selectedSkill.isSelected = false;
            skillButtons[selectedSkill.getSkillID].UpdateSelectText();
            character.SelectedSkills.Remove(selectedSkill);
            //selectedSkillCount--;
        }
    }
}
