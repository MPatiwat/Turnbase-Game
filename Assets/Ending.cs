using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] Quest endingQuest;
    [SerializeField] GameObject endingActivate;
    [SerializeField] GameObject endingScene;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject questText;
    [SerializeField] GameObject settingUI;

    // Update is called once per frame
    void Update()
    {
        if (endingQuest.isQuestComplete)
        {
            endingActivate.GetComponent<BoxCollider2D>().enabled = true;
        }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(End());

    }
    public IEnumerator End()
    {
        joyUI.SetActive(false);
        questText.SetActive(false);
        settingUI.SetActive(false);
        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        UIFade.instance.FadeFromBlack();
        endingScene.SetActive(true);
    }
}
