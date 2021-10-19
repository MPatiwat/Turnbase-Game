using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTargetButton : MonoBehaviour
{
    //[SerializeField] int selectAttack;
    [SerializeField] int activeBattlerTarget;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        BattleSystem.instance.PlayerAttack(activeBattlerTarget);
    }
}
