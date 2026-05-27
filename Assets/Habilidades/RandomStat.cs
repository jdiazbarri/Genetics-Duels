using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStat : MonoBehaviour, Skills
{
    private int tier;

    private CharacterStats stats;

    void Start()
    {
        stats =
            GetComponent<CharacterStats>();

        // TIER ALEATORIO
        tier =
            stats.GenerateTier();

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

        stats.SetHealthMultiplier(
            reduceMultiplier
        );

        stats.AddCritChance(
            -0.05f
        );

        stats.AddLifeSteal(
            -0.05f
        );

        // ELEGIR SOLO 1 STAT
        switch (randomStat)
        {
            // DAMAGE
            case 0:

                stats.SetDamageMultiplier(
                    boostMultiplier
                );

                boostedStat =
                    "Damage";

                break;

            // ATTACK SPEED
            case 1:

                stats.SetAttackSpeedMultiplier(
                    boostMultiplier
                );

                boostedStat =
                    "Attack Speed";

                break;

            // DEFENSE
            case 2:

                stats.SetDefenseMultiplier(
                    boostMultiplier
                );

                boostedStat =
                    "Defense";

                break;

            // HEALTH
            case 3:

                stats.SetHealthMultiplier(
                    boostMultiplier
                );

                boostedStat =
                    "Health";

                break;

            // CRITICAL
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

                boostedStat =
                    "Critical";

                break;

            // LIFE STEAL
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
                    "Life Steal";

                break;
        }

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "Mutation (" +

            boostedStat +

            ") - T" +

            tier;

        skill.description =

            "Boosts one stat and reduces the others";

        // EVITAR DUPLICADOS
        if (
            !stats.HasSkill(
                skill.skillName
            )
        )
        {
            stats.skills.Add(skill);
        }
    }
}
