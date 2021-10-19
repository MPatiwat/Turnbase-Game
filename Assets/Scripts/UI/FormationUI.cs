using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FormationUI : MonoBehaviour
{
    [SerializeField] CharacterBase[] characters;
    [Header("Formation")]
    [SerializeField] GameObject selectClose;
    [SerializeField] CharacterBase character;
    [SerializeField] bool isformationActived;
    [SerializeField] bool isSelectActive;
    [SerializeField] CharacterBase[] activePlayer ;
    [SerializeField] int activeCount;
    
    [Header("Position")]
    [SerializeField] TMP_Text pos;
    [SerializeField] int posId;
    [SerializeField] GameObject posGroup;
    [SerializeField] GameObject chaUI;
    [SerializeField] TMP_Text activeText;


    public void Formation(CharacterBase selectCharacter)
    {
        character = selectCharacter;
  
    }
    public void OpenFormation(GameObject formation)
    {
        if(isformationActived == false)
        {
            isformationActived = true;
            formation.SetActive(true);
            selectClose.SetActive(false);
        }
        
    }
    public void CloseFormation(GameObject formation)
    {
        if (isformationActived == true)
        {
            isformationActived = false;
            isSelectActive = false;
            formation.SetActive(false);
            selectClose.SetActive(true);
            for (int i = 0; i < 5; i++)
            {
                chaUI.transform.GetChild(i).GetComponent<Button>().enabled = false;
                chaUI.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                posGroup.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
            
        }
            
        
    }
    public void ActiveFormation()
    {
        if(!activePlayer.Contains(character))
        {
            pos.text = character.Name;
            activePlayer[posId] = character;
        }
        
    }
    public void SelectPos(TMP_Text pos)
    {
        if(isformationActived == true)
        {
            if (isSelectActive == false)
            {
                isSelectActive = true;
                for (int i = 0; i < 5; i++)
                {
                    chaUI.transform.GetChild(i).GetComponent<Button>().enabled = true;
                }
            }
            this.pos = pos;
        }
    }
    public void SetPos(int id)
    {
        posId = id;
    }
    public void SaveFormation()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].IsActivePlayer = false;
            characters[i].Pos = -1;
        }
        for (int j = 0; j < activePlayer.Length; j++)
        {
            if (activePlayer[j].IsActiveInStory == true)
            {
                activePlayer[j].IsActivePlayer = true;
                activePlayer[j].Pos = j;
            }  
        }
    }
}
