using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Representa una casilla individual del tablero.
//
// Cada tile almacena información relacionada con ocupación, posición y zona permitida para la colocación de unidades.
public class GridTile : MonoBehaviour
{
    public bool occupied = false;

    public bool playerZone = false;

    public int row;

    public int col;
}
