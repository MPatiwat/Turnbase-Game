using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHp(float currentHp)
    {
        health.transform.localScale = new Vector3(currentHp, 1f);
    }
}
