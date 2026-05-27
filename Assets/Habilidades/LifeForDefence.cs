using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeForDefence : MonoBehaviour, Skills
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

        float defenseMultiplier = 1f;

        float healthMultiplier = 1f;

        switch (tier)
        {
            case 1:

                // -50% defensa
                defenseMultiplier = 0.5f;

                // +50% vida
                healthMultiplier = 1.5f;

                break;

            case 2:

                defenseMultiplier = 0.4f;

                healthMultiplier = 2f;

                break;

            case 3:

                defenseMultiplier = 0.3f;

                healthMultiplier = 2.5f;

                break;
        }

        // APLICAR MULTIPLICADORES
        stats.SetDefenseMultiplier(
            defenseMultiplier
        );

        stats.SetHealthMultiplier(
            healthMultiplier
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "LifeForDefense (" +

            "-" +
            ((1f - defenseMultiplier) * 100f) +

            "% Defense) - T" +

            tier;

        skill.description =

            "-" +
            ((1f - defenseMultiplier) * 100f) +

            "% defense, +" +

            ((healthMultiplier - 1f) * 100f) +

            "% health";

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
