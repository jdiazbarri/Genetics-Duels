using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que permite aplicar daño periódico de veneno a los enemigos tras impactar un ataque.
//
// La intensidad y duración del veneno dependen del tier generado aleatoriamente.
public class Poison : MonoBehaviour, Skills
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

        float poisonPercent = 0f;

        float poisonDuration = 0f;

        // =========================
        // Escalado por tier
        // =========================

        switch (tier)
        {
            case 1:

                poisonPercent = 0.03f;

                poisonDuration = 2f;

                break;

            case 2:

                poisonPercent = 0.05f;

                poisonDuration = 3f;

                break;

            case 3:

                poisonPercent = 0.08f;

                poisonDuration = 5f;

                break;
        }

        // ===================================
        // Información visual de la habilidad
        // ====================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Veneno (" + (poisonPercent * 100f) + "%) " + " - " + " " + "T" + tier;

        skill.description = "Aplica " + (poisonPercent * 100f) + "% daño " + poisonDuration + "s";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
