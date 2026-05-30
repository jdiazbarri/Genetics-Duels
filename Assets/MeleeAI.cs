using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla el comportamiento de los personajes de combate cuerpo a cuerpo
public class MeleeAI : MonoBehaviour
{
    // =========================
    // Movimiento
    // =========================

    [Header("Movimiento")]
    [SerializeField]
    private float moveDelay = 1f;

    [SerializeField]
    private float tileSize = 100f;

    // =========================
    // Objetivos
    // =========================

    [Header("Tags")]
    [SerializeField]
    private string targetTag;

    private float timer;

    private Transform target;

    private Animator animator;

    private CharacterStats stats;

    // Controla si la IA puede actuar
    private bool canAct = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        // La IA permanece inactiva hasta el inicio del combate
        if (!canAct)
        {
            return;
        }

        timer += Time.deltaTime;

        float realDelay = moveDelay / stats.attackSpeed;

        // Buscar enemigo más cercano para moverse o atacar
        if (timer >= realDelay)
        {
            timer = 0;
            FindClosestTarget();
            MoveOneTile();
        }
    }

    // Activar IA
    public void ActivateAI()
    {
        canAct = true;
    }

    // Desactivar IA
    public void StopAI()
    {
        canAct = false;
    }

    // Busca el enemigo más cercano
    void FindClosestTarget()
    {
        GameObject[] targets =GameObject.FindGameObjectsWithTag(targetTag);

        float closestDistance = Mathf.Infinity;

        target = null;

        foreach (GameObject possibleTarget in targets)
        {
            // Ignorarse asímismo
            if (possibleTarget == gameObject)
            {
                continue;
            }

            float distance = Vector2.Distance( transform.position,possibleTarget.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = possibleTarget.transform;
            }
        }
    }

    // Avanza una casilla hacia el enemigo o ataca si está suficientemente cerca
    void MoveOneTile()
    {
        // No hacer nada si no hay objetivo
        if (target == null)
        {
            return;
        }

        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;
        float distance =Vector2.Distance(currentPos, targetPos);
        float attackDistance = tileSize * 1.5f;

        // Atacar si está dentro del rango
        if (distance <= attackDistance)
        {
            AttackTarget();
            return;
        }

        Vector3 newPos = currentPos;
        float diffX = targetPos.x - currentPos.x;
        float diffY = targetPos.y - currentPos.y;

        // Movimiento en cuadrícula
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
        {
            if (diffX > 0)
            {
                newPos.x += tileSize;
            }
            else
            {
                newPos.x -= tileSize;
            }
        }
        else
        {
            if (diffY > 0)
            {
                newPos.y += tileSize;
            }
            else
            {
                newPos.y -= tileSize;
            }
        }

        // Evitar ocupar el mismo tile
        if (!IsTileOccupied(newPos))
        {
            StartCoroutine(MoveWithAnimation(newPos));
        }
    }

    // Reproduce la animación de movimiento antes de actualizar la posición.
    IEnumerator MoveWithAnimation(Vector3 newPos)
    {
        animator.SetBool("isWalking", true);
        yield return new WaitForSeconds(0.3f);
        transform.position = newPos;
        animator.SetBool("isWalking",false);
    }

    // Inicia la secuencia de ataque
    void AttackTarget()
    {
        StartCoroutine(MultiAttackCoroutine());
    }

    // Ejecuta todos los ataques del personaje
    IEnumerator MultiAttackCoroutine()
    {
        int attackCount = stats.attackCount;

        for (int i = 0; i < attackCount; i++)
        {
            if (target == null)
            {
                yield break;
            }

            CharacterStats targetStats = target.GetComponent<CharacterStats>();

            // Objetivo inválido
            if (targetStats == null)
            {
                yield break;
            }

            animator.SetTrigger("Attack");

            // =========================
            // Cálculo de daño
            // =========================

            // Daño base
            float damage = stats.damage;

            // Crítico
            bool criticalHit = Random.value < stats.criticalChance;

            if (criticalHit)
            {
                damage *= 2f;
            }

            // Reducción por defensa
            float finalDamage =damage - targetStats.defense;

            if (finalDamage < 1)
            {
                finalDamage = 1;
            }

            // Comprobar si está muerto el objetivo
            if (targetStats.health <= 0)
            {
                yield break;
            }

            // Hacer daño
            targetStats.health -= finalDamage;

            // =========================
            // Efectos de sonido
            // =========================

            if (CompareTag("Player"))
            {
                SoundManager.instance.PlayPlayerHitSound();
            }
            else
            {
                SoundManager.instance.PlayEnemyHitSound();
            }

            // =========================
            // Efectos de ataque
            // =========================

            float heal = finalDamage * stats.lifeSteal;

            stats.health += heal;

            if (stats.health > stats.maxHealth)
            {
                stats.health = stats.maxHealth;
            }

            Poison poison = GetComponent<Poison>();

            if (poison != null)
            {
                PoisonEffect effect = target.gameObject.GetComponent<PoisonEffect>();

                if (effect == null)
                {
                    effect = target.gameObject.AddComponent<PoisonEffect>();
                }

                effect.ApplyPoison( damage * 0.03f, 3f);
            }

            // Dejar de aplicar veneno
            if (targetStats.health <= 0)
            {
                Destroy(target.gameObject);
                yield break;
            }
            
            // Delay entre golpes
            yield return new WaitForSeconds(0.15f);
        }
    }

    // Comprueba si una casilla del tablero ya está ocupada por una unidad
    bool IsTileOccupied(Vector3 position)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag( "Player");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag( "Enemigo");

        // Comprobar aliados
        foreach (GameObject unit in units)
        {
            if (Vector3.Distance(unit.transform.position, position) < 1f)
            {
                return true;
            }
        }

        //Comprobar enemigo
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance( enemy.transform.position, position) < 1f)
            {
                return true;
            }
        }
        // Casillla libre
        return false;
    }
}