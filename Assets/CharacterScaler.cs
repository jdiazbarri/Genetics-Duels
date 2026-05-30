using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sistema encargado de escalar automáticamente las estadísticas de los personajes en función
// del nivel actual de la partida.
public class CharacterScaler : MonoBehaviour
{
    public void ScaleCharacter(GameObject character, int level, string type)
    {
        CharacterStats stats = character.GetComponent<CharacterStats>();

        // Validar que el personaje tenga estadísticas
        if (stats == null)
        {
            return;
        }

        float statBonus = 1f;

        // =========================
        // Escalado enemigos
        // =========================

        if (type == "Enemy")
        {
            // Aumento +5%
            statBonus = 1f + (level * 0.05f);
        }

        // =========================
        // Escalado aliados
        // Esta escalado no se aplica actualmente, pero existe con fin de permitir al juego tener un sistema de estadisticas más robusto en caso de expandirlo en un futuro
        // =========================

        if (type == "Player")
        {
            // Aumento +7%
            statBonus = 1f + (level * 0.07f);
        }

        // =========================
        // Aplicar escalado
        // =========================

        stats.baseDamage *= statBonus;

        stats.baseDefense *= statBonus;

        stats.SetHealthMultiplier(1f + (level * 0.05f));

        // Recalcular estadísticas finales
        stats.UpdateStats();

        // Restaurar vida completa
        stats.health = stats.maxHealth;
    }
}
