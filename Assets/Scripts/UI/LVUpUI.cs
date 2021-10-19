using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVUpUI : MonoBehaviour
{
    [SerializeField] bool isActived = false;
    [SerializeField] bool isLVActived = false;

    [SerializeField] GameObject lvIcon;

    [SerializeField] GameObject skillTreeMenu;

    [SerializeField] GameObject player;

    public void Activated()
    {
        if (isActived == false)
        {
            isActived = true;
            lvIcon.gameObject.SetActive(true);
        }
        else if (isActived == true)
        {
            isActived = false;
            lvIcon.gameObject.SetActive(false);
        }
    }

    public void SkillActivated()
    {

        if (isLVActived == false)
        {
            isLVActived = true;

            //Open SkillTree 0
            skillTreeMenu.gameObject.SetActive(true);
            

            //Disable PlayerControl
            player.GetComponent<PlayerController>().enabled = false;
        }
        else if (isLVActived == true)
        {
            isLVActived = false;
        }
    }
}
