using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supply", menuName = "Supply/Create Supply")]
public class Supply : ScriptableObject
{
    [SerializeField] int supplyValue;

    public int SupplyValue
    {
        get { return supplyValue; }
        set { supplyValue = value; }
    }
}
