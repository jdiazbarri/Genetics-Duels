using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El sistema encargado de marcar automáticamente a los personajes que entran o salen del área de combate.
public class BattleZone : MonoBehaviour
{
    // Detectar entrada de aliados en la zona
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTag tag = other.GetComponent<PlayerTag>();

            if (tag != null)
                tag.isInsideBattleZone = true;
        }
    }

    // Detectar salida de aliados de la zona
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTag tag = other.GetComponent<PlayerTag>();

            if (tag != null)
                tag.isInsideBattleZone = false;
        }
    }

    // Comprobar si todavía quedan aliados
    // dentro del área activa de combate
    public bool HasPlayers()
    {
        PlayerTag[] all = GameObject.FindObjectsOfType<PlayerTag>();

        foreach (PlayerTag player in all)
        {
            if (player.isInsideBattleZone)
                return true;
        }

        return false;
    }
}

