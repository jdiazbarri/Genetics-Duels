using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadPorDano : MonoBehaviour
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

        stats.SetDamageMultiplier(
            damageMultiplier
        );

        stats.SetAttackSpeedMultiplier(
            attackSpeedMultiplier
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "VelocidadPorDaño (" +
            "-" + ((1f - damageMultiplier) * 100f) + "% Daño, +" +
            ((attackSpeedMultiplier - 1f) * 100f) + "% Velocidad)" + "-" + "T" + tier;

        skill.description =
            "-" +
            ((1f - damageMultiplier) * 100f) +
            "% daño, +" +
            ((attackSpeedMultiplier - 1f) * 100f) +
            "% velocidad ataque";

        stats.skills.Add(skill);
    }
}
