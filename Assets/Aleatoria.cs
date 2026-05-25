using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aleatoria : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        int randomStat =
            Random.Range(0, 6);

        string boostedStat = "";

        float reduceMultiplier = 1f;
        float boostMultiplier = 1f;

        // TIERS
        switch (tier)
        {
            case 1:

                // resto al 60%
                reduceMultiplier = 0.6f;

                // stat elegida x3
                boostMultiplier = 3f;

                break;

            case 2:

                // resto al 50%
                reduceMultiplier = 0.5f;

                // stat elegida x4
                boostMultiplier = 4f;

                break;

            case 3:

                // resto al 40%
                reduceMultiplier = 0.4f;

                // stat elegida x5
                boostMultiplier = 5f;

                break;
        }

        // REDUCIR TODO
        stats.SetDamageMultiplier(
            reduceMultiplier
        );

        stats.SetAttackSpeedMultiplier(
            reduceMultiplier
        );

        stats.SetDefenseMultiplier(
            reduceMultiplier
        );

        stats.SetVidaMultiplier(
            reduceMultiplier
        );

        stats.AddCritChance(-0.05f);

        stats.AddLifeSteal(-0.05f);

        // ELEGIR SOLO 1 STAT
        switch (randomStat)
        {
            // DAÑO
            case 0:

                stats.SetDamageMultiplier(
                    boostMultiplier
                );

                boostedStat = "Daño";

                break;

            // VELOCIDAD
            case 1:

                stats.SetAttackSpeedMultiplier(
                    boostMultiplier
                );

                boostedStat =
                    "Velocidad Ataque";

                break;

            // DEFENSA
            case 2:

                stats.SetDefenseMultiplier(
                    boostMultiplier
                );

                boostedStat = "Defensa";

                break;

            // VIDA
            case 3:

                stats.SetVidaMultiplier(
                    boostMultiplier
                );

                boostedStat = "Vida";

                break;

            // CRÍTICO
            case 4:

                switch (tier)
                {
                    case 1:

                        stats.AddCritChance(
                            0.30f
                        );

                        break;

                    case 2:

                        stats.AddCritChance(
                            0.50f
                        );

                        break;

                    case 3:

                        stats.AddCritChance(
                            0.70f
                        );

                        break;
                }

                boostedStat = "Crítico";

                break;

            // ROBO VIDA
            case 5:

                switch (tier)
                {
                    case 1:

                        stats.AddLifeSteal(
                            0.15f
                        );

                        break;

                    case 2:

                        stats.AddLifeSteal(
                            0.30f
                        );

                        break;

                    case 3:

                        stats.AddLifeSteal(
                            0.50f
                        );

                        break;
                }

                boostedStat =
                    "Robo Vida";

                break;
        }

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "Mutación "+
            " (" +
            boostedStat +
            ")" + "-" +"T" +
            tier;

        skill.description =
            "Potencia una stat y reduce el resto";

        stats.skills.Add(skill);
    }
}
