using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField] BattleType[] potemtialBattles;
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("P"))
        {
            if (Random.Range(1, 101) <= 100)
            {
                StartCoroutine(StartEncounter());
            }
        }
    }
    public IEnumerator StartEncounter()
    {
        //player.GetComponent<PlayerController>().moveSpeed = 0;
        player.SetActive(false);
        UIFade.instance.FadeToBlack();
        //BattleSystem.instance.battleActive = true;

        int selectedBattle = Random.Range(0, potemtialBattles.Length);
        BattleSystem.instance.xpGain = potemtialBattles[selectedBattle].rewardEXP;
        BattleSystem.instance.goldGain = potemtialBattles[selectedBattle].rewardGold;
        BattleSystem.instance.crystalGain = potemtialBattles[selectedBattle].rewardCrystal;

        yield return new WaitForSeconds(1.5f);

        BattleSystem.instance.BattleStart(potemtialBattles[selectedBattle].enemies);
        UIFade.instance.FadeFromBlack();
    }
}
