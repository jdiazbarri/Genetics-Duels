using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string nombre = "Nombre";

    // VIDA
    public float vidaMaxima = 0;
    public float vida = 0;

    // BASE
    public float baseDFisico = 0;
    public float baseDefensa = 0f;
    public float baseVelocidadAtaque = 1;
    public float baseCritico = 0f;
    public float baseRoboVida = 0f;


    public int numeroProyectiles = 1;

    public int numeroAtaques = 1;

    public int numeroObjetivos = 1;

    public string tipoSangre;

    // FINALES
    public float dFisico;

    public float velocidadAtaque;

    public float critico;

    public float roboVida;

    public float defensa;

    // MULTIPLICADORES
    private float damageMultiplier = 1f;

    private float attackSpeedMultiplier = 1f;

    private float defenseMultiplier = 1f;

    // BONUS PLANOS
    private float critBonus = 0f;

    private float lifeStealBonus = 0f;

    // HABILIDADES ACTIVAS
    public List<SkillInfo> skills =
        new List<SkillInfo>();

    // Tipo de sangre
    string[] tipos =
        {
        "A","B","C","D",
        "E","F","G","H",
        "I","J","K","L"
        };

    void Start()
    {
        AsignarTipoGenetico();
        UpdateStats();
    }

    public void UpdateStats()
    {
        // DAÑO
        dFisico =
            baseDFisico * damageMultiplier;

        // VELOCIDAD ATAQUE
        velocidadAtaque =
            baseVelocidadAtaque *
            attackSpeedMultiplier;

        // CRTICO
        critico =
            baseCritico + critBonus;

        // ROBO VIDA
        roboVida =
            baseRoboVida + lifeStealBonus;

        // DEFENSA
        defensa =
            baseDefensa * defenseMultiplier;

        // LIMITES

        if (dFisico < 1)
        {
            dFisico = 1;
        }

        if (vidaMaxima < 1)
        {
            vidaMaxima = 1;
        }

        if (velocidadAtaque < 0.1f)
        {
            velocidadAtaque = 0.1f;
        }

        if (defensa < 0)
        {
            defensa = 0;
        }

        // CRTICO 0% ? 100%
        critico = Mathf.Clamp(
            critico,
            0f,
            1f
        );

        // ROBO VIDA 0% ? 100%
        roboVida = Mathf.Clamp(
            roboVida,
            0f,
            3f
        );

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        if (vida < 0)
        {
            vida = 0;
        }
    }

    // =========================
    // MULTIPLICADORES
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

    public void SetVidaMultiplier(float value)
    {
        vidaMaxima *= value;

        vida = vidaMaxima;
    }

    // =========================
    // BONUS PLANOS
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
        numeroProyectiles += amount;
    }

    public void AddAttacks(int amount)
    {
        numeroAtaques += amount;
    }

    public void AddTargets(int amount)
    {
        numeroObjetivos += amount;
    }

    void AsignarTipoGenetico()
    {
        tipoSangre =
            tipos[
                Random.Range(0, tipos.Length)
            ];
    }

    // =========================
    // UI HOVER
    // =========================

    private void OnMouseEnter()
    {
        CharacterInfoUI.instance.ShowInfo(this);
    }

    private void OnMouseExit()
    {
        CharacterInfoUI.instance.HideInfo();
    }

    public bool HasSkill(
    string skillName
)
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
}
