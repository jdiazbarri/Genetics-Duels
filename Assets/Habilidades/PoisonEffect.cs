using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private CharacterStats stats;

    public void ApplyPoison(
        float damagePerSecond,
        float duration
    )
    {
        stats = GetComponent<CharacterStats>();

        StartCoroutine(
            PoisonCoroutine(
                damagePerSecond,
                duration
            )
        );
    }

    IEnumerator PoisonCoroutine(
        float damagePerSecond,
        float duration
    )
    {
        float timer = 0;

        while (timer < duration)
        {
            // HACER DAÑO
            stats.vida -= damagePerSecond;

            // MORIR
            if (stats.vida <= 0)
            {
                Destroy(gameObject);

                yield break;
            }

            timer += 1f;

            yield return new WaitForSeconds(1f);
        }
    }
}
