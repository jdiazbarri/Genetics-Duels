using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceForLife : MonoBehaviour, Skills
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

        float healthMultiplier = 1f;

        float defenseMultiplier = 1f;

        switch (tier)
        {
            case 1:

                healthMultiplier = 0.6f;

                defenseMultiplier = 1.8f;

                break;

            case 2:

                healthMultiplier = 0.5f;

                defenseMultiplier = 2.2f;

                break;

            case 3:

                healthMultiplier = 0.4f;

                defenseMultiplier = 2.8f;

                break;
        }

        // APLICAR MULTIPLICADORES
        stats.SetHealthMultiplier(
            healthMultiplier
        );

        stats.SetDefenseMultiplier(
            defenseMultiplier
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "ArmorForLife (" +

            "-" +
            ((1f - healthMultiplier) * 100f) +

            "% Health) - T" +

            tier;

        skill.description =

            "-" +
            ((1f - healthMultiplier) * 100f) +

            "% health, +" +

            ((defenseMultiplier - 1f) * 100f) +

            "% defense";

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
