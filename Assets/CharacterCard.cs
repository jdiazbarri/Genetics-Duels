using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sistema de cartas seleccionables dentro del juego.
//
// Cuando el jugador pulsa la carta, Se encarga de notificar al tablero (CharacterBoard) para transformar la carta por una unidad.
public class CharacterCard : MonoBehaviour
{
    // Referencia al tablero principal 
    private CharacterBoard board;

    // Conjunto de personajes
    public GameObject[] characterPrefabs;

    // Inicializar referencia al tablero
    public void Init(CharacterBoard b)
    {
        board = b;
    }

    // Detectar selección de la carta
    void OnMouseDown()
    {
        if (board == null) return;

        board.OnCardClicked(this);
    }
}
