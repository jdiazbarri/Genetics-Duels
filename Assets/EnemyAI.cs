using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float moveDelay = 1f;

    [SerializeField]
    private float tileSize = 100f;

    private float timer;

    private Transform target;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        FindClosestPlayer();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveDelay)
        {
            timer = 0;

            FindClosestPlayer();

            MoveOneTile();
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(
                transform.position,
                player.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = player.transform;
            }
        }
    }

    void MoveOneTile()
    {
        if (target == null)
            return;

        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;

        float dx = Mathf.Abs(targetPos.x - currentPos.x);
        float dy = Mathf.Abs(targetPos.y - currentPos.y);

        float tolerance = 1f;

        bool adjacentHorizontal =
            Mathf.Abs(dx - tileSize) < tolerance && dy < tolerance;

        bool adjacentVertical =
            Mathf.Abs(dy - tileSize) < tolerance && dx < tolerance;

        // SI ESTÁ AL LADO ? atacar
        if (adjacentHorizontal || adjacentVertical)
        {
            AttackPlayer();

            return;
        }

        Vector3 newPos = currentPos;

        float diffX = targetPos.x - currentPos.x;
        float diffY = targetPos.y - currentPos.y;

        // Movimiento por grid
        if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
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

        StartCoroutine(MoveWithAnimation(newPos));
    }

    IEnumerator MoveWithAnimation(Vector3 newPos)
    {
        animator.SetBool("isWalking", true);

        // Tiempo para ver la animación caminar
        yield return new WaitForSeconds(0.3f);

        transform.position = newPos;

        animator.SetBool("isWalking", false);
    }

    void AttackPlayer()
    {
        animator.SetTrigger("Attack");

        CharacterStats enemyStats =
            GetComponent<CharacterStats>();

        CharacterStats targetStats =
            target.GetComponent<CharacterStats>();

        if (targetStats != null)
        {
            targetStats.vida -= enemyStats.dFisico;

            // Morir
            if (targetStats.vida <= 0)
            {
                Destroy(target.gameObject);
            }
        }
    }
}