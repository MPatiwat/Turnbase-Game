using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] GameObject cutCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController moveScript = collision.GetComponent<PlayerController>();
        moveScript.canMove = false;
        cutCollider.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(ControlFadeBlack());
        
    }
    /*public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        director.Play();
    }*/
    public IEnumerator ControlFadeBlack()
    {
        //yield return new WaitForSeconds(1.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1f);
        UIFade.instance.FadeFromBlack();
        director.Play();
    }
}
