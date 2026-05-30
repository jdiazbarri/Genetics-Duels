using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona el proceso de fusión entre dos personajes.
//
// La fusión consume una ficha especial y genera un descendiente que hereda estadísticas y habilidades de sus progenitores.
public class FusionMachine : MonoBehaviour
{
    // =========================
    // Slots de fusión
    // =========================

    // Primer progenitor
    [Header("Slots")]
    public Transform slotA;

    // Segundo progenitor
    public Transform slotB;

    // Posición donde aparecerá el descendiente
    public Transform resultSlot;

    private bool fusionDone = false;

    // Comprobar continuamente si existe una fusión disponible
    void Update()
    {
        CheckFusion();
    }

    void CheckFusion()
    {
        // Comprobar que se han seleccionado dos padres y que la fusión no se ha realizado todavia
        if (slotA.childCount > 0 && slotB.childCount > 0 && !fusionDone)
        {
            fusionDone = true;

            // Gastar ficha de fusión
            if (!BattleManager.instance.UseCoin(1))
            {
                fusionDone = false;
                return;
            }

            // Obtener a los progenitores junto a sus estadisticas 
            GameObject parentA = slotA.GetChild(0).gameObject;
            GameObject parentB = slotB.GetChild(0).gameObject;

            CharacterStats statsA = parentA.GetComponent<CharacterStats>();
            CharacterStats statsB = parentB.GetComponent<CharacterStats>();

            GameObject original;

            // Seleccionar la skin a heredar
            if (Random.value < 0.5f)
            {
                original = parentA;
            }
            else
            {
                original = parentB;
            }

            // =========================
            // Creación del descendiente
            // =========================

            // Crear hijo
            GameObject child = Instantiate(original, resultSlot.position, Quaternion.identity);

            // Tamaño del hijo 
            child.transform.localScale = new Vector3(1689.3324f, 1261.11792f, 1f);

            // Eliminamos el parentezco 
            child.transform.SetParent(null);

            // Color de la skin del hijo
            SpriteRenderer sr = child.GetComponentInChildren<SpriteRenderer>();

            if (sr != null)
            {
                sr.color =new Color( Random.value, Random.value, Random.value);
            }

            // Generar estadísticas del hijo
            CharacterStats childStats = child.GetComponent<CharacterStats>();

            // =========================
            // Herencia genética
            // =========================

            childStats.maxHealth = GenerateGene(statsA.maxHealth,statsB.maxHealth);

            childStats.damage = GenerateGene( statsA.damage,statsB.damage);

            childStats.attackSpeed = GenerateGene( statsA.attackSpeed, statsB.attackSpeed);

            childStats.defense = GenerateGene( statsA.defense, statsB.defense);

            childStats.criticalChance = GenerateGene( statsA.criticalChance, statsB.criticalChance);

            childStats.lifeSteal = GenerateGene( statsA.lifeSteal, statsB.lifeSteal);

            childStats.health = childStats.maxHealth;

            // Tipo de sangre
            if (Random.value < 0.5f)
                childStats.bloodTypes = statsA.bloodTypes;
            else
                childStats.bloodTypes = statsB.bloodTypes;

            // =========================
            // Herencia de habilidades
            // =========================

            List<System.Type> inheritedSkills = new List<System.Type>();

            // Padre A
            MonoBehaviour[] componentsA = parentA.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour comp in componentsA)
            {
                if (comp is Skills)
                {
                    System.Type type = comp.GetType();

                    // Evitar duplicados
                    if (!inheritedSkills.Contains(type))
                    {
                        inheritedSkills.Add(type);
                    }
                }
            }

            // Padre B
            MonoBehaviour[] componentsB =
                parentB.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour comp in componentsB)
            {
                if (comp is Skills)
                {
                    System.Type type = comp.GetType();

                    // Evitar duplicados
                    if (!inheritedSkills.Contains(type))
                    {
                        inheritedSkills.Add(type);
                    }
                }
            }

            // Heredar habilidades
            foreach (System.Type skillType in inheritedSkills)
            {
                if (Random.value <= 0.7f)
                {
                    child.AddComponent(skillType);
                }
            }

            // Efecto de endogamia
            if (statsA.bloodTypes == statsB.bloodTypes)
            {
                child.AddComponent<Inbreeding>();
            }

            // Destruir padres
            Destroy(parentA);
            Destroy(parentB);
        }

        // Reiniciar máquina
        if (slotA.childCount == 0 && slotB.childCount == 0)
        {
            fusionDone = false;
        }
    }

    // Algoritmo génetico 
    float GenerateGene( float statA, float statB)
    {
        float min = Mathf.Min(statA, statB);

        float max = Mathf.Max(statA, statB);

        float mid = (min + max) / 2f;

        float rng = Random.Range(0f, 100f);

        // 15% SUPERIOR AL MAX
        if (rng <= 15f)
        {
            return Random.Range( max, max * 1.2f);
        }

        // 5% INFERIOR AL MIN
        if (rng <= 20f)
        {
            return Random.Range(
                min * 0.8f, min);
        }

        // 50% ENTRE MEDIA Y MAX
        if (rng <= 70f)
        {
            return Random.Range(mid, max);
        }

        // 30% ENTRE MIN Y MEDIA
        return Random.Range( min, mid);
    }
}
    
