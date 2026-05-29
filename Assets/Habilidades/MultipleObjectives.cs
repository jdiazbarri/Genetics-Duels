using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que permite atacar múltiples objetivos de forma simultánea.
//
// El número de enemigos adicionales afectados depende del tier generado aleatoriamente.
public class MultipleObjectives : MonoBehaviour, Skills
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

        int extraTargets = 1;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                extraTargets = 1;

                break;

            case 2:

                extraTargets = 2;

                break;

            case 3:

                extraTargets = 3;

                break;
        }

        // =========================
        // Aplicar habilidad
        // =========================

        stats.AddTargets(extraTargets);

        // ===================================
        // Información visual de la habilidad
        // ====================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Multiobjetivos (+" + extraTargets + ") " + "-" + " " + "T" + tier;

        skill.description = "Ataca " + extraTargets +" enemigos extras";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
