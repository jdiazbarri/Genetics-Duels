using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForSpeed : MonoBehaviour, Skills
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

                // +50% daño
                damageMultiplier = 1.5f;

                // -50% velocidad
                attackSpeedMultiplier = 0.5f;

                break;

            case 2:

                // +100% daño
                damageMultiplier = 2f;

                // -60% velocidad
                attackSpeedMultiplier = 0.4f;

                break;

            case 3:

                // +200% daño
                damageMultiplier = 3f;

                // -75% velocidad
                attackSpeedMultiplier = 0.25f;

                break;

            default:

                damageMultiplier = 1.5f;

                attackSpeedMultiplier = 0.5f;

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
            "DamageForSpeed (" +

            "+" +
            ((damageMultiplier - 1f) * 100f) +
            "% Damage, " +

            "-" +
            ((1f - attackSpeedMultiplier) * 100f) +
            "% Speed) - T" +

            tier;

        skill.description =

            "+" +
            ((damageMultiplier - 1f) * 100f) +

            "% damage, -" +

            ((1f - attackSpeedMultiplier) * 100f) +

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
