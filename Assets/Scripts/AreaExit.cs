using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] Vector2 cameraChange;
    [SerializeField] Vector3 playeChange;
    private CameraMovement cam;
    //[SerializeField] bool shouldLoadAfterFade;

    private void Update()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("P"))
        {
            //shouldLoadAfterFade = true;
            //UIFade.instance.FadeToBlack();
            cam.minCamPosition += cameraChange;
            cam.maxCamPosition += cameraChange;
            other.transform.position += playeChange;
        }
    }
}

