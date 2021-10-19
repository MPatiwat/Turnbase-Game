using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree2 : MonoBehaviour
{
    [SerializeField] public string skillName;
    [SerializeField] public Sprite skillSprite;

    [TextArea]
    [SerializeField] public string description;

    [SerializeField] bool isUpgrade;

    public enum SkillElement
    {
        Fire,
        Water,
        Wood,
        Thunder
    }

    public SkillElement skillElement;
}
