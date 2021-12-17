using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBGM : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioClip bgm;
    [SerializeField] Image battleBackground;
    [SerializeField] Sprite bg;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "P")
        {
            mainCamera.GetComponent<AudioSource>().clip = bgm;
            mainCamera.GetComponent<AudioSource>().Play(0);
            battleBackground.sprite = bg;
        }
    }
}
