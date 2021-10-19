using System.Collections;
using System.Collections.Generic;
using System.IO; // using File class
using System.Runtime.Serialization.Formatters.Binary; // using BinaryFormattter class
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject menuCamera;
    [SerializeField] GameObject gameUI;

    [SerializeField] GameObject player;
    [SerializeField] bool isSaveLoadUIActived;
    [SerializeField] GameObject saveloadUI;
    [SerializeField] GameObject selectClose;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject settingUI;

    [Header("Save Data")]
    [SerializeField] public Supply exp;
    [SerializeField] public Supply gold;
    [SerializeField] public Supply crystal;
    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(false);
        //mainCamera.SetActive(false);
        //state = SaveManager.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        menuCamera.SetActive(false);
        //mainCamera.SetActive(true);
        gameUI.SetActive(true);
        joyUI.SetActive(true);
        settingUI.SetActive(true);
        player.SetActive(true);
        player.transform.position = new Vector2(0, 0);
    }
    public void LoadGameButton()
    {
        LoadGame();
        menuCamera.SetActive(false);
        gameUI.SetActive(true);
        joyUI.SetActive(true);
        settingUI.SetActive(true);
        player.SetActive(true);
    }
    public void SaveButton()
    {
        SaveGame();
    }
    public void LoadButton()
    {
        LoadGame();
    }
    private SaveState createSave()
    {
        SaveState save = new SaveState();
        save.exp = exp.SupplyValue;
        save.gold = gold.SupplyValue;
        save.crystal = crystal.SupplyValue;
        save.playerPosX = player.transform.position.x;
        save.playerPosY = player.transform.position.y;

        return save;
    }
    private void SaveGame()
    {
        SaveState save = createSave();
        BinaryFormatter bf = new BinaryFormatter();

        FileStream fileStream = File.Create(Application.persistentDataPath + "/Save.txt");
        bf.Serialize(fileStream, save);

        Debug.Log("Save Success");
        
        fileStream.Close();
    }
    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fileStream = File.Open(Application.persistentDataPath + "/Save.txt", FileMode.Open);

            SaveState save = bf.Deserialize(fileStream) as SaveState;
            fileStream.Close();

            player.transform.position = new Vector2(save.playerPosX, save.playerPosY);
            exp.SupplyValue = save.exp; 
            gold.SupplyValue = save.gold;
            crystal.SupplyValue = save.crystal;
            Debug.Log("Load Success");
        }
        else
        {
            Debug.Log("Failed to Load");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CloseSaveAndLoadUI()
    {
        if(isSaveLoadUIActived == true)
        {
            isSaveLoadUIActived = false;
            saveloadUI.SetActive(false);
            selectClose.SetActive(true);
        }
    }
    public void OpenSaveAndLoadUI()
    {
        if (isSaveLoadUIActived == false)
        {
            isSaveLoadUIActived = true;
            saveloadUI.SetActive(true);
            selectClose.SetActive(false);
        }
    }
}
