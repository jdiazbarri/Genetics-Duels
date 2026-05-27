using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critical : MonoBehaviour, Skills
{
    private int tier;

    private CharacterStats stats;

    void Start()
    {
        stats =
            GetComponent<CharacterStats>();

        // TIER ALEATORIO
        tier =
            stats.GenerateTier();

        float critChance = 0f;

        switch (tier)
        {
            case 1:

                critChance = 0.30f;

                break;

            case 2:

                critChance = 0.50f;

                break;

            case 3:

                critChance = 0.75f;

                break;
        }

        // APLICAR BONUS
        stats.AddCritChance(
            critChance
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =
            "Critical (" +
            (critChance * 100f) +
            "%) - T" +
            tier;

        skill.description =
            "+" +
            (critChance * 100f) +
            "% critical chance";

        // EVITAR DUPLICADOS
        if (
            !stats.HasSkill(
                skill.skillName
            )
        )
        {
            stats.skills.Add(skill);
        }
    }
}
