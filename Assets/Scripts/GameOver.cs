using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject menuCamera;
    [SerializeField] GameObject gameoverCamera;
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadLastSave()
    {

    }
}
