using System.Collections;
using System.Collections.Generic;
using System.IO; // using File class
using System.Runtime.Serialization.Formatters.Binary; // using BinaryFormattter class
using UnityEngine;
using TMPro;

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
    [SerializeField] TMP_Text questText;

    [Header("Save Data")]
    [SerializeField] public Supply exp;
    [SerializeField] public Supply gold;
    [SerializeField] public Supply crystal;
    [SerializeField] public CharacterBase[] characters;
    [SerializeField] public List<SkillData> skills;
    [SerializeField] public List<Quest> quest;
    [SerializeField] public List<GameObject> npc;
    [SerializeField] public List<GameObject> signal;
    [SerializeField] public List<AudioClip> saveBGM;
    [SerializeField] public int questID;
    [SerializeField] BattleSystem _character;
    [SerializeField] BattleSystem _skill;
    [SerializeField] BattleSystem _questList;
    [SerializeField] BattleSystem _npc;
    [SerializeField] BattleSystem _signal;
    [SerializeField] BattleSystem _saveBGM;
    [SerializeField] int loadQuestID = 0;
    [SerializeField] AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        gameUI.SetActive(false);
        _character = FindObjectOfType<BattleSystem>();
        _skill = FindObjectOfType<BattleSystem>();
        _questList = FindObjectOfType<BattleSystem>();
        _npc = FindObjectOfType<BattleSystem>();
        _signal = FindObjectOfType<BattleSystem>();
        _saveBGM = FindObjectOfType<BattleSystem>();
        characters = _character.playerPrefabs;
        skills = _skill.saveSkill;
        quest = _questList.saveQuest;
        npc = _npc.npc;
        signal = _signal.signal;
        saveBGM = _saveBGM.bgm;
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
        mainCamera.GetComponent<AudioSource>().clip = bgm;
        mainCamera.GetComponent<AudioSource>().Play();
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
        if(DialogManager.instance.currentConversation == null)
        {
            save.currentQuestID = loadQuestID;
        }else if(DialogManager.instance.currentConversation.quest == null)
        {
            save.currentQuestID = loadQuestID;
        }
        else
        {
            save.currentQuestID = DialogManager.instance.currentConversation.quest.getQuestID;
        }
        Debug.Log(save.currentQuestID);
        save.minCamX = mainCamera.GetComponent<CameraMovement>().minCamPosition.x;
        save.minCamY = mainCamera.GetComponent<CameraMovement>().minCamPosition.y;
        save.maxCamX = mainCamera.GetComponent<CameraMovement>().maxCamPosition.x;
        save.maxCamY = mainCamera.GetComponent<CameraMovement>().maxCamPosition.y;
        //CharacterSaveData characterSave = new CharacterSaveData();
        for (int i = 0; i < characters.Length; i++)
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
            if (skills[i].isSelected)
            {
                save.skillSelected[i] = skills[i].getSkillName;
                save.skillPos[i] = skills[i].SkillPos;
                Debug.Log(skills[i].getSkillName + "is selected");
                Debug.Log(skills[i].getSkillName + "is Pos" + skills[i].SkillPos);
            }
            if (skills[i].isUnlocked)
            {
                save.skillUnlock[i] = skills[i].getSkillName;
                Debug.Log(skills[i].getSkillName + "Unlocked");
            }
            //save.skillPos[i] = skills[i].SkillPos;
            //Debug.Log("Save " + skills[i].getSkillName + " Success");
        }
        /*for(int i = 0; i < characters.Length; i++)
        {
            for(int j = 1; j < characters[i].SelectedSkills.Count; j++)
            {
                save..Add(characters[i].SelectedSkills[j].getSkillID, characters[i].SelectedSkills[j].SkillPos);
                Debug.Log(characters[i].SelectedSkills[j].getSkillName + " skill id : " + characters[i].SelectedSkills[j].getSkillID + " skill pos : " + characters[i].SelectedSkills[j].SkillPos);
            }
        }*/
        /*for(int i = 0; i < npc.Count; i++)
        {
            save.npcClose[i] = npc[i].GetComponent<Close>().isClose;
        }*/
        for (int i = 0; i < npc.Count; i++)
        {
            if (npc[i].GetComponent<Close>().isClose)
            {
                save.npc[i] = npc[i].name;
                Debug.Log("Save close : " + save.npc[i].ToString());
            }
        }
        for (int i = 0; i < signal.Count; i++)
        {
            if (signal[i].GetComponent<TimelineTrigger>().trigger)
            {
                save.signal[i] = signal[i].name;
                Debug.Log("Save close : " + save.signal[i].ToString());
            }
        }
        save.bgm = mainCamera.GetComponent<AudioSource>().clip.name;
        Debug.Log(save.bgm + "Saved");
        
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
            mainCamera.GetComponent<CameraMovement>().minCamPosition.x = save.minCamX;
            mainCamera.GetComponent<CameraMovement>().minCamPosition.y = save.minCamY;
            mainCamera.GetComponent<CameraMovement>().maxCamPosition.x = save.maxCamX;
            mainCamera.GetComponent<CameraMovement>().maxCamPosition.y = save.maxCamY;
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
                /*if (characters[i].IsActivePlayer)
                {
                    FormationUI.instance.activePlayer[characters[i].Pos] = characters[i];
                }*/
                Debug.Log("Load " + characters[i].Name + " Success");
            }
            /*for(int i = 0; i < save.skillSelected.Length; i++)
            {
                for(int j = 0; j < skills.Count; j++)
                {
                    if(save.skillSelected[i] == skills[j].getSkillName)
                    {
                        characters[skills[j].PlayerID].SelectedSkills[save.skillPos[i]] = skills[j];
                        skills[j].isSelected = true;
                        skills[j].SkillPos = save.skillPos[i];
                        Debug.Log(skills[j].getSkillName + "is selected" + "have skill pos" + save.skillPos[i]);
                    }
                    else
                    {
                        skills[j].isSelected = false;
                        skills[j].SkillPos = 0;
                    }
                }
            }*/
            for(int i = 0; i < skills.Count; i++)
            {
                for(int j = 0; j < save.skillSelected.Length; j++)
                {
                    if(skills[i].getSkillName == save.skillSelected[j])
                    {
                        characters[skills[i].PlayerID].SelectedSkills[save.skillPos[j]] = skills[i];
                        skills[i].isSelected = true;
                        skills[i].SkillPos = save.skillPos[j];
                        Debug.Log(skills[i].getSkillName + "is selected" + "have skill pos" + save.skillPos[j]);
                        break;
                    }
                    else
                    {
                        skills[i].isSelected = false;
                        skills[i].SkillPos = 0;
                    }
                }
            }
            for(int i = 0;i< skills.Count; i++)
            {
                for(int j = 0; j < save.skillUnlock.Length; j++)
                {
                    if(skills[i].getSkillName == save.skillUnlock[j])
                    {
                        skills[i].isUnlocked = true;
                        break;
                    }
                    else
                    {
                        skills[i].isUnlocked = false;
                    }
                }
            }
            /*for(int i = 0; i < save.skillUnlock.Length; i++)
            {
                for(int j = 0; j < skills.Count; j++)
                {
                    if(save.skillUnlock[i] == skills[j].getSkillName)
                    {
                        skills[j].isUnlocked = true;
                    }
                    else
                    {
                        skills[j].isUnlocked = false;
                    }
                }
            }*/
            /*for(int i = 0; i < skills.Count; i++)
            {
                if (skills[i].isSelected && skills[i].isUnlocked)
                {
                    int id = skills[i].PlayerID;
                    int pos = skills[i].SkillPos;
                    characters[id].SelectedSkills[pos] = skills[i];
                }
                Debug.Log("Load " + skills[i].getSkillName + " Success");
            }*/
            for (int i = 0; i< quest.Count; i++)
            {
                if(save.currentQuestID == quest[i].getQuestID)
                {
                    loadQuestID = save.currentQuestID;
                    questText.text = quest[i].GetDescription;
                }
            }
            /*for (int i = 0; i < npc.Count; i++)
            {
                if (save.npcClose[i])
                {
                    npc[i].SetActive(false);
                }
            }*/
            for (int i = 0; i < npc.Count; i++)
            {
                for(int j = 0; j < save.npc.Length; j++)
                {
                    if(npc[i].name == save.npc[j])
                    {
                        npc[i].SetActive(false);
                        npc[i].GetComponent<Close>().isClose = true;
                        Debug.Log(npc[i] + "Close Success");
                        break;
                    }
                    else
                    {
                        npc[i].SetActive(true);
                        npc[i].GetComponent<Close>().isClose = false;
                    }
                }
            }
            for (int i = 0; i < signal.Count; i++)
            {
                if (save.signal[i] == signal[i].name)
                {
                    signal[i].GetComponent<BoxCollider2D>().enabled = false;
                    signal[i].GetComponent<TimelineTrigger>().trigger = true;
                }
                else
                {
                    signal[i].GetComponent<BoxCollider2D>().enabled = true;
                    signal[i].GetComponent<TimelineTrigger>().trigger = false;
                }
            }
            for(int i = 0; i < saveBGM.Count; i++)
            {
                if (save.bgm == saveBGM[i].name)
                {
                    mainCamera.GetComponent<AudioSource>().clip = saveBGM[i];
                    mainCamera.GetComponent<AudioSource>().Play();
                }
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
