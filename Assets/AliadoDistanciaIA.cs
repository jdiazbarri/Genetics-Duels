using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliadoDistanciaIA : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private float attackRange = 1500f;

    [SerializeField]
    private float attackCooldown = 2f;

    [SerializeField]
    private string targetTag;

    private float timer;

    private Transform target;

    private CharacterStats stats;

    private Animator animator;

    private bool canAct = false;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canAct)
            return;

        timer += Time.deltaTime;

        List<Transform> targets =
            FindClosestEnemies();

        if (targets.Count > 0)
        {
            float distance = Vector2.Distance(
                transform.position,
                targets[0].position
            );

            if (distance <= attackRange)
            {
                float realCooldown =
                    attackCooldown / stats.velocidadAtaque;

                if (timer >= realCooldown)
                {
                    timer = 0;

                    ShootArrow();
                }
            }
        }
    }

    List<Transform> FindClosestEnemies()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(
                targetTag
            );

        List<GameObject> enemyList =
            new List<GameObject>(enemies);

        enemyList.Sort((a, b) =>
        {
            float distA = Vector2.Distance(
                transform.position,
                a.transform.position
            );

            float distB = Vector2.Distance(
                transform.position,
                b.transform.position
            );

            return distA.CompareTo(distB);
        });

        List<Transform> targets =
            new List<Transform>();

        int maxTargets =
            stats.numeroObjetivos;

        for (int i = 0;
            i < enemyList.Count &&
            i < maxTargets;
            i++)
        {
            targets.Add(
                enemyList[i].transform
            );
        }

        return targets;
    }

    void ShootArrow()
    {
        StartCoroutine(
            ShootMultipleProjectiles()
        );
    }

    IEnumerator ShootMultipleProjectiles()
    {
        List<Transform> targets =
            FindClosestEnemies();

        int projectileCount =
            stats.numeroProyectiles;

        foreach (Transform enemy in targets)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                GameObject arrow = Instantiate(
                    arrowPrefab,
                    shootPoint.position,
                    Quaternion.identity
                );

                ArrowProjectile projectile =
                    arrow.GetComponent<ArrowProjectile>();

                projectile.SetTarget(
                    enemy,
                    stats.dFisico,
                    stats
                );

                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    public void ActivateAI()
    {
        canAct = true;
    }
}
