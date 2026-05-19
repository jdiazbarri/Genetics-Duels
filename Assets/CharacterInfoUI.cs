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
        infoText.text =
            "Nombre: " + stats.nombre + "\n" +
            "Vida: " + stats.vida + "\n" +
            "D. Físico: " + stats.dFisico + "\n" +
            "D. Mágico: " + stats.dMagico + "\n" +
            "Def. Física: " + stats.defFisica + "\n" +
            "Def. Mágica: " + stats.defMagica + "\n" +
            "Vel. Ataque: " + stats.velocidadAtaque + "\n" +
            "Robo Vida: " + stats.roboVida + "\n" +
            "P. Crítico: " + stats.probCritico + "\n";
    }

    public void HideInfo()
    {
        infoText.text = "";
    }


}
