using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    public GameObject control;
    [SerializeField] GameObject player;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }
    private void Director_Stopped(PlayableDirector director)
    {
        control.SetActive(true);
        player.GetComponent<PlayerController>().canMove = true;
    }
    private void Director_Played(PlayableDirector director)
    {
        //StartCoroutine(ControlFadeBlack());
        control.SetActive(false);
    }
    public void StartTimeline()
    {
        director.Play();
    }
    public IEnumerator ControlFadeBlack()
    {
        yield return new WaitForSeconds(.5f);

        UIFade.instance.FadeToBlack();

        yield return new WaitForSeconds(1.5f);
        UIFade.instance.FadeFromBlack();
        //control.SetActive(false);
    }
}
