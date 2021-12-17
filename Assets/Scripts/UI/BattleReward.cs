using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;

    [SerializeField] TMP_Text xpText, goldText, crystalText;
    [SerializeField] GameObject rewardScreen;

    [SerializeField] int xpGain, goldGain, crystalGain;
    [SerializeField] GameObject player;
    [SerializeField] GameObject joyUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject questUI;
    [SerializeField] public Supply xp, gold, crystal;
    [SerializeField] GameObject mainCamera;
    [SerializeField] AudioClip bgm;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            penBattelRewardScreen(1, 1, 1);
        }*/
    }
    public void OpenBattelRewardScreen(int xp, int gold, int crystal)
    {
        xpGain = xp;
        goldGain = gold;
        crystalGain = crystal;

        xpText.text = xpGain + " EXP";
        goldText.text = goldGain + " G";
        crystalText.text = crystalGain + "";

        this.xp.SupplyValue += xp;
        this.gold.SupplyValue += gold;
        this.crystal.SupplyValue += crystal;

        rewardScreen.SetActive(true);
    }
    public void CloseRewardScreen()
    {
        rewardScreen.SetActive(false);
        settingUI.SetActive(true);
        joyUI.SetActive(true);
        questUI.SetActive(true);
        //player.GetComponent<PlayerController>().moveSpeed = 200;
        player.SetActive(true);
        mainCamera.GetComponent<AudioSource>().clip = FindObjectOfType<BattleSystem>().beforeBattleBGM;
        mainCamera.GetComponent<AudioSource>().Play();
    }
}