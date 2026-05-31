using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona el comportamiento de los proyectilesdisparados por personajes a distancia.
//
// El proyectil almacena una copia de las estadísticas necesarias del atacante para evitar depender de él una vez lanzado.
public class Projectile : MonoBehaviour
{
    // =========================
    // Configuración proyectil
    // =========================

    [SerializeField]
    private float speed = 600f;

    private Transform target;

    // =========================
    // Copia de estadísticas
    // =========================

    private float damage;
    private float critChance;
    private float lifeSteal;
    private CharacterStats owner;
    // Permite indentificar si puede usar veneno
    private bool hasPoison;
    // Permite identificar si el disparo pertenece al jugador o al enemigo
    private bool shotByPlayer;


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Direcíón del proyectil
        Vector2 direction = target.position - transform.position;

        // Rotación del proyectil
        float angle = Mathf.Atan2(  direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,angle - 230f);

        // Movimiento del proyectil
        transform.position = Vector2.MoveTowards( transform.position, target.position, speed * Time.deltaTime);

        // Distancia del proyectil
        float distance = Vector2.Distance( transform.position, target.position);

        // Impacto
        if (distance < 10f)
        {
            HitTarget();
        }
    }

    // Inicializa el proyectil con toda lainformación necesaria para el combate
    public void SetTarget(Transform newTarget, float newDamage, CharacterStats newOwner)
    {
        target = newTarget;
        damage = newDamage;
        // Copiar estadísticas relevantes del atacante.
        critChance = newOwner.criticalChance;
        lifeSteal = newOwner.lifeSteal;
        hasPoison = newOwner.GetComponent<Poison>() != null;
        shotByPlayer = newOwner.CompareTag("Player");
        owner = newOwner;
    }

    // Aplica el daño final al objetivo teniendo en cuenta críticos, defensa y efectos
    void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        CharacterStats targetStats = target.GetComponent<CharacterStats>();

        // Objetivo invalido
        if (targetStats == null)
        {
            Destroy(gameObject);
            return;
        }

        // Objetivo ya está muerto
        if (targetStats.health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Daño base
        float baseDamage = damage;

        // Daño crítico
        bool criticalHit = Random.value < critChance;

        if (criticalHit)
        {
            baseDamage *= 2f;
        }

        // Reduccion de daño por defensa
        float finalDamage = baseDamage - targetStats.defense;

        // Daño mínimo
        if (finalDamage < 1)
        {
            finalDamage = 1;
        }

        // Aplicar daño
        targetStats.health -= finalDamage;

        // =========================
        // Efectos de sonido
        // =========================

        if (shotByPlayer)
        {
            SoundManager.instance.PlayPlayerHitSound();
        }
        else
        {
            SoundManager.instance.PlayEnemyHitSound();
        }

        // Evitar valor negativos
        if (targetStats.health < 0)
        {
            targetStats.health = 0;
        }

        // =========================
        // Efectos de ataque
        // =========================

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

        if (owner != null && lifeSteal > 0)
        {
            float healAmount = finalDamage * lifeSteal;

            owner.health += healAmount;

            if (owner.health > owner.maxHealth)
            {
                owner.health = owner.maxHealth;
            }
        }

        // Eliminar objetivo si muere
        if (targetStats.health <= 0)
        {
            Destroy(target.gameObject);
        }

        // Eliminar proyectil
        Destroy(gameObject);
    }
}
