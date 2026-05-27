using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleAttacks : MonoBehaviour, Skills
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

        int extraProjectiles = 0;

        switch (tier)
        {
            case 1:

                extraProjectiles = 2;

                break;

            case 2:

                extraProjectiles = 3;

                break;

            case 3:

                extraProjectiles = 4;

                break;
        }

        // APLICAR PROYECTILES
        stats.AddProjectiles(
            extraProjectiles
        );

        // INFO SKILL
        SkillInfo skill =
            new SkillInfo();

        skill.skillName =

            "MultiShot (+" +

            extraProjectiles +

            ") - T" +

            tier;

        skill.description =

            "Shoots " +

            extraProjectiles +

            " extra projectiles";

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
