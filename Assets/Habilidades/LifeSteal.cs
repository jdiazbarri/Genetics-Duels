using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que permite recuperar vida al atacar.
//
// La cantidad de vida recuperada depende del tier generado aleatoriamente.
public class LifeSteal : MonoBehaviour, Skills
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

        float lifeSteal = 0f;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                lifeSteal = 0.10f;

                break;

            case 2:

                lifeSteal = 0.20f;

                break;

            case 3:

                lifeSteal = 0.35f;

                break;
        }

        // =========================
        // Aplicar habilidad
        // =========================

        stats.AddLifeSteal(lifeSteal);

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Robo de vida (" + (lifeSteal * 100f) +"%) " + "-" + " " + "T" + tier;

        skill.description = (lifeSteal * 100f) + "% de daño curado";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
