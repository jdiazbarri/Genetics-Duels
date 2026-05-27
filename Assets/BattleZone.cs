using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTag tag = other.GetComponent<PlayerTag>();

            if (tag != null)
                tag.isInsideBattleZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTag tag = other.GetComponent<PlayerTag>();

            if (tag != null)
                tag.isInsideBattleZone = false;
        }
    }

    public bool HasPlayers()
    {
        PlayerTag[] all =
            GameObject.FindObjectsOfType<PlayerTag>();

        foreach (var p in all)
        {
            if (p.isInsideBattleZone)
                return true;
        }

        return false;
    }
}

