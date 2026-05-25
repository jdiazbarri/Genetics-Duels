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

        foreach (SkillInfo skill in stats.skills)
        {
            skillText += "\n•" + skill.skillName;
        }

        infoText.text =
            "Nombre: " + stats.nombre + "\n" +
            "•Vida: " + (stats.vida).ToString("0.##") + "/" + stats.vidaMaxima + "\n" +
            "•D. Físico: " + stats.dFisico + "\n" +
            "•Vel. Ataque: " + stats.velocidadAtaque + "\n" +
            "•Crítico: " + (stats.critico * 100).ToString("0.##") + "%" +"\n" +
            "•Robo Vida: " + (stats.roboVida * 100).ToString("0.##") + "%" + "\n" +
            "•Defensa: " + stats.defensa + "\n" +
            "\nHabilidades:" +
            skillText;
    }
    

    public void HideInfo()
    {
        infoText.text = "";
    }
}
