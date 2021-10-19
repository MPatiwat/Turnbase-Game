using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShowCharacterInfo : MonoBehaviour
{
    [SerializeField] CharacterBase character;
    [SerializeField] Image characterSprite;
    [SerializeField] TMP_Text characterName;
    [SerializeField] TMP_Text currentHp;
    [SerializeField] TMP_Text maxHp;
    [SerializeField] TMP_Text level;
    [SerializeField] TMP_Text atk;
    [SerializeField] TMP_Text def;
    [SerializeField] TMP_Text role;
    //[SerializeField] TMP_Text activeText;

    public void Update()
    {
        characterSprite.sprite = character.Sprite;
        characterName.text = character.Name;
        currentHp.text = character.CurrentHp.ToString();
        maxHp.text = character.MaxHp.ToString();
        level.text = character.Level.ToString();
        atk.text = character.Attack.ToString();
        def.text = character.Defense.ToString();
        role.text = character.Role.ToString();
        
    }
    /*public void UpdateText()
    {
        if(character.IsActivePlayer != true)
        {
            activeText.text = "Active";
        }
        else if(character.IsActivePlayer == true)
        {
            activeText.text = "Deactive";
        }
    }*/
}
