using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que intercambia vida máxima por defensa.
//
// El personaje sacrifica una parte de su vida para obtener una gran cantidad de defensa.
public class DefenceForLife : MonoBehaviour, Skills
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

        float healthMultiplier = 1f;

        float defenseMultiplier = 1f;

        // =========================
        // Escalado por tier
        // =========================

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

        // =========================
        // Aplicar modificadores
        // =========================
        stats.SetHealthMultiplier(
            healthMultiplier
        );

        stats.SetDefenseMultiplier(
            defenseMultiplier
        );

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Defensa por vida (" + "-" + ((1f - healthMultiplier) * 100f) + "% vida) " + "-" + " " + "T" + tier;

        skill.description = "-" + ((1f - healthMultiplier) * 100f) + "% vida, +" + ((defenseMultiplier - 1f) * 100f) + "% defensa";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
