using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que permite atacar múltiples objetivos de forma simultánea.
//
// El número de enemigos adicionales afectados depende del tier generado aleatoriamente.
public class MultipleBlow : MonoBehaviour, Skills
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

        int extraHits = 0;

        // =========================
        // Escalado por tier
        // =========================
        switch (tier)
        {
            case 1:

                extraHits = 2;

                break;

            case 2:

                extraHits = 3;

                break;

            case 3:

                extraHits = 4;

                break;
        }

        // =========================
        // Aplicar habilidad
        // =========================

        stats.AddAttacks( extraHits);

        // ===================================
        // Información visual de la habilidad
        // ===================================
 
        SkillInfo skill = new SkillInfo();

        skill.skillName = "Multiataque (+" + extraHits + ") " + "-" + " " + "T" + tier;

        skill.description = "Permite " + extraHits +" golpes extras";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
