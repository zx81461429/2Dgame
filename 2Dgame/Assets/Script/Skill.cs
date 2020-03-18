using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    CharacterSkillSystem skillSystem;

    private void Start()
    {
        skillSystem = FindObjectOfType<CharacterSkillSystem>();
    }

    public void OnSkillButtonClick()
    {
        skillSystem.AttackUseSkill(1001);
    }
}
