using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inbreeding : MonoBehaviour, Skills
{
    private CharacterStats stats;

    private float multiplier = 1.5f;

    void Start()
    {
        stats =
            GetComponent<CharacterStats>();

        stats.SetHealthMultiplier(
            multiplier
        );

        stats.SetDamageMultiplier(
            multiplier
        );

        stats.SetDefenseMultiplier(
            multiplier
        );

        stats.SetAttackSpeedMultiplier(
            multiplier
        );

        SkillInfo skill =
            new SkillInfo();

        skill.skillName =
            "Endogamia";

        skill.description =
            "Todas las stats x1.5";

        stats.skills.Add(skill);
    }
}
