using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPorVelocidad : MonoBehaviour, Habilidad
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

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

        stats.SetDamageMultiplier(
            damageMultiplier
        );

        stats.SetAttackSpeedMultiplier(
            attackSpeedMultiplier
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "DañoPorVelocidad (" +
            "+" + ((damageMultiplier - 1f) * 100f) + "% Daño, " +
            "-" + ((1f - attackSpeedMultiplier) * 100f) + "% Velocidad)" + "-" + "T" + tier;

        skill.description =
            "+" +
            ((damageMultiplier - 1f) * 100f) +
            "% daño, -" +
            ((1f - attackSpeedMultiplier) * 100f) +
            "% velocidad ataque";

        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
