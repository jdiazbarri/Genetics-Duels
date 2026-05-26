using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 600f;

    private Transform target;

    private float damage;

    // COPIAS DE STATS
    private float critChance;

    private float lifeSteal;

    private bool hasPoison;

    // RECIBIR DATOS
    public void SetTarget(
        Transform newTarget,
        float newDamage,
        CharacterStats newOwner
    )
    {
        target = newTarget;

        damage = newDamage;

        // COPIAR STATS
        critChance =
            newOwner.critico;

        lifeSteal =
            newOwner.roboVida;

        hasPoison =
            newOwner.GetComponent<Veneno>()
            != null;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        // DIRECCIÓN
        Vector2 direction =
            target.position
            - transform.position;

        // ROTACIÓN
        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            ) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(
                0,
                0,
                angle - 230f
            );

        // MOVIMIENTO
        transform.position =
            Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );

        // DISTANCIA
        float distance =
            Vector2.Distance(
                transform.position,
                target.position
            );

        // IMPACTO
        if (distance < 10f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        CharacterStats targetStats =
            target.GetComponent<CharacterStats>();

        // TARGET INVÁLIDO
        if (targetStats == null)
        {
            Destroy(gameObject);

            return;
        }

        // TARGET YA MUERTO
        if (targetStats.vida <= 0)
        {
            Destroy(gameObject);

            return;
        }

        // DAÑO BASE
        float baseDamage =
            damage;

        // CRÍTICO
        bool criticalHit =
            Random.value < critChance;

        if (criticalHit)
        {
            baseDamage *= 2f;
        }

        // DEFENSA
        float finalDamage =
            baseDamage
            - targetStats.defensa;

        // DAÑO MÍNIMO
        if (finalDamage < 1)
        {
            finalDamage = 1;
        }

        // HACER DAÑO
        targetStats.vida -= finalDamage;

        // EVITAR NEGATIVOS
        if (targetStats.vida < 0)
        {
            targetStats.vida = 0;
        }

        // VENENO
        if (hasPoison)
        {
            PoisonEffect effect =
                target.gameObject.GetComponent<PoisonEffect>();

            if (effect == null)
            {
                effect =
                    target.gameObject.AddComponent<PoisonEffect>();
            }

            effect.ApplyPoison(
                baseDamage * 0.03f,
                3f
            );
        }

        // ROBO VIDA
        // SOLO SI EL TARGET SIGUE VIVO
        if (targetStats.vida > 0)
        {
            // Aquí podrías curar al owner
            // si más adelante guardas referencia segura
        }

        // MORIR
        if (targetStats.vida <= 0)
        {
            Destroy(target.gameObject);
        }

        // DESTRUIR PROYECTIL
        Destroy(gameObject);
    }
}
