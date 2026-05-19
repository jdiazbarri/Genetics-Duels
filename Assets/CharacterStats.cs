using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string nombre;

    public float vida;
    public float dFisico;
    public float dMagico;

    public float defFisica;
    public float defMagica;

    public float velocidadAtaque;

    public float roboVida;

    public float probCritico;

    public bool tipoUnidad;

    private void OnMouseEnter()
    {
        CharacterInfoUI.instance.ShowInfo(this);
    }

    private void OnMouseExit()
    {
        CharacterInfoUI.instance.HideInfo();
    }
}
