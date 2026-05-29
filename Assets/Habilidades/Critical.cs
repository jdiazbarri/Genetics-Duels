using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que aumenta la probabilidad de realizar golpes críticos.
//
// Los golpes críticos duplican el daño infligido al objetivo.
// La probabilidad de activación depende del tier generado aleatoriamente.
public class Critical : MonoBehaviour, Skills
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

        float critChance = 0f;

        // =========================
        // Escalado por tier
        // =========================

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

        // =========================
        // Aplicar modificadores
        // =========================

        stats.AddCritChance(
            critChance
        );

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Crítico (" + (critChance * 100f) + "%" + " - " + " " + "T" + tier;

        skill.description = "+" + (critChance * 100f) + "% critical chance";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
