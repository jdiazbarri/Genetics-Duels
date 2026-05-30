using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Almacena todas las estadísticas de combate de un personaje.
//
// Esta clase actúa como núcleo del sistema de atributos,permitiendo aplicar modificadores procedentes de habilidades y mecánicas genéticas.
public class CharacterStats : MonoBehaviour
{
    // =========================
    // Información personaje
    // =========================

    public string characterName = "Nombre";

    // Vida base
    public float maxHealth = 0;
    public float health = 0;

    // Valores base
    public float baseDamage = 0f;
    public float baseDefense = 0f;
    public float baseAttackSpeed = 1f;
    public float baseCriticalChance = 0f;
    public float baseLifeSteal = 0f;

    // Efectos de ataque
    public int projectileCount = 1;
    public int attackCount = 1;
    public int targetCount = 1;

    // Tipo de sangre
    public string bloodTypes;

    // Valores finales
    public float damage;
    public float attackSpeed;
    public float criticalChance;
    public float lifeSteal;
    public float defense;

    // MULTIPLICADORES
    private float damageMultiplier = 1f;
    private float attackSpeedMultiplier = 1f;
    private float defenseMultiplier = 1f;

    // BONUS PLANOS
    private float critBonus = 0f;
    private float lifeStealBonus = 0f;

    // HABILIDADES ACTIVAS
    public List<SkillInfo> skills = new List<SkillInfo>();

    // Tipos de sangre
    string[] bloodTypesList =
        {
        "A","B","C","D",
        "E","F","G","H",
        "I","J","K","L"
        };

    // Crear conjunto de estadisticas del personaje
    void Start()
    {
        // Asignar un tipo de sangre
        AssignBloodType();
        // Calcular estadísticas finales
        UpdateStats();
    }

    // Actuallizar estadisticas
    public void UpdateStats()
    {
        damage = baseDamage * damageMultiplier;

        attackSpeed = baseAttackSpeed * attackSpeedMultiplier;

        criticalChance = baseCriticalChance + critBonus;

        lifeSteal = baseLifeSteal + lifeStealBonus;


        defense = baseDefense * defenseMultiplier;

        // =========================
        // Límites mínimos
        // =========================

        if (damage < 1)
        {
            damage = 1;
        }

        if (maxHealth < 1)
        {
            maxHealth = 1;
        }

        if (attackSpeed < 0.1f)
        {
            attackSpeed = 0.1f;
        }

        if (defense < 0)
        {
            defense = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health < 0)
        {
            health = 0;
        }

        // =========================
        // Límites porcentuales
        // ========================

        criticalChance = Mathf.Clamp(criticalChance, 0f, 1f);

        lifeSteal = Mathf.Clamp(lifeSteal, 0f, 3f);
    }

    // =========================
    // Multiplicadores
    // =========================

    public void SetDamageMultiplier(float value)
    {
        damageMultiplier = value;

        UpdateStats();
    }

    public void SetAttackSpeedMultiplier(float value)
    {
        attackSpeedMultiplier = value;

        UpdateStats();
    }

    public void SetDefenseMultiplier(float value)
    {
        defenseMultiplier = value;

        UpdateStats();
    }

    public void SetHealthMultiplier(float value)
    {
        maxHealth *= value;

        health = maxHealth;
    }

    // =========================
    // Bonus planos
    // =========================

    public void AddCritChance(float value)
    {
        critBonus += value;

        UpdateStats();
    }

    public void AddLifeSteal(float value)
    {
        lifeStealBonus += value;

        UpdateStats();
    }

    // =========================
    // Multiples Ataques
    // =========================

    public void AddProjectiles(int amount)
    {
        projectileCount += amount;
    }

    public void AddAttacks(int amount)
    {
        attackCount += amount;
    }

    public void AddTargets(int amount)
    {
        targetCount += amount;
    }

    // =========================
    // Métodos auxiliares
    // =========================

    void AssignBloodType()
    {
        bloodTypes = bloodTypesList[Random.Range(0, bloodTypesList.Length)];
    }

    public bool HasSkill(string skillName)
    {
        foreach (SkillInfo skill
            in skills)
        {
            if (skill.skillName
                == skillName)
            {
                return true;
            }
        }

        return false;
    }

    public int GenerateTier()
    {
        float roll = Random.value;

        if (roll <= 0.92f)
        {
            return 1;
        }

        if (roll <= 0.99f)
        {
            return 2;
        }

        return 3;
    }

    // =========================
    // UI de las estadisticas
    // =========================

    private void OnMouseEnter()
    {
        if (CompareTag("NoHover"))
        {
            return;
        }

        CharacterInfoUI.instance.ShowInfo(this);
    }

    private void OnMouseExit()
    {
        CharacterInfoUI.instance.HideInfo();
    }
}
