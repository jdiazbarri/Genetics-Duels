using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Habilidad especial obtenida cuando dos personajes compatibles comparten el mismo grupo sanguíneo.
//
// Representa una bonificación genética que aumenta todas las estadísticas principales del personaje.
// A diferencia del resto de habilidades, no posee tiers y siempre aplica el mismo multiplicador.
public class Inbreeding : MonoBehaviour, Skills
{
    // Referencia a estadísticas del personaje
    private CharacterStats stats;

    private float multiplier = 1.5f;

    void Start()
    {
        // =========================
        // Aplicar modificadores
        // =========================

        stats = GetComponent<CharacterStats>();

        stats.SetHealthMultiplier(multiplier);

        stats.SetDamageMultiplier(multiplier);

        stats.SetDefenseMultiplier(multiplier);

        stats.SetAttackSpeedMultiplier(multiplier);

        // ===================================
        // Información visual de la habilidad
        // ===================================

        SkillInfo skill = new SkillInfo();

        skill.skillName = "Endogamia";

        skill.description = "Todas las stats x1.5";

        stats.skills.Add(skill);
    }
}
