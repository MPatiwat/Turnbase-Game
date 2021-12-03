using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialog, navButtonText;
    public Image speakerSprite;

    private int currentIndex;
    public Conversation currentConversation;
    public static DialogManager instance;

    private Animator dialogControl;
    private Coroutine typing;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject questUI;
    [SerializeField] GameObject conButton;
    [SerializeField] TMP_Text questText;
    [SerializeField] public GameObject[] npc;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            dialogControl = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void StartConversation(Conversation con)
    {
        instance.dialogControl.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConversation = con;
        instance.speakerName.text = "";
        instance.dialog.text = "";
        instance.navButtonText.text = ">";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if(currentIndex > currentConversation.GetLength())
        {
            instance.dialogControl.SetBool("isOpen", false);
            joyUI.SetActive(true);
            settingUI.SetActive(true);
            questUI.SetActive(true);
            if (currentConversation.quest != null)
            {
                questText.text = currentConversation.GetQuestDescription();
                if (currentConversation.isDestroy == true)
                {
                    for(int i = 0; i < npc.Length; i++)
                    {
                        npc[i].SetActive(false);
                        npc[i].GetComponent<Close>().isClose = true;
                    }
                    if(currentConversation.quest.questCondition != null)
                    {
                        currentConversation.quest.questCondition.isQuestComplete = true;
                    }
                }
                
            }  
            //conButton.SetActive(true);
            return;
        }
        speakerName.text = currentConversation.GetDialogLine(currentIndex).characterSpeaker.Name;
        
        if(typing == null)
        {
            typing = instance.StartCoroutine(TypeText(currentConversation.GetDialogLine(currentIndex).dialog));
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConversation.GetDialogLine(currentIndex).dialog));
        }
        
        speakerSprite.sprite = currentConversation.GetDialogLine(currentIndex).characterSpeaker.Sprite;
        currentIndex++;

        if(currentIndex > currentConversation.GetLength())
        {
            navButtonText.text = "x";
            
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialog.text = "";

        bool complete = false;
        int index = 0;
        while (!complete)
        {
            dialog.text += text[index];
            index++;
            yield return new WaitForSeconds(0.02f);

            if(index == text.Length)
            {
                complete = true;
                //conOpen = false;
            }
        }

        typing = null;
    }
}
