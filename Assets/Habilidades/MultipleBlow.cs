using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBlow : MonoBehaviour, Skills
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

        int extraHits = 0;

        switch (tier)
        {
            case 1:

                extraHits = 2;

                break;

            case 2:

                extraHits = 3;

                break;

            case 3:

                extraHits = 4;

                break;
        }

        // APLICAR GOLPES EXTRA
        stats.AddAttacks(
            extraHits
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "MultiHit (+" +

            extraHits +

            ") - T" +

            tier;

        skill.description =

            "Performs " +

            extraHits +

            " extra hits";

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
