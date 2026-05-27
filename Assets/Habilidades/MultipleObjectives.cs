using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectives : MonoBehaviour, Skills
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

        int extraTargets = 1;

        switch (tier)
        {
            case 1:

                extraTargets = 1;

                break;

            case 2:

                extraTargets = 2;

                break;

            case 3:

                extraTargets = 3;

                break;
        }

        // APLICAR OBJETIVOS EXTRA
        stats.AddTargets(
            extraTargets
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "MultiTarget (+" +

            extraTargets +

            ") - T" +

            tier;

        skill.description =

            "Attacks " +

            extraTargets +

            " extra enemies";

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
