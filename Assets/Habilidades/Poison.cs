using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour, Skills
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

        float poisonPercent = 0f;

        float poisonDuration = 0f;

        switch (tier)
        {
            case 1:

                // 3% durante 2s
                poisonPercent = 0.03f;

                poisonDuration = 2f;

                break;

            case 2:

                // 5% durante 3s
                poisonPercent = 0.05f;

                poisonDuration = 3f;

                break;

            case 3:

                // 8% durante 5s
                poisonPercent = 0.08f;

                poisonDuration = 5f;

                break;
        }

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "Poison (" +

            (poisonPercent * 100f) +

            "%) - T" +

            tier;

        skill.description =

            "Applies " +

            (poisonPercent * 100f) +

            "% damage over " +

            poisonDuration +

            "s";

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
