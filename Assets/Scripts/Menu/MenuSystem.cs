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
    [SerializeField] public CharacterBase[] characters;
    [SerializeField] public List<SkillData> skills;
    [SerializeField] BattleSystem _character;
    [SerializeField] BattleSystem _skill;
    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(false);
        _character = FindObjectOfType<BattleSystem>();
        _skill = FindObjectOfType<BattleSystem>();
        characters = _character.playerPrefabs;
        skills = _skill.saveSkill;
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
        //player.transform.position = new Vector2(0, 0);
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
        //CharacterSaveData characterSave = new CharacterSaveData();
        for(int i = 0; i < characters.Length; i++)
        {
            save.level[i] = characters[i].Level;
            save.pos[i] = characters[i].Pos;
            save.currentHp[i] = characters[i].CurrentHp;
            save.maxHp[i] = characters[i].MaxHp;
            save.atk[i] = characters[i].Attack;
            save.def[i] = characters[i].Defense;
            save.isActive[i] = characters[i].IsActivePlayer;
            save.isDied[i] = characters[i].IsDied;
            save.isActiveInStory[i] = characters[i].IsActiveInStory;
            /*for (int j = 0; j < characters[i].SelectedSkills.Count; i++)
            {
                save.selectedSkill[j] = characters[i].SelectedSkills[j];
                Debug.Log("Save " + characters[i].SelectedSkills[j] + " Success");
            }*/
            Debug.Log("Save " + characters[i].Name + " Success");
        }
        for(int i = 0; i < skills.Count; i++)
        {
            save.skillIsSelected[i] = skills[i].isSelected;
            save.skillIsUnlock[i] = skills[i].isUnlocked;
            if (skills[i].isSelected)
            {
                save.skillPos[i] = skills[i].SkillPos;
                Debug.Log(skills[i].getSkillName + " is selected have skill pos " + skills[i].SkillPos);
            }
            //save.skillPos[i] = skills[i].SkillPos;
            //Debug.Log("Save " + skills[i].getSkillName + " Success");
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
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].Level = save.level[i];
                characters[i].Pos = save.pos[i];
                characters[i].CurrentHp = save.currentHp[i];
                characters[i].MaxHp = save.maxHp[i];
                characters[i].Attack = save.atk[i];
                characters[i].Defense = save.def[i];
                characters[i].IsActivePlayer = save.isActive[i];
                characters[i].IsDied = save.isDied[i];
                characters[i].IsActiveInStory = save.isActiveInStory[i];
                Debug.Log("Load " + characters[i].Name + " Success");
            }
            for(int i = 0; i < skills.Count; i++)
            {
                skills[i].isSelected = save.skillIsSelected[i];
                skills[i].isUnlocked = save.skillIsUnlock[i];
                skills[i].SkillPos = save.skillPos[i];
                if (skills[i].isSelected && skills[i].isUnlocked)
                {
                    int id = skills[i].PlayerID;
                    int pos = skills[i].SkillPos;
                    characters[id].SelectedSkills[pos] = skills[i];
                }
                Debug.Log("Load " + skills[i].getSkillName + " Success");
            }
            /*for(int i = 0; i< characters.Length; i++)
            {
                characters[i].SelectedSkills
            }*/
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
