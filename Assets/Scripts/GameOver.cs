using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject menuCamera;
    [SerializeField] GameObject gameoverCamera;
    public void ReturnToMenu()
    {
        gameoverCamera.SetActive(false);
        menuCamera.SetActive(true);
    }
    public void LoadLastSave()
    {

    }
}
