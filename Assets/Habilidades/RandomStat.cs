using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad que genera una mutación aleatoria.
//
// El personaje recibe una gran mejora en una estadística concreta mientras el resto deatributos son reducidos.
// El nivel de la mutación depende del tier generado.
public class RandomStat : MonoBehaviour, Skills
{
    // Nivel de rareza de la habilidad
    private int tier;

    // Referencia a estadísticas del personaje
    private CharacterStats stats;

    // Generar tier aleatorio y elegir estadistica a aumentar
    void Start()
    {
        stats = GetComponent<CharacterStats>();

        tier = stats.GenerateTier();

        int randomStat = Random.Range(0, 6);

        string boostedStat = "";

        float reduceMultiplier = 1f;

        float boostMultiplier = 1f;

        // =========================
        // Escalado por tiers 
        // =========================

        switch (tier)
        {
            case 1:

                reduceMultiplier = 0.6f;

                boostMultiplier = 3f;

                break;

            case 2:

                reduceMultiplier = 0.5f;

                boostMultiplier = 4f;

                break;

            case 3:

                reduceMultiplier = 0.4f;

                boostMultiplier = 5f;

                break;
        }

        // =========================
        // Reducir estadísticas base
        // =========================

        stats.SetDamageMultiplier(reduceMultiplier);

        stats.SetAttackSpeedMultiplier(reduceMultiplier);

        stats.SetDefenseMultiplier(reduceMultiplier);

        stats.SetHealthMultiplier(reduceMultiplier);

        stats.AddCritChance(-0.05f);

        stats.AddLifeSteal(-0.05f);

        // =============================
        // Potenciar estadistica elegida
        // =============================

        switch (randomStat)
        {
            // Daño
            case 0:

                stats.SetDamageMultiplier(boostMultiplier);
                boostedStat = "Daño";

                break;

            // Velocidad de ataque
            case 1:

                stats.SetAttackSpeedMultiplier(boostMultiplier);

                boostedStat = "Velocidad de Ataque";

                break;

            // Defensa
            case 2:

                stats.SetDefenseMultiplier(boostMultiplier);

                boostedStat = "Defensa";

                break;

            // Salud
            case 3:

                stats.SetHealthMultiplier(boostMultiplier);

                boostedStat = "Vida";

                break;

            // Probabilidad crítica
            case 4:

                switch (tier)
                {
                    case 1:

                        stats.AddCritChance(0.30f);

                        break;

                    case 2:

                        stats.AddCritChance(0.50f);

                        break;

                    case 3:

                        stats.AddCritChance(0.70f);

                        break;
                }

                boostedStat = "Crítico";

                break;

            // Robo de vida
            case 5:

                switch (tier)
                {
                    case 1:

                        stats.AddLifeSteal(0.15f);

                        break;

                    case 2:

                        stats.AddLifeSteal(0.30f);

                        break;

                    case 3:

                        stats.AddLifeSteal(0.50f);

                        break;
                }

                boostedStat = "Robo de vida";

                break;
        }

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Mutación (" + boostedStat + ")" + " " + "-" + " " + "T" + tier;

        skill.description = "Mejora un stat pero reduce el resto";

        // Evitar habilidades duplicadas
        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
