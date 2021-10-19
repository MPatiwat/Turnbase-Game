using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    public SkillData Base { get; set; }

    public Skill(SkillData pBase)
    {
        Base = pBase;
    }
}
