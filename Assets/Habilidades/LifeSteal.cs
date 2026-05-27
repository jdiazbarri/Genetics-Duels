using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : MonoBehaviour, Skills
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

        float lifeSteal = 0f;

        switch (tier)
        {
            case 1:

                lifeSteal = 0.10f;

                break;

            case 2:

                lifeSteal = 0.20f;

                break;

            case 3:

                lifeSteal = 0.35f;

                break;
        }

        // APLICAR ROBO VIDA
        stats.AddLifeSteal(
            lifeSteal
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "LifeSteal (" +

            (lifeSteal * 100f) +

            "%) - T" +

            tier;

        skill.description =

            (lifeSteal * 100f) +

            "% of damage heals the attacker";

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
