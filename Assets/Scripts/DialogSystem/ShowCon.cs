using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCon : MonoBehaviour
{
    public static ShowCon instance;

    [SerializeField] public Conversation conversation;

    public void StartCon()
    {
        DialogManager.StartConversation(conversation);
    }


}

