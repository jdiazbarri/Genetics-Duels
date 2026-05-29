using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que permite disparar múltiples proyectiles en cada ataque.
//
// El número de proyectiles adicionales depende del tier generado aleatoriamente.
//
// Esta habilidad únicamente afecta a personajes que utilizan ataques a distancia mediante proyectiles.
// Las unidades cuerpo a cuerpo no obtienen beneficio de este modificador.
public class MultipleAttacks : MonoBehaviour, Skills
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

        int extraProjectiles = 0;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                extraProjectiles = 2;

                break;

            case 2:

                extraProjectiles = 3;

                break;

            case 3:

                extraProjectiles = 4;

                break;
        }

        // =========================
        // Aplicar habilidad
        // =========================

        stats.AddProjectiles( extraProjectiles);

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Multidisparo (+" + extraProjectiles + ") " + "-" + " " + "T" + tier;

        skill.description = "Disparos " + extraProjectiles +" proyectiles extras";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
