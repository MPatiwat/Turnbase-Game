using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton2 : MonoBehaviour
{
    public SkillData skillData;

    public Image skillImage;
    public TMP_Text skillNameText;
    public TMP_Text skillDesText;

    public TMP_Text selectText;
    [SerializeField] GameObject skillSelect;
    public void PressSkillButton()
    {
        SkillManager2.instance.selectedSkill = skillData;

        skillImage.sprite = skillData.getSkillSprite;
        skillDesText.text = skillData.getSkillDescription;
        skillNameText.text = skillData.getSkillName;   
    }
    private void Update()
    {
        if (skillData.isUnlocked == true)
        {
            skillSelect.GetComponent<Image>().color = Color.white;
            //skillSelect.transform.GetChild(1).gameObject.SetActive(true);
            //skillSelect.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = skillData.getSkillLevel.ToString();
        }
        else
        {
            skillSelect.GetComponent<Image>().color = Color.gray;
        }
        if(skillData.isSelected == true)
        {
            skillSelect.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            skillSelect.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }

    public void UpdateSelectText()
    {
       if(skillData.isSelected != true)
        {
            selectText.text = "Select";
        }
        else if(skillData.isSelected == true)
        {
            selectText.text = "Deselect";
        }
    }
}
