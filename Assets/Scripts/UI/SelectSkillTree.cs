using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillTree : MonoBehaviour
{
    [SerializeField] CharacterBase character;
    [SerializeField] Image characterTree;
    [SerializeField] GameObject selectSkilltree;
    [SerializeField] int treeID;

    public void SelectedSkillTree()
    {
        for(int i = 0; i < 5; i++)
        {
            if(i != treeID)
            {
                selectSkilltree.transform.GetChild(i).gameObject.SetActive(false);
            }
            else if(i == treeID)
            {
                SkillManager2.instance.character = character;
                selectSkilltree.transform.GetChild(treeID).gameObject.SetActive(true);
                characterTree.sprite = character.Sprite;
            }
        }
        
        
    }
}
