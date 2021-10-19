using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackColor : MonoBehaviour
{
    [SerializeField] GameObject group;

    public void backOri()
    {
        for (int i = 0; i < group.transform.childCount; i++)
        {
            group.transform.GetChild(i).GetComponent<ChangeColor>().enabled = false;
        }
    }
}
