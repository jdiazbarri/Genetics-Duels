using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endogamia : MonoBehaviour
{
    private CharacterStats stats;

    private float multiplicador = 1.5f;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        stats.SetVidaMultiplier(multiplicador);

        stats.SetDamageMultiplier(multiplicador);

        stats.SetDefenseMultiplier(multiplicador);

        stats.SetAttackSpeedMultiplier(multiplicador);

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "Endogamia";

        skill.description =
            "Todas las stats x1.5";

        stats.skills.Add(skill);
    }
}
