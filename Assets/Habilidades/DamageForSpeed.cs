using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que intercambia velocidad de ataque por daño.
//
// Cuanto mayor es el tier de la habilidad, más daño inflige la unidad, pero ataca más lento.
public class DamageForSpeed : MonoBehaviour, Skills
{
    // Nivel de rareza de la habilidad
    private int tier;

    // Referencia a estadísticas del personaje
    private CharacterStats stats;

    // Tier aleatorio
    void Start()
    {
        stats = GetComponent<CharacterStats>();

        tier = stats.GenerateTier();

        float damageMultiplier = 1f;

        float attackSpeedMultiplier = 1f;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                damageMultiplier = 1.5f;

                attackSpeedMultiplier = 0.5f;

                break;

            case 2:

                damageMultiplier = 2f;

                attackSpeedMultiplier = 0.4f;

                break;

            case 3:

                damageMultiplier = 3f;

                attackSpeedMultiplier = 0.25f;

                break;

        }

        // =========================
        // Aplicar modificadores
        // =========================

        stats.SetDamageMultiplier(damageMultiplier);

        stats.SetAttackSpeedMultiplier(attackSpeedMultiplier);

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Daño por velocidad (" + "+" + ((damageMultiplier - 1f) * 100f) + "% Damage, " + "-" + ((1f - attackSpeedMultiplier) * 100f) + "% Speed) " + "-" + " " + "T" + tier;

        skill.description = "+" + ((damageMultiplier - 1f) * 100f) + "% daño, -" + ((1f - attackSpeedMultiplier) * 100f) + "% attack speed";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
