using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedForDamage : MonoBehaviour, Skills
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

        float damageMultiplier = 1f;

        float attackSpeedMultiplier = 1f;

        switch (tier)
        {
            case 1:

                // -80% daño
                damageMultiplier = 0.2f;

                // +300% velocidad
                attackSpeedMultiplier = 4f;

                break;

            case 2:

                // -70% daño
                damageMultiplier = 0.3f;

                // +500% velocidad
                attackSpeedMultiplier = 6f;

                break;

            case 3:

                // -60% daño
                damageMultiplier = 0.4f;

                // +700% velocidad
                attackSpeedMultiplier = 8f;

                break;

            default:

                damageMultiplier = 0.2f;

                attackSpeedMultiplier = 4f;

                break;
        }

        // APLICAR MULTIPLICADORES
        stats.SetDamageMultiplier(
            damageMultiplier
        );

        stats.SetAttackSpeedMultiplier(
            attackSpeedMultiplier
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "SpeedForDamage (" +

            "-" +
            ((1f - damageMultiplier) * 100f) +

            "% Damage, +" +

            ((attackSpeedMultiplier - 1f) * 100f) +

            "% Speed) - T" +

            tier;

        skill.description =

            "-" +
            ((1f - damageMultiplier) * 100f) +

            "% damage, +" +

            ((attackSpeedMultiplier - 1f) * 100f) +

            "% attack speed";

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
