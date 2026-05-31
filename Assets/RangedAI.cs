using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla el comportamiento de los personajesde combate a distancia.
public class RangedAI : MonoBehaviour
{
    // Prefab del proyectil
    [SerializeField]
    private GameObject arrowPrefab;

    // Origen del proyectil
    [SerializeField]
    private Transform shootPoint;

    // Rango de ataque
    [SerializeField]
    private float attackRange = 1500f;

    // Tiempo base entre ataques
    [SerializeField]
    private float attackCooldown = 2f;

    // Tipo de objetivo
    [SerializeField]
    private string targetTag;

    // Temporizador de ataque
    private float timer;

    // Objetivo actual
    private Transform target;

    // Estadísticas del personaje
    private CharacterStats stats;

    // Animador del personaje.
    private Animator animator;

    // Controla si la IA puede actuar
    private bool canAct = false;

    // Obtener estadísticas y animador
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // La IA permanece inactiva hasta que comienza el combate
        if (!canAct)
        {
            return;
        }

        // Actualizar temporizador
        timer += Time.deltaTime;

        // Buscar objetivos cercanos
        List<Transform> targets = FindClosestEnemies();

        if (targets.Count > 0)
        {
            // Distancia al objetivo más cercano
            float distance = Vector2.Distance(transform.position, targets[0].position);

            // Comprobar si está dentro del alcance
            if (distance <= attackRange)
            {
                float realCooldown = attackCooldown / stats.attackSpeed;
                // Disparar cuando finaliza el cooldown
                if (timer >= realCooldown)
                {
                    timer = 0;
                    ShootArrow();
                }
            }
        }
    }

    // Busca enemigos y devuelve los más cercanos según el número de objetivos permitido
    List<Transform> FindClosestEnemies()
    {
        GameObject[] enemies =GameObject.FindGameObjectsWithTag(targetTag);
        List<GameObject> enemyList = new List<GameObject>(enemies);

        // Número máximo de objetivos que puede atacar el personaje
        enemyList.Sort((a, b) =>
        {
            float distA = Vector2.Distance(transform.position, a.transform.position);
            float distB = Vector2.Distance(transform.position, b.transform.position);
            return distA.CompareTo(distB);
        });

        List<Transform> targets = new List<Transform>();

        // Seleccionar únicamente los objetivos permitidos por la habilidad MultiTarget
        int maxTargets = stats.targetCount;

        // Seleccionar únicamente los objetivos permitidos por la habilidad MultiTarget
        for (int i = 0; i < enemyList.Count && i < maxTargets; i++)
        {
            targets.Add(enemyList[i].transform);
        }
        return targets;
    }

    // Inicia la secuencia de disparo
    void ShootArrow()
    {
        StartCoroutine( ShootMultipleProjectiles());
    }

    // Dispara proyectiles contra todos los objetivos seleccionados
    IEnumerator ShootMultipleProjectiles()
    {
        // Obtener enemigos objetivo
        List<Transform> targets = FindClosestEnemies();
        
        // Número de proyectiles por ataque
        int projectileCount =stats.projectileCount;

        foreach (Transform enemy in targets)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                // Creación y configuración del proyectil
                GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
                Projectile projectile = arrow.GetComponent<Projectile>();
                projectile.SetTarget(enemy,stats.damage, stats);
                // Separación temporal entre proyectil, para evitar que visualmente solo se vea uno
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    // Activa la IA cuando comienza la batalla
    public void ActivateAI()
    {
        canAct = true;
    }

    // Detiene la IA cuando finaliza la batalla
    public void StopAI()
    {
        canAct = false;
    }
}
