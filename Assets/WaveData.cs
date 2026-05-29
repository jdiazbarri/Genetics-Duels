using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase preparada para almacenar datos de oleadas.

// Actualmente no se utiliza directamente en el sistema final, pero puede servir como base para un sistema más automatizado de niveles
[System.Serializable]
public class WaveData
{
    // Lista de enemigos que aparecerán en la oleada
    public List<GameObject> enemies;
}
