using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionMachine : MonoBehaviour
{
    [Header("Slots")]
    public Transform slotA;

    public Transform slotB;

    public Transform resultSlot;

    private bool fusionDone = false;

    void Update()
    {
        CheckFusion();
    }

    void CheckFusion()
    {
        // NECESITA 2 PERSONAJES
        if (
            slotA.childCount > 0
            && slotB.childCount > 0
            && !fusionDone
        )
        {
            fusionDone = true;

            GameObject parentA =
                slotA.GetChild(0).gameObject;

            GameObject parentB =
                slotB.GetChild(0).gameObject;

            CharacterStats statsA =
                parentA.GetComponent<CharacterStats>();

            CharacterStats statsB =
                parentB.GetComponent<CharacterStats>();

            GameObject original;

            // SKIN RANDOM
            if (Random.value < 0.5f)
                original = parentA;
            else
                original = parentB;

            // CREAR HIJO
            GameObject child =
                Instantiate(
                    original,
                    resultSlot.position,
                    Quaternion.identity
                );

            // ESCALA
            child.transform.localScale =
                new Vector3(
                    1689.3324f,
                    1261.11792f,
                    1f
                );

            // SIN PADRE
            child.transform.SetParent(null);

            // COLOR RANDOM
            SpriteRenderer sr =
                child.GetComponentInChildren<SpriteRenderer>();

            if (sr != null)
            {
                sr.color =
                    new Color(
                        Random.value,
                        Random.value,
                        Random.value
                    );
            }

            // STATS HIJO
            CharacterStats childStats =
                child.GetComponent<CharacterStats>();

            // GENÉTICA
            childStats.vidaMaxima =
                GenerateGene(
                    statsA.vidaMaxima,
                    statsB.vidaMaxima
                );

            childStats.dFisico =
                GenerateGene(
                    statsA.dFisico,
                    statsB.dFisico
                );

            childStats.velocidadAtaque =
                GenerateGene(
                    statsA.velocidadAtaque,
                    statsB.velocidadAtaque
                );

            childStats.defensa =
                GenerateGene(
                    statsA.defensa,
                    statsB.defensa
                );

            childStats.critico =
                GenerateGene(
                    statsA.critico,
                    statsB.critico
                );

            childStats.roboVida =
                GenerateGene(
                    statsA.roboVida,
                    statsB.roboVida
                );

            // VIDA COMPLETA
            childStats.vida =
                childStats.vidaMaxima;

            // TIPO DE SANGRE RANDOM
            if (Random.value < 0.5f)
                childStats.tipoSangre =
                    statsA.tipoSangre;
            else
                childStats.tipoSangre =
                    statsB.tipoSangre;

            // HERENCIA DE HABILIDADES

            // LISTA ÚNICA
            List<System.Type> inheritedSkills =
                new List<System.Type>();

            // PADRE A
            MonoBehaviour[] componentsA =
                parentA.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour comp
                in componentsA)
            {
                if (comp is Habilidad)
                {
                    System.Type type =
                        comp.GetType();

                    // EVITAR DUPLICADOS
                    if (!inheritedSkills.Contains(type))
                    {
                        inheritedSkills.Add(type);
                    }
                }
            }

            // PADRE B
            MonoBehaviour[] componentsB =
                parentB.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour comp
                in componentsB)
            {
                if (comp is Habilidad)
                {
                    System.Type type =
                        comp.GetType();

                    // EVITAR DUPLICADOS
                    if (!inheritedSkills.Contains(type))
                    {
                        inheritedSkills.Add(type);
                    }
                }
            }

            // HEREDAR SKILLS
            foreach (System.Type skillType
                in inheritedSkills)
            {
                // 70% PROBABILIDAD
                if (Random.value <= 0.7f)
                {
                    child.AddComponent(skillType);
                }
            }

            // ENDOGAMIA EXTRA
            if (
                statsA.tipoSangre
                == statsB.tipoSangre
            )
            {
                child.AddComponent<Endogamia>();
            }

            // TEST
            Debug.Log(
                "========== FUSIÓN =========="
            );

            Debug.Log(
                "PADRE A: "
                + statsA.nombre
                + " | Sangre: "
                + statsA.tipoSangre
            );

            Debug.Log(
                "PADRE B: "
                + statsB.nombre
                + " | Sangre: "
                + statsB.tipoSangre
            );

            Debug.Log(
                "HIJO: "
                + childStats.nombre
            );

            Debug.Log(
                "Sangre heredada: "
                + childStats.tipoSangre
            );

            Debug.Log(
                "Vida: "
                + childStats.vidaMaxima
            );

            Debug.Log(
                "Daño: "
                + childStats.dFisico
            );

            Debug.Log(
                "Velocidad Ataque: "
                + childStats.velocidadAtaque
            );

            Debug.Log(
                "Defensa: "
                + childStats.defensa
            );

            Debug.Log(
                "Crítico: "
                + childStats.critico
            );

            Debug.Log(
                "Robo Vida: "
                + childStats.roboVida
            );

            // DESTRUIR PADRES
            Destroy(parentA);

            Destroy(parentB);
        }

        // RESET
        if (
            slotA.childCount == 0
            && slotB.childCount == 0
        )
        {
            fusionDone = false;
        }
    }

    float GenerateGene(
        float valueA,
        float valueB
    )
    {
        float min =
            Mathf.Min(valueA, valueB);

        float max =
            Mathf.Max(valueA, valueB);

        float mid =
            (min + max) / 2f;

        float roll =
            Random.Range(0f, 100f);

        // 15% SUPERIOR
        if (roll <= 15f)
        {
            return Random.Range(
                max,
                max * 1.2f
            );
        }

        // 5% INFERIOR
        if (roll <= 20f)
        {
            return Random.Range(
                min * 0.8f,
                min
            );
        }

        // 50% ENTRE MID Y MAX
        if (roll <= 70f)
        {
            return Random.Range(
                mid,
                max
            );
        }

        // 30% ENTRE MIN Y MID
        return Random.Range(
            min,
            mid
        );
    }
}
    
