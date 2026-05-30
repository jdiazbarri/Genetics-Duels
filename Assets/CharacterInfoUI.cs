using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Sistema encargado de mostrar en pantalla la información detallada de un personaje,
// incluyendo estadísticas y habilidades.
public class CharacterInfoUI : MonoBehaviour
{
    // Instancia global accesible desde otros sistemas
    public static CharacterInfoUI instance;

    // Texto donde se muestran los datos
    [SerializeField]
    private TextMeshProUGUI infoText;

    // Panel visual que contiene la información
    [SerializeField]
    private GameObject panel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    // Mostrar información del personaje 
    public void ShowInfo(CharacterStats stats)
    {
        panel.SetActive(true);

        string skillText = "";

        // Construir lista de habilidades
        foreach (SkillInfo skill in stats.skills)
        {
            skillText += "\n• " + skill.skillName;
        }

        // Construcción dinámica del texto mostrado
        infoText.text =
            "Nombre: "
            + stats.characterName
            + "\n"

            + "Sangre: "
            + stats.bloodTypes
            + "\n"

            + "• Vida: "
            + FormatStat(stats.health)
            + "/"
            + FormatStat(stats.maxHealth)
            + "\n"

            + "• Daño: "
            + FormatStat(stats.damage)
            + "\n"

            + "• Vel. Ataque: "
            + FormatStat(stats.attackSpeed)
            + "\n"

            + "• Crítico: "
            + FormatPercent(stats.criticalChance)
            + "\n"

            + "• Robo Vida: "
            + FormatPercent(stats.lifeSteal)
            + "\n"

            + "• Defensa: "
            + FormatStat(stats.defense)
            + "\n"

            + "\nHabilidades:"
            + skillText;
    }

    // =========================
    // Formato numérico
    // =========================

    string FormatStat(float value)
    {
 
        // MILES
        if (value >= 1000)
        {
            return (value / 1000f).ToString("0.#") + "K";
        }

        // Nornmal
        return value.ToString("0.##");
    }

    // =========================
    // Formato porcentajes
    // =========================

    string FormatPercent(float value)
    {
        return (value * 100f).ToString("0.##") + "%";
    }

    // Ocultar panel de información
    public void HideInfo()
    {
        infoText.text = "";
        panel.SetActive(false);
    }
}
