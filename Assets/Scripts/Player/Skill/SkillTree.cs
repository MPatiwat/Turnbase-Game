using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillTree : MonoBehaviour
{
    [SerializeField] public string skillName;
    [SerializeField] public Sprite skillSprite;

    [TextArea]
    [SerializeField] public string description;

    [SerializeField] bool isUpgrade;

    [SerializeField] ElementType element;
    [SerializeField] int damage;
    [SerializeField] int cooldown;

   
}
