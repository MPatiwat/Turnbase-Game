using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Quest currentQuest;
    [SerializeField] CharacterBase[] player;
    [SerializeField] GameObject[] characterUI;
    [SerializeField] GameObject[] characterSkillTree;
    [SerializeField] TMP_Text questText;

    private void Start()
    {
        questText.text = currentQuest.GetDescription;
    }
    private void Update()
    {
        for(int i = 0; i < player.Length; i++)
        {
            if (player[i].IsActiveInStory == true)
            {
                characterUI[i].SetActive(true);
                characterSkillTree[i].SetActive(true);
            }
        }
    }
}
