using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAI : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField]
    private float moveDelay = 1f;

    [SerializeField]
    private float tileSize = 100f;

    [Header("Tags")]
    [SerializeField]
    private string targetTag;

    private float timer;

    private Transform target;

    private Animator animator;

    private CharacterStats stats;

    // IA ACTIVADA
    private bool canAct = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        // NO HACER NADA
        // HASTA ACTIVAR IA
        if (!canAct)
            return;

        timer += Time.deltaTime;

        float realDelay =
            moveDelay / stats.velocidadAtaque;

        if (timer >= realDelay)
        {
            timer = 0;

            FindClosestTarget();

            MoveOneTile();
        }
    }

    // ACTIVAR IA
    public void ActivateAI()
    {
        canAct = true;
    }

    public void StopAI()
    {
        canAct = false;
    }

    void FindClosestTarget()
    {
        GameObject[] targets =
            GameObject.FindGameObjectsWithTag(
                targetTag
            );

        float closestDistance =
            Mathf.Infinity;

        target = null;

        foreach (GameObject possibleTarget
            in targets)
        {
            // IGNORARSE A SI MISMO
            if (possibleTarget == gameObject)
                continue;

            float distance =
                Vector2.Distance(
                    transform.position,
                    possibleTarget.transform.position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;

                target =
                    possibleTarget.transform;
            }
        }
    }

    void MoveOneTile()
    {
        if (target == null)
            return;

        Vector3 currentPos =
            transform.position;

        Vector3 targetPos =
            target.position;

        // DISTANCIA REAL
        float distance =
            Vector2.Distance(
                currentPos,
                targetPos
            );

        // DISTANCIA PARA ATACAR
        float attackDistance =
            tileSize * 1.5f;

        // SI ESTÁ CERCA -> ATACAR
        if (distance <= attackDistance)
        {
            AttackTarget();

            return;
        }

        Vector3 newPos = currentPos;

        float diffX =
            targetPos.x - currentPos.x;

        float diffY =
            targetPos.y - currentPos.y;

        // MOVIMIENTO GRID
        if (Mathf.Abs(diffX)
            > Mathf.Abs(diffY))
        {
            if (diffX > 0)
                newPos.x += tileSize;
            else
                newPos.x -= tileSize;
        }
        else
        {
            if (diffY > 0)
                newPos.y += tileSize;
            else
                newPos.y -= tileSize;
        }

        // EVITAR OCUPAR MISMO TILE
        if (!IsTileOccupied(newPos))
        {
            StartCoroutine(
                MoveWithAnimation(newPos)
            );
        }
    }

    IEnumerator MoveWithAnimation(
        Vector3 newPos)
    {
        animator.SetBool(
            "isWalking",
            true
        );

        yield return new WaitForSeconds(
            0.3f
        );

        transform.position = newPos;

        animator.SetBool(
            "isWalking",
            false
        );
    }

    void AttackTarget()
    {
        StartCoroutine(
            MultiAttackCoroutine()
        );
    }

    IEnumerator MultiAttackCoroutine()
    {
        int attackCount =
            stats.numeroAtaques;

        for (int i = 0;
            i < attackCount;
            i++)
        {
            if (target == null)
            {
                yield break;
            }

            CharacterStats targetStats =
                target.GetComponent<CharacterStats>();

            if (targetStats == null)
            {
                yield break;
            }

            animator.SetTrigger("Attack");

            // DAÑO BASE
            float damage =
                stats.dFisico;

            // CRÍTICO
            bool criticalHit =
                Random.value < stats.critico;

            if (criticalHit)
            {
                damage *= 2f;
            }

            // DEFENSA
            float finalDamage =
                damage - targetStats.defensa;

            if (finalDamage < 1)
            {
                finalDamage = 1;
            }

            // SI YA ESTÁ MUERTO (ver si eso corrige un bug)
            if (targetStats.vida <= 0)
            {
                yield break;
            }

            // HACER DAÑO
            targetStats.vida -= finalDamage;

            // ROBO VIDA
            float heal =
                finalDamage * stats.roboVida;

            stats.vida += heal;

            if (stats.vida >
                stats.vidaMaxima)
            {
                stats.vida =
                    stats.vidaMaxima;
            }

            // VENENO
            Veneno poison =
                GetComponent<Veneno>();

            if (poison != null)
            {
                PoisonEffect effect =
                    target.gameObject.GetComponent<PoisonEffect>();

                if (effect == null)
                {
                    effect =
                        target.gameObject.AddComponent<PoisonEffect>();
                }

                effect.ApplyPoison(
                    damage * 0.03f,
                    3f
                );
            }

            // MORIR
            if (targetStats.vida <= 0)
            {
                Destroy(target.gameObject);

                yield break;
            }

            // DELAY ENTRE GOLPES
            yield return new WaitForSeconds(
                0.15f
            );
        }
    }

    bool IsTileOccupied(Vector3 position)
    {
        GameObject[] units =
            GameObject.FindGameObjectsWithTag(
                "Player"
            );

        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(
                "Enemigo"
            );

        foreach (GameObject unit in units)
        {
            if (Vector3.Distance(
                unit.transform.position,
                position
            ) < 1f)
            {
                return true;
            }
        }

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(
                enemy.transform.position,
                position
            ) < 1f)
            {
                return true;
            }
        }

        return false;
    }
}