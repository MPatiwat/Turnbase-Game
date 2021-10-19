using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConSelect : MonoBehaviour
{
    [SerializeField] Conversation conversation;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject conButton;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "P")
        {
            conButton.SetActive(true);
            SkillManager2.instance.conversation = conversation;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "P")
        {
            conButton.SetActive(false);

        }
    }

}
