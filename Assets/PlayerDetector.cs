using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sistema auxiliar para detectar si existe al menos un personaje del jugador dentro de una zona determinada.
public class PlayerDetector : MonoBehaviour
{
    // Devuelve true si se detecta algún objeto con la etiqueta "Player" dentro del área.
    public bool HasPlayers()
    {
        Collider2D[] hits =
            Physics2D.OverlapBoxAll(
                transform.position,
                transform.localScale,
                0f
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
