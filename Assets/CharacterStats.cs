using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string nombre;

    // VIDA
    public float vidaMaxima = 100;

    public float vida = 100;

    // DAÑO
    public float dFisico = 10;

    public float dMagico = 10;

    // DEFENSAS
    public float defFisica = 20;

    public float defMagica = 20;

    // OTROS
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
