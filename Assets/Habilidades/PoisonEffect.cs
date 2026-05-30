using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Componente responsable de aplicar daño periódicoa una unidad afectada por veneno.
//
// El efecto se ejecuta mediante una corrutina que reduce la vida del objetivo durante un tiempo determinado.
public class PoisonEffect : MonoBehaviour
{
    private CharacterStats stats;

    // Aplicar efecto de veneno
    public void ApplyPoison(float damagePerSecond, float duration)
    {
        stats = GetComponent<CharacterStats>();

        StartCoroutine(PoisonCoroutine(damagePerSecond, duration));
    }

    // Corrutina encargada de aplicar el daño periódico
    IEnumerator PoisonCoroutine( float damagePerSecond, float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            // Aplicar daño de veneno
            stats.health -= damagePerSecond;

            // Eliminar unidad si muere
            if (stats.health <= 0)
            {
                Destroy(gameObject);

                yield break;
            }

            // Avanzar un segundo de duración
            timer += 1f;

            yield return new WaitForSeconds(1f);
        }
    }
}
