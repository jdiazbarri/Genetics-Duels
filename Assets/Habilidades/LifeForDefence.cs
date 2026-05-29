using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que intercambia defensa por vida.
//
// El personaje pierde una parte de su defensaa cambio de aumentar su vida máxima.
public class LifeForDefence : MonoBehaviour, Skills
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

        float defenseMultiplier = 1f;

        float healthMultiplier = 1f;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                defenseMultiplier = 0.5f;

                healthMultiplier = 1.3f;

                break;

            case 2:

                defenseMultiplier = 0.4f;

                healthMultiplier = 1.6f;

                break;

            case 3:

                defenseMultiplier = 0.3f;

                healthMultiplier = 2f;

                break;
        }

        // =========================
        // Aplicar modificadores
        // =========================

        stats.SetDefenseMultiplier(defenseMultiplier);

        stats.SetHealthMultiplier( healthMultiplier);

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Vida por defensa (" + "-" + ((1f - defenseMultiplier) * 100f) + "% defensa) " + "-"+ " " + "T" + tier;

        skill.description = "-" + ((1f - defenseMultiplier) * 100f) + "% defensa, +" + ((healthMultiplier - 1f) * 100f) + "% vida";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
