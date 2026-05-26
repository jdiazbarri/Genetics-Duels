using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D zoneCollider;

    public bool HasPlayers()
    {
        Collider2D[] hits =
            Physics2D.OverlapBoxAll(
                zoneCollider.bounds.center,
                zoneCollider.bounds.size,
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

