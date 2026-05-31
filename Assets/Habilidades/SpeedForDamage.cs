using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que intercambia daño por velocidad de ataque.
//
// Cuanto mayor es el tier de la habilidad,
// menor daño inflige la unidad, pero más rápido ataca.
public class SpeedForDamage : MonoBehaviour, Skills
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

                damageMultiplier = 0.2f;

                attackSpeedMultiplier = 4f;

                break;

            case 2:

                damageMultiplier = 0.3f;

                attackSpeedMultiplier = 6f;

                break;

            case 3:

                damageMultiplier = 0.4f;

                attackSpeedMultiplier = 8f;

                break;
        }

        // =========================
        // Aplicar modificadores
        // =========================

        stats.SetDamageMultiplier(
            damageMultiplier
        );

        stats.SetAttackSpeedMultiplier(
            attackSpeedMultiplier
        );

        // ===================================
        // Información visual de la habilidad
        // ====================================
 
        SkillInfo skill = new SkillInfo();

        skill.skillName = "Vel.ataque por daño (" + "-" + ((1f - damageMultiplier) * 100f) + "% daño, +" + ((attackSpeedMultiplier - 1f) * 100f) + "% vel. Ataque) " + "-" + " " + "T" + tier;

        skill.description = "-" + ((1f - damageMultiplier) * 100f) +"% daño, +" + ((attackSpeedMultiplier - 1f) * 100f) +"% vel. Ataque";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
