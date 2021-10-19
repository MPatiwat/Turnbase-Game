using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Animator cameraControl;
    //[SerializeField] GameObject joy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "P")
        {
            cameraControl.SetBool("cutscene1", true);
            //joy.GetComponent<FixedJoystick>().enabled = false;
            Invoke(nameof(StopCutscene), 3f);
        }
    }
    private void StopCutscene()
    {
        cameraControl.SetBool("cutscene1", false);
    }
}
