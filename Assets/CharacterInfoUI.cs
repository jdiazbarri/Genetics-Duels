using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterInfoUI : MonoBehaviour
{
    public static CharacterInfoUI instance;

    [SerializeField]
    private TextMeshProUGUI infoText;

    private void Awake()
    {
        instance = this;
    }

    public void ShowInfo(CharacterStats stats)
    {
        string skillText = "";

        foreach (SkillInfo skill
            in stats.skills)
        {
            skillText +=
                "\n• "
                + skill.skillName;
        }

        infoText.text =
            "Nombre: "
            + stats.nombre
            + "\n"

            + "Sangre: "
            + stats.tipoSangre
            + "\n"

            + "•Vida: "
            + FormatStat(stats.vida)
            + "/"
            + FormatStat(stats.vidaMaxima)
            + "\n"

            + "•D. Físico: "
            + FormatStat(stats.dFisico)
            + "\n"

            + "•Vel. Ataque: "
            + FormatStat(stats.velocidadAtaque)
            + "\n"

            + "•Crítico: "
            + FormatPercent(stats.critico)
            + "\n"

            + "•Robo Vida: "
            + FormatPercent(stats.roboVida)
            + "\n"

            + "•Defensa: "
            + FormatStat(stats.defensa)
            + "\n"

            + "\nHabilidades:"
            + skillText;
    }

    // FORMATO NUMÉRICO
    string FormatStat(float value)
    {
        // MILLONES En principio no se deberia llegar aquí
        if (value >= 1000000)
        {
            return
                (value / 1000000f)
                .ToString("0.#")
                + "M";
        }

        // MILES
        if (value >= 1000)
        {
            return
                (value / 1000f)
                .ToString("0.#")
                + "K";
        }

        // NORMAL
        return value.ToString("0.##");
    }

    // FORMATO %
    string FormatPercent(float value)
    {
        return
            (value * 100f)
            .ToString("0.##")
            + "%";
    }

    public void HideInfo()
    {
        infoText.text = "";
    }
}
