using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPlayAnimation : MonoBehaviour
{
    public void UpdateAnimator()
    {
        this.gameObject.GetComponent<Animator>().Play("hit");
    }
}
