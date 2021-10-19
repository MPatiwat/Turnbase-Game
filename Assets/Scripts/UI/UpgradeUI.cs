using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;
    [SerializeField] bool isActived = false;
    
    [Header("SkillTree")]
    [SerializeField] bool isSkillActived = false;
    //[SerializeField] GameObject skillIcon;
    [SerializeField] GameObject skillTreeMenu;
    [SerializeField] GameObject panelSetting;

    [Header("LvUp")]
    //[SerializeField] GameObject lvIcon;
    /*[SerializeField] GameObject lvUpMenu;
    [SerializeField] bool isLVActived = false;*/
    [SerializeField] public Supply gold;
    [SerializeField] public Supply exp;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text expText;

    [Header("Disable Controller while Upgrade Activating")]
    [SerializeField] GameObject player;

    private void Update()
    {
        goldText.text = gold.SupplyValue + " G";
        expText.text = exp.SupplyValue + " EXP";
    }
    public void Activated()
    {
        if(isActived == false)
        {
            isActived = true;
            panelSetting.gameObject.SetActive(true);
            //Disable PlayerControl
            player.GetComponent<PlayerController>().enabled = false;


        }
        else if (isActived == true)
        {
            isActived = false;
            panelSetting.gameObject.SetActive(false);
            //Enable PlayerControl
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

    public void SkillActivated()
    {
        
        if (isSkillActived == false)
        {
            isSkillActived = true;

            //Open SkillTree 0
            skillTreeMenu.gameObject.SetActive(true);
            skillTreeMenu.transform.GetChild(0).gameObject.SetActive(true);
            skillTreeMenu.transform.GetChild(1).gameObject.SetActive(false);
            skillTreeMenu.transform.GetChild(2).gameObject.SetActive(false);
            skillTreeMenu.transform.GetChild(3).gameObject.SetActive(false);
            skillTreeMenu.transform.GetChild(4).gameObject.SetActive(false);

            //Disable PlayerControl
            player.GetComponent<PlayerController>().enabled = false;
        }
        else if (isSkillActived == true)
        {
            isSkillActived = false;
        }
    }

    /*public void LVActivated()
    {

        if (isLVActived == false)
        {
            isLVActived = true;

            //Open SkillTree 0
            lvUpMenu.gameObject.SetActive(true);

            //Disable PlayerControl
            player.GetComponent<PlayerController>().enabled = false;
        }
        else if (isLVActived == true)
        {
            isLVActived = false;
        }
    }*/
}
