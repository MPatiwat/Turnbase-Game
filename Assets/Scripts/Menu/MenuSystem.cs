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
    [SerializeField] GameObject gameoverUI;

    [Header("Save Data")]
    [SerializeField] public Supply exp;
    [SerializeField] public Supply gold;
    [SerializeField] public Supply crystal;
    [SerializeField] public List<CharacterBase> characters;
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
        //gameoverUI.SetActive(false);
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
        for(int i = 0; i < characters.Count; i++)
        {
            /*var newCharacter = ScriptableObject.CreateInstance<CharacterBase>();
            newCharacter.Name = characters[i].Name;
            newCharacter.Level = characters[i].Level;
            newCharacter.PlayerID = characters[i].PlayerID;
            newCharacter.Pos = i;
            newCharacter.Description = characters[i].Description;
            newCharacter.Sprite = characters[i].Sprite;
            newCharacter.BattleSprite = characters[i].BattleSprite;
            newCharacter.Element = characters[i].Element;
            newCharacter.Role = characters[i].Role;
            newCharacter.CurrentHp = characters[i].CurrentHp;
            newCharacter.MaxHp = characters[i].MaxHp;
            newCharacter.Attack = characters[i].Attack;
            newCharacter.Defense = characters[i].Defense;
            newCharacter.SelectedSkills = characters[i].SelectedSkills;
            newCharacter.IsActivePlayer = characters[i].IsActivePlayer;
            newCharacter.IsPlayer = characters[i].IsPlayer;
            newCharacter.PlayerAnimator = characters[i].PlayerAnimator;
            newCharacter.IsDied = characters[i].IsDied;
            newCharacter.DeadImage = characters[i].DeadImage;
            save.character.Add(newCharacter);
            Debug.Log("Create" + newCharacter.Name + "Succes");*/
            save.character.Add(characters[i]);
        }

        
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
            for(int i = 0; i < save.character.Count; i++)
            {
                characters[i].Name = save.character[i].Name;
                characters[i].Level = save.character[i].Level;
                characters[i].PlayerID = save.character[i].PlayerID;
                characters[i].Pos = i;
                characters[i].Description = save.character[i].Description;
                characters[i].Sprite = save.character[i].Sprite;
                characters[i].BattleSprite = save.character[i].BattleSprite;
                characters[i].Element = save.character[i].Element;
                characters[i].Role = save.character[i].Role;
                characters[i].CurrentHp = save.character[i].CurrentHp;
                characters[i].MaxHp = save.character[i].MaxHp;
                characters[i].Attack = save.character[i].Attack;
                characters[i].Defense = save.character[i].Defense;
                characters[i].SelectedSkills = save.character[i].SelectedSkills;
                characters[i].IsActivePlayer = save.character[i].IsActivePlayer;
                characters[i].IsPlayer = save.character[i].IsPlayer;
                characters[i].PlayerAnimator = save.character[i].PlayerAnimator;
                characters[i].IsDied = save.character[i].IsDied;
                characters[i].DeadImage = save.character[i].DeadImage;
            }
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
